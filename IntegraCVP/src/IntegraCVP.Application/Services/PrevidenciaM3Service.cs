using System.Text.Json;
using IntegraCVP.Application.Enums;
using IntegraCVP.Application.Helper;
using IntegraCVP.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace IntegraCVP.Application.Services
{
    public partial class PrevidenciaM3Service : IPrevidenciaM3Service
    {
        private const string PrevidenciaM3Folder = "PrevidenciaM3";

        private readonly IImportFileConverterService _importFileConverterService;

        public PrevidenciaM3Service(IImportFileConverterService dataConverterService)
        {
            _importFileConverterService = dataConverterService;
        }
        public async Task<byte[]> ConverterEGerarPrevidenciaM3PdfAsync(IFormFile file, PrevidenciaM3Type tipo)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("O arquivo enviado está vazio ou é inválido.");

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            var jsonResult = _importFileConverterService.ConvertToJson(memoryStream);

            var PrevidenciaM3Data = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(jsonResult);

            if (PrevidenciaM3Data == null || !PrevidenciaM3Data.Any())
                throw new ArgumentException("O arquivo não contém dados válidos.");

            var PrevidenciaM3sFiltrados = PrevidenciaM3Data
                .Where(e => e.ContainsKey("TIPO_DADO") && e["TIPO_DADO"] == tipo.ToString())
                .ToList();

            if (!PrevidenciaM3sFiltrados.Any())
                throw new ArgumentException($"Nenhum dado do tipo {tipo} foi encontrado no arquivo.");

            return GerarDocumentoPrevidenciaM3(PrevidenciaM3sFiltrados.FirstOrDefault(), tipo);
        }

        public byte[] GerarDocumentoPrevidenciaM3(Dictionary<string, string> dados, PrevidenciaM3Type tipo)
        {
            string imagePath = GetImagePath(tipo, PrevidenciaM3Folder);

            var campos = tipo switch
            {
                PrevidenciaM3Type.PK56 => GetPK56(),
                PrevidenciaM3Type.PK57 => GetPK57(),
                PrevidenciaM3Type.PK58 => GetPK58(),
                _ => throw new ArgumentException("Tipo de PrevidenciaM3 inválida.")
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
