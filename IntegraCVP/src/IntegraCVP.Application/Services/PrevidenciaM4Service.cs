using System.Text.Json;
using IntegraCVP.Application.Enums;
using IntegraCVP.Application.Helper;
using IntegraCVP.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace IntegraCVP.Application.Services
{
    public partial class PrevidenciaM4Service : IPrevidenciaM4Service
    {
        private const string PrevidenciaM4Folder = "PrevidenciaM4";

        private readonly IImportFileConverterService _importFileConverterService;

        public PrevidenciaM4Service(IImportFileConverterService dataConverterService)
        {
            _importFileConverterService = dataConverterService;
        }
        public async Task<byte[]> ConverterEGerarPrevidenciaM4PdfAsync(IFormFile file, PrevidenciaM4Type tipo)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("O arquivo enviado está vazio ou é inválido.");

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            var jsonResult = _importFileConverterService.ConvertToJson(memoryStream);

            var PrevidenciaM4Data = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(jsonResult);

            if (PrevidenciaM4Data == null || !PrevidenciaM4Data.Any())
                throw new ArgumentException("O arquivo não contém dados válidos.");

            var PrevidenciaM4sFiltrados = PrevidenciaM4Data
                .Where(e => e.ContainsKey("TIPO_DADO") && e["TIPO_DADO"] == tipo.ToString())
                .ToList();

            if (!PrevidenciaM4sFiltrados.Any())
                throw new ArgumentException($"Nenhum dado do tipo {tipo} foi encontrado no arquivo.");

            return GerarDocumentoPrevidenciaM4(PrevidenciaM4sFiltrados.FirstOrDefault(), tipo);
        }

        public byte[] GerarDocumentoPrevidenciaM4(Dictionary<string, string> dados, PrevidenciaM4Type tipo)
        {
            string imagePath = GetImagePath(tipo, PrevidenciaM4Folder);

            var campos = tipo switch
            {
                PrevidenciaM4Type.PK05 => GetPK05(),
                PrevidenciaM4Type.PK06 => GetPK06(),
                _ => throw new ArgumentException("Tipo de PrevidenciaM4 inválida.")
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
