using IntegraCVP.Application.Interfaces;
using IntegraCVP.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace IntegraCVP.UI.Controllers
{
    [ApiController]
    [Route("api/boleto")]
    public class BoletoController : ControllerBase
    {
        private readonly IBoletoService _boletoService;
        private readonly IBoletoV2Service _boletoV2Service;

        private readonly IDataConverterService _dataConverterService;
        public BoletoController(IBoletoService boletoService, IBoletoV2Service boletoV2Service, IDataConverterService dataConverterService)
        {
            _boletoService = boletoService;
            _dataConverterService = dataConverterService;
        
        }

        [HttpPost("gerar-vd33")]
        public async Task<IActionResult> GerarBoletoVD33([FromForm] FileUploadModel model)
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

            // Filtrar os tipos VD33
            var boletosInfo = boletoData.Where(b => b.ContainsKey("TIPO_DADO") && b["TIPO_DADO"] == "VD33").ToList();

            // Lista para armazenar os arquivos PDF
            var pdfFiles = new List<(string FileName, byte[] Data)>();

            byte[] pdfData = _boletoService.GerarBoletoVD33Pdf(boletosInfo.FirstOrDefault());
            return File(pdfData, "application/pdf", "Boleto.pdf");
        }

        [HttpPost("gerar-vida02")]
        public async Task<IActionResult> GerarBoletoVIDA02([FromForm] FileUploadModel model)
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

            // Filtrar os tipos VD02 
            var boletosInfo = boletoData.Where(b => b.ContainsKey("TIPO_DADO") && b["TIPO_DADO"] == "VIDA02").ToList();

            // Lista para armazenar os arquivos PDF
            var pdfFiles = new List<(string FileName, byte[] Data)>();

            byte[] pdfData = _boletoService.GerarBoletoVIDA02Pdf(boletosInfo.FirstOrDefault());
            return File(pdfData, "application/pdf", "Boleto.pdf");
        }


        [HttpPost("gerar-vd02")]
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
            // Filtrar os tipos VD02 
            var boletosVD02 = boletoData.Where(b => b.ContainsKey("TIPO_DADO") && b["TIPO_DADO"] == "VD02").ToList();
            
            // Lista para armazenar os arquivos PDF
            var pdfFiles = new List<(string FileName, byte[] Data)>();

            byte[] pdfData = _boletoService.GerarBoletoPdf(boletosVD02.FirstOrDefault(), "VD02");
            return File(pdfData, "application/pdf", "Boleto.pdf");
        }

        [HttpPost("gerar-va18")]
        public async Task<IActionResult> GerarBoletoVA18([FromForm] FileUploadModel model)
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

            // Filtrar os tipos VD02 
            var boletosInfo = boletoData.Where(b => b.ContainsKey("TIPO_DADO") && b["TIPO_DADO"] == "VA18").ToList();

            // Lista para armazenar os arquivos PDF
            var pdfFiles = new List<(string FileName, byte[] Data)>();

            byte[] pdfData = _boletoService.GerarBoletoVA18Pdf(boletosInfo.FirstOrDefault());
            return File(pdfData, "application/pdf", "Boleto.pdf");
        }

        [HttpPost("gerar-va24")]
        public async Task<IActionResult> GerarBoletoVA24([FromForm] FileUploadModel model)
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

            // Filtrar os tipos VD02 
            var boletosInfo = boletoData.Where(b => b.ContainsKey("TIPO_DADO") && b["TIPO_DADO"] == "VA24").ToList();

            // Lista para armazenar os arquivos PDF
            var pdfFiles = new List<(string FileName, byte[] Data)>();

            byte[] pdfData = _boletoService.GerarBoletoVA24Pdf(boletosInfo.FirstOrDefault());
            return File(pdfData, "application/pdf", "Boleto.pdf");
        }

        [HttpPost("gerar-vida23")]
        public async Task<IActionResult> GerarBoletoVIDA23([FromForm] FileUploadModel model)
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

            // Filtrar os tipos VD02 
            var boletosInfo = boletoData.Where(b => b.ContainsKey("TIPO_DADO") && b["TIPO_DADO"] == "VIDA23").ToList();

            // Lista para armazenar os arquivos PDF
            var pdfFiles = new List<(string FileName, byte[] Data)>();

            byte[] pdfData = _boletoService.GerarBoletoVIDA23Pdf(boletosInfo.FirstOrDefault());
            return File(pdfData, "application/pdf", "Boleto.pdf");
        }

        [HttpPost("gerar-vida24")]
        public async Task<IActionResult> GerarBoletoVIDA24([FromForm] FileUploadModel model)
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

            // Filtrar os tipos VD02 
            var boletosInfo = boletoData.Where(b => b.ContainsKey("TIPO_DADO") && b["TIPO_DADO"] == "VIDA24").ToList();

            // Lista para armazenar os arquivos PDF
            var pdfFiles = new List<(string FileName, byte[] Data)>();

            byte[] pdfData = _boletoService.GerarBoletoVIDA24Pdf(boletosInfo.FirstOrDefault());
            return File(pdfData, "application/pdf", "Boleto.pdf");
        }

        [HttpPost("gerar-boletov2")]
        public IActionResult GerarBoletoV2()
        {
            byte[] pdfData = _boletoV2Service.GerarBoletoPdf();
            return File(pdfData, "application/pdf", "Boleto.pdf");
        }
    }
}
