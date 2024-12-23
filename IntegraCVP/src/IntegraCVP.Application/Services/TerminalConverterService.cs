using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using IntegraCVP.Application.Enums;
using IntegraCVP.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace IntegraCVP.Application.Services
{
    public class TerminalConverterService: ITerminalConverterService
    {
        private readonly IImportFileConverterService _dataConverterService;
        private readonly IBoletoM1Service _boletoM1Service;
        private readonly IBoletoM2Service _boletoM2Service;
        private readonly IBoletoM3Service _boletoM3Service;
        private readonly IBoletoM4Service _boletoM4Service;
        private readonly IPrestamistaService _prestamistaService;

        public TerminalConverterService(
            IImportFileConverterService dataConverterService,
            IBoletoM1Service boletoM1Service,
            IBoletoM4Service boletoM4Service,
            IPrestamistaService prestamistaService)
        {
            _dataConverterService = dataConverterService;
            _boletoM1Service = boletoM1Service;
            _boletoM4Service = boletoM4Service;
            _prestamistaService = prestamistaService;
        }

        public async Task<byte[]> ConverterEGerarZipAsync(IFormFile file)
        {
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            var jsonResult = _dataConverterService.ConvertToJson(memoryStream);
            var boletoData = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(jsonResult);

            if (boletoData == null || !boletoData.Any())
            {
                throw new ArgumentException("O arquivo não contém dados válidos.");
            }

            var pdfFiles = new List<(string FileName, byte[] Data)>();

            // Gerar PDFs por tipo de boleto M1
            ProcessarBoletosM1PorTipo(boletoData, BoletoM1Type.VD02, pdfFiles);
            ProcessarBoletosM1PorTipo(boletoData, BoletoM1Type.VIDA25, pdfFiles);

            // Gerar PDFs por tipo de boleto M4
            ProcessarBoletosM4PorTipo(boletoData, BoletoM4Type.VA18,pdfFiles);
            ProcessarBoletosM4PorTipo(boletoData, BoletoM4Type.VA24, pdfFiles);
            ProcessarBoletosM4PorTipo(boletoData, BoletoM4Type.VIDA23, pdfFiles);
            ProcessarBoletosM4PorTipo(boletoData, BoletoM4Type.VIDA24, pdfFiles);

            // Gerar o arquivo ZIP
            return GerarZipComPdfs(pdfFiles);
        }

        public async Task<string> ConverterEGerarPrevidenciaAsync(IFormFile file)
        {
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            var jsonResult = _dataConverterService.ConvertFileToJson(memoryStream);
            
            return jsonResult;
        }

        

        private void ProcessarBoletosM1PorTipo(
            List<Dictionary<string, string>> boletos,
            BoletoM1Type tipo,
            List<(string FileName, byte[] Data)> pdfFiles)
        {
            var boletosFiltrados = boletos.Where(b => b.ContainsKey("TIPO_DADO") && b["TIPO_DADO"] == tipo.ToString()).ToList();

            foreach (var itemBoleto in boletosFiltrados)
            {
                var pdfData = _boletoM1Service.GerarBoletoM1(itemBoleto, tipo);
                pdfFiles.Add(($"{tipo}_{itemBoleto["FATURA"] ?? "Unknown"}.pdf", pdfData));
            }
        }

        private void ProcessarBoletosM4PorTipo(
            List<Dictionary<string, string>> boletos,
            BoletoM4Type tipo,
            List<(string FileName, byte[] Data)> pdfFiles)
        {
            var boletosFiltrados = boletos.Where(b => b.ContainsKey("TIPO_DADO") && b["TIPO_DADO"] == tipo.ToString()).ToList();

            foreach (var itemBoleto in boletosFiltrados)
            {
                var pdfData = _boletoM4Service.GerarBoletoM4(itemBoleto, tipo);
                pdfFiles.Add(($"{tipo}_{itemBoleto["NUMDOCTO"] ?? "Unknown"}.pdf", pdfData));
            }
        }

        private byte[] GerarZipComPdfs(List<(string FileName, byte[] Data)> pdfFiles)
        {
            using var zipStream = new MemoryStream();
            using (var archive = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
            {
                foreach (var file in pdfFiles)
                {
                    var zipEntry = archive.CreateEntry(file.FileName, CompressionLevel.Fastest);
                    using var entryStream = zipEntry.Open();
                    entryStream.Write(file.Data, 0, file.Data.Length);
                }
            }

            return zipStream.ToArray();
        }
    }
}

