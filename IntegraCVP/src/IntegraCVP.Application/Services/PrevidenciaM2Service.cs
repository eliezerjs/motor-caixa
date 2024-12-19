using System.Text.Json;
using IntegraCVP.Application.Enums;
using IntegraCVP.Application.Helper;
using IntegraCVP.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace IntegraCVP.Application.Services
{
    public partial class PrevidenciaM2Service : IPrevidenciaM2Service
    {
        private const string PrevidenciaM2Folder = "PrevidenciaM2";

        private readonly IImportFileConverterService _importFileConverterService;

        public PrevidenciaM2Service(IImportFileConverterService dataConverterService)
        {
            _importFileConverterService = dataConverterService;
        }
        public async Task<byte[]> ConverterEGerarPrevidenciaM2PdfAsync(IFormFile file, PrevidenciaM2Type tipo)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("O arquivo enviado está vazio ou é inválido.");

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            var jsonResult = _importFileConverterService.ConvertToJson(memoryStream);

            var PrevidenciaM2Data = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(jsonResult);

            if (PrevidenciaM2Data == null || !PrevidenciaM2Data.Any())
                throw new ArgumentException("O arquivo não contém dados válidos.");

            var PrevidenciaM2sFiltrados = PrevidenciaM2Data
                .Where(e => e.ContainsKey("TIPO_DADO") && e["TIPO_DADO"] == tipo.ToString())
                .ToList();

            if (!PrevidenciaM2sFiltrados.Any())
                throw new ArgumentException($"Nenhum dado do tipo {tipo} foi encontrado no arquivo.");

            return GerarDocumentoPrevidenciaM2(PrevidenciaM2sFiltrados.FirstOrDefault(), tipo);
        }

        public byte[] GerarDocumentoPrevidenciaM2(Dictionary<string, string> dados, PrevidenciaM2Type tipo)
        {
            string imagePath = GetImagePath(tipo, PrevidenciaM2Folder);

            var campos = tipo switch
            {
                PrevidenciaM2Type.PK08 => GetPK08(),
                PrevidenciaM2Type.PK09 => GetPK09(),
                PrevidenciaM2Type.PK10 => GetPK10(),
                _ => throw new ArgumentException("Tipo de PrevidenciaM2 inválida.")
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
