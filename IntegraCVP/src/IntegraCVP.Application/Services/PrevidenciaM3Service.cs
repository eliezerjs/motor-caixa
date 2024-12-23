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

        private readonly IImportFilePrevConverterService _importFileConverterService;

        public PrevidenciaM3Service(IImportFilePrevConverterService dataConverterService)
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

            var PrevidenciaM3Data = await _importFileConverterService.ProcessDataAsync(memoryStream);

            if (PrevidenciaM3Data == null || !PrevidenciaM3Data.Any())
                throw new ArgumentException("O arquivo não contém dados válidos.");

            var primeiroParticipante = PrevidenciaM3Data
                 .FirstOrDefault(e => e.ContainsKey("RecordType") && e["RecordType"] == "13");

            return GerarDocumentoPrevidenciaM3(primeiroParticipante, tipo);
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
