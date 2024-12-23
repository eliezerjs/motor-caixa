using System.Text.Json;
using IntegraCVP.Application.Enums;
using IntegraCVP.Application.Helper;
using IntegraCVP.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace IntegraCVP.Application.Services
{
    public partial class PrevidenciaM1Service : IPrevidenciaM1Service
    {
        private const string PrevidenciaM1Folder = "PrevidenciaM1";

        private readonly IImportFilePrevConverterService _importFilePrevConverterService;

        public PrevidenciaM1Service(IImportFilePrevConverterService dataConverterService)
        {
            _importFilePrevConverterService = dataConverterService;
        }
        public async Task<byte[]> ConverterEGerarPrevidenciaM1PdfAsync(IFormFile file, PrevidenciaM1Type tipo)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("O arquivo enviado está vazio ou é inválido.");

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            var PrevidenciaM1Data = await _importFilePrevConverterService.ProcessDataAsync(memoryStream);

            if (PrevidenciaM1Data == null || !PrevidenciaM1Data.Any())
                throw new ArgumentException("O arquivo não contém dados válidos.");

            var primeiroParticipante = PrevidenciaM1Data
                .FirstOrDefault(e => e.ContainsKey("RecordType") && e["RecordType"] == "13");

            if (!primeiroParticipante.Any())
                throw new ArgumentException($"Nenhum dado do tipo {tipo} foi encontrado no arquivo.");

            return GerarDocumentoPrevidenciaM1(primeiroParticipante, tipo);
        }

        public byte[] GerarDocumentoPrevidenciaM1(Dictionary<string, string> dados, PrevidenciaM1Type tipo)
        {
            string imagePath = GetImagePath(tipo, PrevidenciaM1Folder);

            var campos = tipo switch
            {
                PrevidenciaM1Type.PK28 => GetPK28(),
                PrevidenciaM1Type.PK29 => GetPK29(),
                PrevidenciaM1Type.PK30 => GetPK30(),
                PrevidenciaM1Type.PK31 => GetPK31(),
                PrevidenciaM1Type.PK32 => GetPK32(),
                PrevidenciaM1Type.PK33 => GetPK33(),
                PrevidenciaM1Type.PK34 => GetPK34(),
                PrevidenciaM1Type.PK36 => GetPK36(),
                PrevidenciaM1Type.PK47 => GetPK47(),
                _ => throw new ArgumentException("Tipo de PrevidenciaM1 inválida.")
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
