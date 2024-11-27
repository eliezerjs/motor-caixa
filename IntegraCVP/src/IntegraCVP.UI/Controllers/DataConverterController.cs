using IntegraCVP.Application.Enums;
using IntegraCVP.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.IO.Compression;

namespace IntegraCVP.UI.Controllers
{
    [ApiController]
    [Route("api/data-converter")]
    public class DataConverterController : ControllerBase
    {
        private readonly IReturnDataConverterService _dataConverterService;
        private readonly IBoletoM1Service _boletoM1Service;
        private readonly IPrestamistaService _prestamistaService;
        private readonly IEmailService _emailService;

        public DataConverterController(IReturnDataConverterService dataConverterService,
                                       IBoletoM1Service boletoM1Service,
                                       IPrestamistaService prestamistaService,
                                       IEmailService emailService)
        {
            _dataConverterService = dataConverterService;
            _boletoM1Service = boletoM1Service;
            _prestamistaService = prestamistaService;
            _emailService = emailService;
        }

        [HttpPost("converter-e-gerar")]
        public async Task<IActionResult> ConverterEGerar([FromForm] FileUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
            }

            // Converte o arquivo para JSON
            using var memoryStream = new MemoryStream();
            await model.File.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            var jsonResult = _dataConverterService.ConvertToJson(memoryStream);

            // Parse JSON para obter os dados
            var boletoData = System.Text.Json.JsonSerializer.Deserialize<List<Dictionary<string, string>>>(jsonResult);

            // Filtrar os tipos VD08, VD09, VIDA17 e VIDA18 - (Email)
            var boletosVD08 = boletoData.Where(b => b.ContainsKey("TIPO_DADO") && b["TIPO_DADO"] == "VD08").ToList();
            var boletosVIDA17 = boletoData.Where(b => b.ContainsKey("TIPO_DADO") && b["TIPO_DADO"] == "VIDA17").ToList();
            var boletosVD09 = boletoData.Where(b => b.ContainsKey("TIPO_DADO") && b["TIPO_DADO"] == "VD09").ToList();
            var boletosVIDA18 = boletoData.Where(b => b.ContainsKey("TIPO_DADO") && b["TIPO_DADO"] == "VIDA18").ToList();

            // Filtrar os tipos VD02 e VIDA25
            var boletosVD02 = boletoData.Where(b => b.ContainsKey("TIPO_DADO") && b["TIPO_DADO"] == "VD02").ToList();
            var boletosVIDA25 = boletoData.Where(b => b.ContainsKey("TIPO_DADO") && b["TIPO_DADO"] == "VIDA25").ToList();

            var boletosVD05 = boletoData.Where(b => b.ContainsKey("TIPO_DADO") && b["TIPO_DADO"] == "VIDA05").ToList();

            //Filtrar os tipos VD03 e VIDA27 - Pasta Seguro
            var boletosVD03 = boletoData.Where(b => b.ContainsKey("TIPO_DADO") && b.ContainsKey("FATURA") && b["TIPO_DADO"] == "VD03").ToList();
            var boletosVIDA27 = boletoData.Where(b => b.ContainsKey("TIPO_DADO") && b.ContainsKey("FATURA") && b["TIPO_DADO"] == "VIDA27").ToList();

            // Filtrar os tipos VA18, VA24, VIDA23 e VIDA24
            var boletosVA18 = boletoData.Where(b => b.ContainsKey("TIPO_DADO") && b["TIPO_DADO"] == "VA18").ToList();
            var boletosVA24 = boletoData.Where(b => b.ContainsKey("TIPO_DADO") && b["TIPO_DADO"] == "VA24").ToList();
            var boletosVIDA23 = boletoData.Where(b => b.ContainsKey("TIPO_DADO") && b["TIPO_DADO"] == "VIDA23").ToList();
            var boletosVIDA24 = boletoData.Where(b => b.ContainsKey("TIPO_DADO") && b["TIPO_DADO"] == "VIDA54").ToList();

            //Carta de Recusa
            var boletosVIDA01 = boletoData.Where(b => b.ContainsKey("TIPO_DADO") && b["TIPO_DADO"] == "VIDA01").ToList();
            var boletosVIDA02 = boletoData.Where(b => b.ContainsKey("TIPO_DADO") && b["TIPO_DADO"] == "VIDA02").ToList();
            var boletosVIDA03 = boletoData.Where(b => b.ContainsKey("TIPO_DADO") && b["TIPO_DADO"] == "VIDA03").ToList();
            var boletosVIDA04 = boletoData.Where(b => b.ContainsKey("TIPO_DADO") && b["TIPO_DADO"] == "VIDA04").ToList();

            //CartaRecusa
            var boletosVD33 = boletoData.Where(b => b.ContainsKey("TIPO_DADO") && b["TIPO_DADO"] == "VD33").ToList();

            // Lista para armazenar os arquivos PDF
            var pdfFiles = new List<(string FileName, byte[] Data)>();

            // Gera boletos para VD08
            //foreach (var boleto in boletosVD08)
            //{
            //    byte[] pdfData = _emailService.GerarEmailVidaExclusivaPdf(boleto, "VD08");
            //    pdfFiles.Add(($"VD08_{boleto["COD_PRODUTO"] ?? "Unknown"}.pdf", pdfData));
            //}

            // Gera boletos para VIDA17
            //foreach (var boleto in boletosVIDA17)
            //{
            //    byte[] pdfData = _emailService.GerarEmailVidaExclusivaPdf(boleto, "VIDA17");
            //    pdfFiles.Add(($"VIDA17_{boleto["COD_PRODUTO"] ?? "Unknown"}.pdf", pdfData));
            //}

            // Gera boletos para VD09
            //foreach (var boleto in boletosVD09)
            //{
            //    byte[] pdfData = _emailService.GerarEmailSegurosPdf(boleto, "VD09");
            //    pdfFiles.Add(($"VD09_{boleto["COD_PRODUTO"] ?? "Unknown"}.pdf", pdfData));
            //}

            // Gera boletos para VIDA18
            //foreach (var boleto in boletosVIDA18)
            //{
            //    byte[] pdfData = _emailService.GerarEmailSegurosVIDA18Pdf(boleto, "VIDA18");
            //    pdfFiles.Add(($"VIDA18_{boleto["COD_PRODUTO"] ?? "Unknown"}.pdf", pdfData));
            //}

            // Gera boletos para VD02
            foreach (var boleto in boletosVD02)
            {
                byte[] pdfData = _boletoM1Service.GerarBoletoM1(boleto, BoletoM1Type.VD02);
                pdfFiles.Add(($"VD02_{boleto["FATURA"] ?? "Unknown"}.pdf", pdfData));
            }

            // Gera boletos para VIDA25
            foreach (var boleto in boletosVIDA25)
            {
                byte[] pdfData = _boletoM1Service.GerarBoletoM1(boleto, BoletoM1Type.VIDA25);
                pdfFiles.Add(($"VIDA25_{boleto["FATURA"] ?? "Unknown"}.pdf", pdfData));
            }

            //// Gera boletos para VD03
            //foreach (var boleto in boletosVD03)
            //{
            //    byte[] pdfData = _boletoService.GerarBoletoSeguro(boleto, "VD03");
            //    pdfFiles.Add(($"VD03_{boleto["FATURA"] ?? "Unknown"}.pdf", pdfData));
            //}

            //// Gera boletos para VIDA27
            //foreach (var boleto in boletosVIDA27)
            //{
            //    byte[] pdfData = _boletoService.GerarBoletoSeguro(boleto, "VIDA27");
            //    pdfFiles.Add(($"VIDA27_{boleto["FATURA"] ?? "Unknown"}.pdf", pdfData));
            //}

            //// Gera boletos para Seguro Grupo
            //foreach (var boleto in boletosVA18)
            //{
            //    byte[] pdfData = _boletoService.GerarDocumentoVA18(boleto);
            //    pdfFiles.Add(($"VA18_{boleto["NUMDOCTO"] ?? "Unknown"}.pdf", pdfData));
            //}

            //// Gera boletos para VA24
            //foreach (var boleto in boletosVA24)
            //{
            //    byte[] pdfData = _boletoService.GerarDocumentoVA24(boleto);
            //    pdfFiles.Add(($"VA24{boleto["NUMDOCTO"] ?? "Unknown"}.pdf", pdfData));
            //}


            //// Gera boletos para VIDA23
            //foreach (var boleto in boletosVIDA23)
            //{
            //    byte[] pdfData = _boletoService.GerarBoletoVIDA23Pdf(boleto);
            //    pdfFiles.Add(($"VIDA24_{boleto["NUMDOCTO"] ?? "Unknown"}.pdf", pdfData));
            //}

            //// Gera boletos para VIDA24
            //foreach (var boleto in boletosVIDA24)
            //{
            //    byte[] pdfData = _boletoService.GerarBoletoVIDA24Pdf(boleto);
            //    pdfFiles.Add(($"VIDA24_{boleto["NUMDOCTO"] ?? "Unknown"}.pdf", pdfData));
            //}

            //// Gera boletos para Boas-Vinda
            //foreach (var body in boletosVD05)
            //{
            //    byte[] pdfData = _prestamistaService.GerarBoasVindasPdf(body);
            //    pdfFiles.Add(($"Boas-Vindas.pdf", pdfData));
            //}

            //// Gera boletos para VIDA01
            //foreach (var boleto in boletosVIDA01)
            //{
            //    byte[] pdfData = _boletoService.GerarBoletoVIDA01Pdf(boleto);
            //    pdfFiles.Add(($"VIDA01_{boleto["NUMDOCTO"] ?? "Unknown"}.pdf", pdfData));
            //}

            //// Gera boletos para VIDA02
            //foreach (var boleto in boletosVIDA02)
            //{
            //    byte[] pdfData = _boletoService.GerarBoletoVIDA02Pdf(boleto);
            //    pdfFiles.Add(($"VIDA02_{boleto["NUMDOCTO"] ?? "Unknown"}.pdf", pdfData));
            //}

            //// Gera boletos para VIDA03
            //foreach (var boleto in boletosVIDA03)
            //{
            //    byte[] pdfData = _boletoService.GerarBoletoVIDA03Pdf(boleto);
            //    pdfFiles.Add(($"VIDA03_{boleto["NUMDOCTO"] ?? "Unknown"}.pdf", pdfData));
            //}

            //// Gera boletos para VIDA04
            //foreach (var boleto in boletosVIDA04)
            //{
            //    byte[] pdfData = _boletoService.GerarBoletoVIDA04Pdf(boleto);
            //    pdfFiles.Add(($"VIDA04_{boleto["NUMDOCTO"] ?? "Unknown"}.pdf", pdfData));
            //}

            //// Gera boletos para VD33
            //foreach (var boleto in boletosVD33)
            //{
            //    byte[] pdfData = _boletoService.GerarBoletoVD33Pdf(boleto);
            //    pdfFiles.Add(($"VD33_{boleto["NUMDOCTO"] ?? "Unknown"}.pdf", pdfData));
            //}

            // Cria o arquivo ZIP contendo os PDFs
            using var zipStream = new MemoryStream();
            using (var archive = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
            {
                foreach (var file in pdfFiles)
                {
                    var zipEntry = archive.CreateEntry(file.FileName, CompressionLevel.Fastest);
                    using var entryStream = zipEntry.Open();
                    await entryStream.WriteAsync(file.Data, 0, file.Data.Length);
                }
            }
            zipStream.Position = 0;

            // Retorna o arquivo ZIP para download
            return File(zipStream.ToArray(), "application/zip", "VIDA.zip");
        }
    }
}
