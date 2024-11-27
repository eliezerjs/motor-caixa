using IntegraCVP.Application.Enums;
using IntegraCVP.Application.Helper;
using IntegraCVP.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text.Json;

namespace IntegraCVP.Application.Services
{
    public partial class BoletoM3Service : IBoletoM3Service
    {
        public const string BoletoM3 = "BoletoM3";
        private readonly IImportFileConverterService _dataConverterService;

        public BoletoM3Service(IImportFileConverterService dataConverterService)
        {
            _dataConverterService = dataConverterService;
        }

        public async Task<byte[]> ConverterEGerarPdfAsync(IFormFile file, BoletoM3Type tipo)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("O arquivo enviado está vazio ou é inválido.");

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            var jsonResult = _dataConverterService.ConvertToJson(memoryStream);

            var boletoData = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(jsonResult);

            if (boletoData == null || !boletoData.Any())
                throw new ArgumentException("O arquivo não contém dados válidos.");

            var boletosFiltrados = boletoData
                .Where(b => b.ContainsKey("TIPO_DADO") && b["TIPO_DADO"] == tipo.ToString())
                .ToList();

            if (!boletosFiltrados.Any())
                throw new ArgumentException($"Nenhum dado do tipo {tipo} foi encontrado no arquivo.");

            return GerarBoletoM3(boletosFiltrados.FirstOrDefault(), tipo);
        }
        public byte[] GerarBoletoM3(Dictionary<string, string> dadosBoleto, BoletoM3Type tipo)
        {
            string imagePath = GetImagePath(tipo, BoletoM3);

            var campos = tipo switch
            {
                BoletoM3Type.VD03 => GetCamposSeguro(),
                BoletoM3Type.VIDA27 => GetCamposSeguro(),
                _ => throw new ArgumentException("Tipo de boleto inválido.")
            };

            using var pdfStream = new MemoryStream();
            var (document, pdfDocument, pdfPage) = PdfHelper.InitializePdfDocument(imagePath, pdfStream);

            foreach (var (key, x, y, fontSize) in campos)
            {
                document.AddTextField(dadosBoleto, key, x, y, fontSize, pdfPage);
            }

            document.Close();
            return pdfStream.ToArray();
        }
    }
}
