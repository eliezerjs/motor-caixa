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

        private readonly IImportFilePrevConverterService _importFileConverterService;

        public PrevidenciaM2Service(IImportFilePrevConverterService dataConverterService)
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

            var PrevidenciaM2Data = await _importFileConverterService.ProcessDataAsync(memoryStream);

            if (PrevidenciaM2Data == null || !PrevidenciaM2Data.Any())
                throw new ArgumentException("O arquivo não contém dados válidos.");

            var primeiroParticipante = PrevidenciaM2Data
                .FirstOrDefault(e => e.ContainsKey("RecordType") && e["RecordType"] == "13");

            if (!PrevidenciaM2Data.Any())
                throw new ArgumentException($"Nenhum dado do tipo {tipo} foi encontrado no arquivo.");

            return GerarDocumentoPrevidenciaM2(primeiroParticipante, tipo);
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
