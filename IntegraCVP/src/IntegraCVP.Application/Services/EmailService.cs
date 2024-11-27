using System.Text.Json;
using IntegraCVP.Application.Enums;
using IntegraCVP.Application.Helper;
using IntegraCVP.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace IntegraCVP.Application.Services
{
    public partial class EmailService : IEmailService
    {
        private const string EmailFolder = "Email";

        private readonly IImportFileConverterService _dataConverterService;

        public EmailService(IImportFileConverterService dataConverterService)
        {
            _dataConverterService = dataConverterService;
        }
        public async Task<byte[]> ConverterEGerarEmailPdfAsync(IFormFile file, EmailType tipo)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("O arquivo enviado está vazio ou é inválido.");

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            var jsonResult = _dataConverterService.ConvertToJson(memoryStream);

            var emailData = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(jsonResult);

            if (emailData == null || !emailData.Any())
                throw new ArgumentException("O arquivo não contém dados válidos.");

            var emailsFiltrados = emailData
                .Where(e => e.ContainsKey("TIPO_DADO") && e["TIPO_DADO"] == tipo.ToString())
                .ToList();

            if (!emailsFiltrados.Any())
                throw new ArgumentException($"Nenhum dado do tipo {tipo} foi encontrado no arquivo.");

            return GerarEmailPdf(emailsFiltrados.FirstOrDefault(), tipo);
        }

        public byte[] GerarEmailPdf(Dictionary<string, string> dados, EmailType tipo)
        {
            string imagePath = GetImagePath(tipo, EmailFolder);

            var campos = tipo switch
            {              
                EmailType.VIDA18 => GetVIDA18(),
                EmailType.SEGUROS => GetEmailSeguros(),
                EmailType.VIDAEXCLUSIVA => GetEmailVidaExclusiva(),
                _ => throw new ArgumentException("Tipo de email inválido.")
            };

            using var pdfStream = new MemoryStream();
            var (document, pdfDocument, pdfPage) = PdfHelper.InitializePdfDocument(imagePath, pdfStream);

            foreach (var (key, x, y, fontSize, fontColor) in campos)
            {
                if (dados.ContainsKey(key))
                {
                    document.AddTextField(dados[key], x, y, fontSize, pdfPage, fontColor);
                }
            }

            document.Close();
            return pdfStream.ToArray();
        }
    }
}
