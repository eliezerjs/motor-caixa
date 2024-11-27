using IntegraCVP.Application.Enums;
using IntegraCVP.Application.Interfaces;
using IntegraCVP.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace IntegraCVP.UI.Controllers
{
    [ApiController]
    [Route("api/boleto-m1")]
    public class BoletoM1Controller : ControllerBase
    {
        private readonly IBoletoM1Service _boletoM1Service;
        private readonly IBoletoM2Service _boletoM2Service;
        private readonly IBoletoM3Service _boletoM3Service;
        private readonly IBoletoM4Service _boletoM4Service;

        private readonly IImportFileConverterService _dataConverterService;
        public BoletoM1Controller(IBoletoM1Service boletoM1Service, 
                                IBoletoM2Service boletoM2Service,
                                IBoletoM3Service boletoM3Service,
                                IBoletoM4Service boletoM4Service,
                                IImportFileConverterService dataConverterService)
        {
            _boletoM1Service = boletoM1Service;
            _boletoM2Service = boletoM2Service;
            _boletoM3Service = boletoM3Service;
            _boletoM4Service = boletoM4Service;
            _dataConverterService = dataConverterService;        
        }

        //[HttpPost("gerar-vd33")]
        //public async Task<IActionResult> GerarBoletoVD33([FromForm] FileUploadModel model)
        //{
        //    if (model.File == null || model.File.Length == 0)
        //    {
        //        return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
        //    }

        //    // Converte o arquivo para JSON
        //    using var memoryStream = new MemoryStream();
        //    await model.File.CopyToAsync(memoryStream);
        //    memoryStream.Position = 0;

        //    var jsonResult = _dataConverterService.ConvertToJson(memoryStream);

        //    // Parse JSON para obter os dados
        //    var boletoData = System.Text.Json.JsonSerializer.Deserialize<List<Dictionary<string, string>>>(jsonResult);

        //    // Filtrar os tipos VD33
        //    var boletosInfo = boletoData.Where(b => b.ContainsKey("TIPO_DADO") && b["TIPO_DADO"] == "VD33").ToList();

        //    // Lista para armazenar os arquivos PDF
        //    var pdfFiles = new List<(string FileName, byte[] Data)>();

        //    byte[] pdfData = _boletoService.GerarBoletoVD33Pdf(boletosInfo.FirstOrDefault());
        //    return File(pdfData, "application/pdf", "Boleto.pdf");
        //}

        [HttpPost("gerar-vd02")]
        public async Task<IActionResult> GerarVD02([FromForm] FileUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
            }

            try
            {
                var pdfData = await _boletoM1Service.ConverterEGerarPdfAsync(model.File, BoletoM1Type.VD02);
                return File(pdfData, "application/pdf", "Boleto-VD02.pdf");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao processar o boleto: {ex.Message}");
            }
        }

        [HttpPost("gerar-vida25")]
        public async Task<IActionResult> GerarVida25([FromForm] FileUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
            }

            try
            {
                var pdfData = await _boletoM1Service.ConverterEGerarPdfAsync(model.File, BoletoM1Type.VIDA25);
                return File(pdfData, "application/pdf", "Boleto-VIDA25.pdf");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao processar o boleto: {ex.Message}");
            }
        }

        //[HttpPost("gerar-vd02")]
        //public async Task<IActionResult> ConverterEGerar([FromForm] FileUploadModel model)
        //{
        //    if (model.File == null || model.File.Length == 0)
        //    {
        //        return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
        //    }

        //    // Converte o arquivo para JSON
        //    using var memoryStream = new MemoryStream();
        //    await model.File.CopyToAsync(memoryStream);
        //    memoryStream.Position = 0;

        //    var jsonResult = _dataConverterService.ConvertToJson(memoryStream);

        //    // Parse JSON para obter os dados
        //    var boletoData = System.Text.Json.JsonSerializer.Deserialize<List<Dictionary<string, string>>>(jsonResult);
        //    // Filtrar os tipos VD02 
        //    var boletosVD02 = boletoData.Where(b => b.ContainsKey("TIPO_DADO") && b["TIPO_DADO"] == "VD02").ToList();

        //    // Lista para armazenar os arquivos PDF
        //    var pdfFiles = new List<(string FileName, byte[] Data)>();

        //    byte[] pdfData = _boletoService.GerarBoletoM1(boletosVD02.FirstOrDefault(), BoletoM1Type.VD02);
        //    return File(pdfData, "application/pdf", "Boleto.pdf");
        //}

        //[HttpPost("gerar-va18")]
        //public async Task<IActionResult> GerarBoletoVA18([FromForm] FileUploadModel model)
        //{
        //    if (model.File == null || model.File.Length == 0)
        //    {
        //        return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
        //    }

        //    // Converte o arquivo para JSON
        //    using var memoryStream = new MemoryStream();
        //    await model.File.CopyToAsync(memoryStream);
        //    memoryStream.Position = 0;

        //    var jsonResult = _dataConverterService.ConvertToJson(memoryStream);

        //    // Parse JSON para obter os dados
        //    var boletoData = System.Text.Json.JsonSerializer.Deserialize<List<Dictionary<string, string>>>(jsonResult);

        //    // Filtrar os tipos VD02 
        //    var boletosInfo = boletoData.Where(b => b.ContainsKey("TIPO_DADO") && b["TIPO_DADO"] == "VA18").ToList();

        //    // Lista para armazenar os arquivos PDF
        //    var pdfFiles = new List<(string FileName, byte[] Data)>();

        //    byte[] pdfData = _boletoService.GerarDocumentoVA18(boletosInfo.FirstOrDefault());
        //    return File(pdfData, "application/pdf", "Boleto.pdf");
        //}

        //[HttpPost("gerar-va24")]
        //public async Task<IActionResult> GerarBoletoVA24([FromForm] FileUploadModel model)
        //{
        //    if (model.File == null || model.File.Length == 0)
        //    {
        //        return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
        //    }

        //    // Converte o arquivo para JSON
        //    using var memoryStream = new MemoryStream();
        //    await model.File.CopyToAsync(memoryStream);
        //    memoryStream.Position = 0;

        //    var jsonResult = _dataConverterService.ConvertToJson(memoryStream);

        //    // Parse JSON para obter os dados
        //    var boletoData = System.Text.Json.JsonSerializer.Deserialize<List<Dictionary<string, string>>>(jsonResult);

        //    // Filtrar os tipos VD02 
        //    var boletosInfo = boletoData.Where(b => b.ContainsKey("TIPO_DADO") && b["TIPO_DADO"] == "VA24").ToList();

        //    // Lista para armazenar os arquivos PDF
        //    var pdfFiles = new List<(string FileName, byte[] Data)>();

        //    byte[] pdfData = _boletoService.GerarDocumentoVA24(boletosInfo.FirstOrDefault());
        //    return File(pdfData, "application/pdf", "Boleto.pdf");
        //}

        //[HttpPost("gerar-vida23")]
        //public async Task<IActionResult> GerarBoletoVIDA23([FromForm] FileUploadModel model)
        //{
        //    if (model.File == null || model.File.Length == 0)
        //    {
        //        return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
        //    }

        //    // Converte o arquivo para JSON
        //    using var memoryStream = new MemoryStream();
        //    await model.File.CopyToAsync(memoryStream);
        //    memoryStream.Position = 0;

        //    var jsonResult = _dataConverterService.ConvertToJson(memoryStream);

        //    // Parse JSON para obter os dados
        //    var boletoData = System.Text.Json.JsonSerializer.Deserialize<List<Dictionary<string, string>>>(jsonResult);

        //    // Filtrar os tipos VD02 
        //    var boletosInfo = boletoData.Where(b => b.ContainsKey("TIPO_DADO") && b["TIPO_DADO"] == "VIDA23").ToList();

        //    // Lista para armazenar os arquivos PDF
        //    var pdfFiles = new List<(string FileName, byte[] Data)>();

        //    byte[] pdfData = _boletoService.GerarBoletoVIDA23Pdf(boletosInfo.FirstOrDefault());
        //    return File(pdfData, "application/pdf", "Boleto.pdf");
        //}

        //[HttpPost("gerar-vida24")]
        //public async Task<IActionResult> GerarBoletoVIDA24([FromForm] FileUploadModel model)
        //{
        //    if (model.File == null || model.File.Length == 0)
        //    {
        //        return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
        //    }

        //    // Converte o arquivo para JSON
        //    using var memoryStream = new MemoryStream();
        //    await model.File.CopyToAsync(memoryStream);
        //    memoryStream.Position = 0;

        //    var jsonResult = _dataConverterService.ConvertToJson(memoryStream);

        //    // Parse JSON para obter os dados
        //    var boletoData = System.Text.Json.JsonSerializer.Deserialize<List<Dictionary<string, string>>>(jsonResult);

        //    // Filtrar os tipos VD02 
        //    var boletosInfo = boletoData.Where(b => b.ContainsKey("TIPO_DADO") && b["TIPO_DADO"] == "VIDA24").ToList();

        //    // Lista para armazenar os arquivos PDF
        //    var pdfFiles = new List<(string FileName, byte[] Data)>();

        //    byte[] pdfData = _boletoService.GerarBoletoVIDA24Pdf(boletosInfo.FirstOrDefault());
        //    return File(pdfData, "application/pdf", "Boleto.pdf");
        //}

        //[HttpPost("gerar-boletov2")]
        //public IActionResult GerarBoletoV2()
        //{
        //    byte[] pdfData = _boletoV2Service.GerarBoletoPdf();
        //    return File(pdfData, "application/pdf", "Boleto.pdf");
        //}
    }
}
