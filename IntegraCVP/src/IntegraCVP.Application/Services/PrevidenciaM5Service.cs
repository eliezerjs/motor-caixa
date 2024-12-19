using System.Text.Json;
using IntegraCVP.Application.Enums;
using IntegraCVP.Application.Helper;
using IntegraCVP.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace IntegraCVP.Application.Services
{
    public partial class PrevidenciaM5Service : IPrevidenciaM5Service
    {
        private const string PrevidenciaM5Folder = "PrevidenciaM5";

        private readonly IImportFileConverterService _importFileConverterService;

        public PrevidenciaM5Service(IImportFileConverterService dataConverterService)
        {
            _importFileConverterService = dataConverterService;
        }
        public async Task<byte[]> ConverterEGerarPrevidenciaM5PdfAsync(IFormFile file, PrevidenciaM5Type tipo)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("O arquivo enviado está vazio ou é inválido.");

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            var jsonResult = _importFileConverterService.ConvertToJson(memoryStream);

            var PrevidenciaM5Data = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(jsonResult);

            if (PrevidenciaM5Data == null || !PrevidenciaM5Data.Any())
                throw new ArgumentException("O arquivo não contém dados válidos.");

            var PrevidenciaM5sFiltrados = PrevidenciaM5Data
                .Where(e => e.ContainsKey("TIPO_DADO") && e["TIPO_DADO"] == tipo.ToString())
                .ToList();

            if (!PrevidenciaM5sFiltrados.Any())
                throw new ArgumentException($"Nenhum dado do tipo {tipo} foi encontrado no arquivo.");

            return GerarDocumentoPrevidenciaM5(PrevidenciaM5sFiltrados.FirstOrDefault(), tipo);
        }

        public byte[] GerarDocumentoPrevidenciaM5(Dictionary<string, string> dados, PrevidenciaM5Type tipo)
        {
            string imagePath = GetImagePath(tipo, PrevidenciaM5Folder);

            var campos = tipo switch
            {
                PrevidenciaM5Type.PK12 => GetPK12(),
                PrevidenciaM5Type.PK13 => GetPK13(),
                PrevidenciaM5Type.PK14 => GetPK14(),
                _ => throw new ArgumentException("Tipo de PrevidenciaM5 inválida.")
            };

            using var pdfStream = new MemoryStream();
            var (document, pdfDocument, pdfPage) = PdfHelper.InitializePdfDocument(imagePath, pdfStream);

            foreach (var (key, x, y, fontSize, isBold) in campos)
            {
                if (dados.ContainsKey(key))
                {
                    document.AddTextField(dados[key], x, y, fontSize, isBold, pdfPage);
                }
            }

            document.Close();
            return pdfStream.ToArray();
        }
    }
}
