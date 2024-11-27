using System.Text.Json;
using IntegraCVP.Application.Enums;
using IntegraCVP.Application.Helper;
using IntegraCVP.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace IntegraCVP.Application.Services
{
    public partial class BoasVindasService : IBoasVindasService
    {
        private const string BoasVindasFolder = "BoasVindas";

        private readonly IImportFileConverterService _dataConverterService;

        public BoasVindasService(IImportFileConverterService dataConverterService)
        {
            _dataConverterService = dataConverterService;
        }
        public async Task<byte[]> ConverterEGerarBoasVindasPdfAsync(IFormFile file, BoasVindasType tipo)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("O arquivo enviado está vazio ou é inválido.");

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            var jsonResult = _dataConverterService.ConvertToJson(memoryStream);

            var BoasVindasData = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(jsonResult);

            if (BoasVindasData == null || !BoasVindasData.Any())
                throw new ArgumentException("O arquivo não contém dados válidos.");

            var BoasVindassFiltrados = BoasVindasData
                .Where(e => e.ContainsKey("TIPO_DADO") && e["TIPO_DADO"] == tipo.ToString())
                .ToList();

            if (!BoasVindassFiltrados.Any())
                throw new ArgumentException($"Nenhum dado do tipo {tipo} foi encontrado no arquivo.");

            return GerarDocumentoBoasVindas(BoasVindassFiltrados.FirstOrDefault(), tipo);
        }

        public byte[] GerarDocumentoBoasVindas(Dictionary<string, string> dados, BoasVindasType tipo)
        {
            string imagePath = GetImagePath(tipo, BoasVindasFolder);

            var campos = tipo switch
            {              
                BoasVindasType.VIDA05 => GetCamposVIDA05(),
                BoasVindasType.VIDA07 => GetCamposVIDA07(),
                _ => throw new ArgumentException("Tipo de Boas-Vindas inválido.")
            };

            using var pdfStream = new MemoryStream();
            var (document, pdfDocument, pdfPage) = PdfHelper.InitializePdfDocument(imagePath, pdfStream);

            foreach (var (key, x, y, fontSize) in campos)
            {
                if (dados.ContainsKey(key))
                {
                    document.AddTextField(dados[key], x, y, fontSize, pdfPage);
                }
            }

            document.Close();
            return pdfStream.ToArray();
        }
    }
}
