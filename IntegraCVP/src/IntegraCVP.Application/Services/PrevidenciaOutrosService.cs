using System.Text.Json;
using IntegraCVP.Application.Enums;
using IntegraCVP.Application.Helper;
using IntegraCVP.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace IntegraCVP.Application.Services
{
    public partial class PrevidenciaOutrosService : IPrevidenciaOutrosService
    {
        private const string PrevidenciaOutrosFolder = "PrevidenciaOutros";

        private readonly IImportFileConverterService _importFileConverterService;

        public PrevidenciaOutrosService(IImportFileConverterService dataConverterService)
        {
            _importFileConverterService = dataConverterService;
        }
        public async Task<byte[]> ConverterEGerarPrevidenciaOutrosPdfAsync(IFormFile file, PrevidenciaOutrosType tipo)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("O arquivo enviado está vazio ou é inválido.");

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            var jsonResult = _importFileConverterService.ConvertToJson(memoryStream);

            var PrevidenciaOutrosData = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(jsonResult);

            if (PrevidenciaOutrosData == null || !PrevidenciaOutrosData.Any())
                throw new ArgumentException("O arquivo não contém dados válidos.");

            var PrevidenciaOutrossFiltrados = PrevidenciaOutrosData
                .Where(e => e.ContainsKey("TIPO_DADO") && e["TIPO_DADO"] == tipo.ToString())
                .ToList();

            if (!PrevidenciaOutrossFiltrados.Any())
                throw new ArgumentException($"Nenhum dado do tipo {tipo} foi encontrado no arquivo.");

            return GerarDocumentoPrevidenciaOutros(PrevidenciaOutrossFiltrados.FirstOrDefault(), tipo);
        }

        public byte[] GerarDocumentoPrevidenciaOutros(Dictionary<string, string> dados, PrevidenciaOutrosType tipo)
        {
            string imagePath = GetImagePath(tipo, PrevidenciaOutrosFolder);

            var campos = tipo switch
            {
                PrevidenciaOutrosType.PK11 => GetPK11(),
                PrevidenciaOutrosType.PK15 => GetPK15(),
                PrevidenciaOutrosType.PK35 => GetPK35(),
                PrevidenciaOutrosType.PK37 => GetPK37(),
                PrevidenciaOutrosType.PK44 => GetPK44(),
                PrevidenciaOutrosType.PK48 => GetPK48(),
                PrevidenciaOutrosType.PK49 => GetPK49(),
                PrevidenciaOutrosType.PK53 => GetPK53(),
                _ => throw new ArgumentException("Tipo de PrevidenciaOutros inválida.")
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
