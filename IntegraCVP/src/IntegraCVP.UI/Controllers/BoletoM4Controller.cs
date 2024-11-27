using IntegraCVP.Application.Enums;
using IntegraCVP.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IntegraCVP.UI.Controllers
{
    [ApiController]
    [Route("api/boleto-m4")]
    public class BoletoM4Controller : ControllerBase
    {
        private readonly IBoletoM4Service _boletoM4Service;

        private readonly IImportFileConverterService _dataConverterService;
        public BoletoM4Controller(IBoletoM4Service boletoM4Service,
                                IImportFileConverterService dataConverterService)
        {
            _boletoM4Service = boletoM4Service;
            _dataConverterService = dataConverterService;        
        }

        [HttpPost("gerar-va18")]
        public async Task<IActionResult> GerarVA18([FromForm] FileUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
            }

            try
            {
                var pdfData = await _boletoM4Service.ConverterEGerarPdfAsync(model.File, BoletoM4Type.VA18);
                return File(pdfData, "application/pdf", "Boleto-VA18.pdf");
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

        [HttpPost("gerar-va24")]
        public async Task<IActionResult> GerarVA24([FromForm] FileUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
            }

            try
            {
                var pdfData = await _boletoM4Service.ConverterEGerarPdfAsync(model.File, BoletoM4Type.VA24);
                return File(pdfData, "application/pdf", "Boleto-VA24.pdf");
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

        [HttpPost("gerar-vida23")]
        public async Task<IActionResult> GerarVIDA23([FromForm] FileUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
            }

            try
            {
                var pdfData = await _boletoM4Service.ConverterEGerarPdfAsync(model.File, BoletoM4Type.VIDA23);
                return File(pdfData, "application/pdf", "Boleto-VIDA23.pdf");
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

        [HttpPost("gerar-vida24")]
        public async Task<IActionResult> GerarVIDA24([FromForm] FileUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
            }

            try
            {
                var pdfData = await _boletoM4Service.ConverterEGerarPdfAsync(model.File, BoletoM4Type.VIDA24);
                return File(pdfData, "application/pdf", "Boleto-VIDA24.pdf");
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
    }
}
