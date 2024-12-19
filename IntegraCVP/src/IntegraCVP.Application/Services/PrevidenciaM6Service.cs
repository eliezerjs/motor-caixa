using System.Text.Json;
using IntegraCVP.Application.Enums;
using IntegraCVP.Application.Helper;
using IntegraCVP.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace IntegraCVP.Application.Services
{
    public partial class PrevidenciaM6Service : IPrevidenciaM6Service
    {
        private const string PrevidenciaM6Folder = "PrevidenciaM6";

        private readonly IImportFileConverterService _importFileConverterService;

        public PrevidenciaM6Service(IImportFileConverterService dataConverterService)
        {
            _importFileConverterService = dataConverterService;
        }
        public async Task<byte[]> ConverterEGerarPrevidenciaM6PdfAsync(IFormFile file, PrevidenciaM6Type tipo)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("O arquivo enviado está vazio ou é inválido.");

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            var jsonResult = _importFileConverterService.ConvertToJson(memoryStream);

            var PrevidenciaM6Data = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(jsonResult);

            if (PrevidenciaM6Data == null || !PrevidenciaM6Data.Any())
                throw new ArgumentException("O arquivo não contém dados válidos.");

            var PrevidenciaM6sFiltrados = PrevidenciaM6Data
                .Where(e => e.ContainsKey("TIPO_DADO") && e["TIPO_DADO"] == tipo.ToString())
                .ToList();

            if (!PrevidenciaM6sFiltrados.Any())
                throw new ArgumentException($"Nenhum dado do tipo {tipo} foi encontrado no arquivo.");

            return GerarDocumentoPrevidenciaM6(PrevidenciaM6sFiltrados.FirstOrDefault(), tipo);
        }

        public byte[] GerarDocumentoPrevidenciaM6(Dictionary<string, string> dados, PrevidenciaM6Type tipo)
        {
            string imagePath = GetImagePath(tipo, PrevidenciaM6Folder);

            var campos = tipo switch
            {
                PrevidenciaM6Type.PK17 => GetPK17(),
                PrevidenciaM6Type.PK21 => GetPK21(),
                _ => throw new ArgumentException("Tipo de PrevidenciaM6 inválida.")
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
