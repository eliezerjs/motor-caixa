﻿using System.Text.Json;
using IntegraCVP.Application.Enums;
using IntegraCVP.Application.Helper;
using IntegraCVP.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace IntegraCVP.Application.Services
{
    public partial class InadimplenciaService : IInadimplenciaService
    {
        private const string InadimplenciaFolder = "Inadimplencia";

        private readonly IImportFileConverterService _importFileConverterService;

        public InadimplenciaService(IImportFileConverterService dataConverterService)
        {
            _importFileConverterService = dataConverterService;
        }
        public async Task<byte[]> ConverterEGerarInadimplenciaPdfAsync(IFormFile file, InadimplenciaType tipo)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("O arquivo enviado está vazio ou é inválido.");

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            var jsonResult = _importFileConverterService.ConvertToJson(memoryStream);

            var InadimplenciaData = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(jsonResult);

            if (InadimplenciaData == null || !InadimplenciaData.Any())
                throw new ArgumentException("O arquivo não contém dados válidos.");

            var InadimplenciasFiltrados = InadimplenciaData
                .Where(e => e.ContainsKey("TIPO_DADO") && e["TIPO_DADO"] == tipo.ToString())
                .ToList();

            if (!InadimplenciasFiltrados.Any())
                throw new ArgumentException($"Nenhum dado do tipo {tipo} foi encontrado no arquivo.");

            return GerarDocumentoInadimplencia(InadimplenciasFiltrados.FirstOrDefault(), tipo);
        }

        public byte[] GerarDocumentoInadimplencia(Dictionary<string, string> dados, InadimplenciaType tipo)
        {
            string imagePath = GetImagePath(tipo, InadimplenciaFolder);

            var campos = tipo switch
            {
                InadimplenciaType.VD33 => GetVD33(),
                _ => throw new ArgumentException("Tipo de Inadimplencia inválida.")
            };

            using var pdfStream = new MemoryStream();
            var (document, pdfDocument, pdfPage) = PdfHelper.InitializePdfDocument(imagePath, pdfStream);

            foreach (var (key, x, y, fontSize) in campos)
            {
                if (dados.ContainsKey(key))
                {
                    document.AddTextField(dados[key], x, y, fontSize, pdfPage);
                }
            }

            document.Close();
            return pdfStream.ToArray();
        }
    }
}
