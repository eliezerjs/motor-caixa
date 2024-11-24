using IntegraCVP.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.IO.Compression;

namespace IntegraCVP.UI.Controllers
{
    [ApiController]
    [Route("api/data-converter")]
    public class DataConverterController : ControllerBase
    {
        private readonly IDataConverterService _dataConverterService;
        private readonly IBoletoService _boletoService;
        private readonly IPrestamistaService _prestamistaService;

        public DataConverterController(IDataConverterService dataConverterService, 
                                       IBoletoService boletoService,
                                       IPrestamistaService prestamistaService)
        {
            _dataConverterService = dataConverterService;
            _boletoService = boletoService;
            _prestamistaService = prestamistaService;
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

            // Filtrar os tipos VD02 e VIDA25
            var boletosVD02 = boletoData.Where(b => b.ContainsKey("TIPO_DADO") && b["TIPO_DADO"] == "VD02").ToList();
            var boletosVIDA25 = boletoData.Where(b => b.ContainsKey("TIPO_DADO") && b["TIPO_DADO"] == "VIDA25").ToList();

            var boletosVD05 = boletoData.Where(b => b.ContainsKey("TIPO_DADO") && b["TIPO_DADO"] == "VIDA05").ToList();

            // Filtrar os tipos VA18, VA24, VIDA23 e VIDA24
            var boletosVA18 = boletoData.Where(b => b.ContainsKey("TIPO_DADO") && b["TIPO_DADO"] == "VA18").ToList();
            var boletosVA24 = boletoData.Where(b => b.ContainsKey("TIPO_DADO") && b["TIPO_DADO"] == "VA24").ToList();
            var boletosVIDA23 = boletoData.Where(b => b.ContainsKey("TIPO_DADO") && b["TIPO_DADO"] == "VIDA23").ToList();
            var boletosVIDA24 = boletoData.Where(b => b.ContainsKey("TIPO_DADO") && b["TIPO_DADO"] == "VIDA54").ToList();


            // Lista para armazenar os arquivos PDF
            var pdfFiles = new List<(string FileName, byte[] Data)>();

            // Gera boletos para VD02
            foreach (var boleto in boletosVD02)
            {
                byte[] pdfData = _boletoService.GerarBoletoVD02Pdf(boleto);
                pdfFiles.Add(($"VD02_{boleto["FATURA"] ?? "Unknown"}.pdf", pdfData));
            }

            // Gera boletos para VIDA25
            foreach (var boleto in boletosVIDA25)
            {
                byte[] pdfData = _boletoService.GerarBoletoVD02Pdf(boleto);
                pdfFiles.Add(($"VIDA25_{boleto["FATURA"] ?? "Unknown"}.pdf", pdfData));
            }

            // Gera boletos para Seguro Grupo
            foreach (var boleto in boletosVA18)
            {
                byte[] pdfData = _boletoService.GerarBoletoVA18Pdf(boleto);
                pdfFiles.Add(($"VA18_{boleto["NUMDOCTO"] ?? "Unknown"}.pdf", pdfData));
            }


            foreach (var body in boletosVD05)
            {
                byte[] pdfData = _prestamistaService.GerarBoasVindasPdf(body);
                pdfFiles.Add(($"Boas-Vindas.pdf", pdfData));
            }

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
