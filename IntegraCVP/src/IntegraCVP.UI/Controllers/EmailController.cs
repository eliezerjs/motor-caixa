using IntegraCVP.Application.Enums;
using IntegraCVP.Application.Interfaces;
using IntegraCVP.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace IntegraCVP.UI.Controllers
{
    [ApiController]
    [Route("api/email-vida")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _EmailService;
       
        private readonly IImportFileConverterService _dataConverterService;
        public EmailController(IEmailService EmailService,
                               IImportFileConverterService dataConverterService)
        {
            _EmailService = EmailService;
            _dataConverterService = dataConverterService;        
        }

        [HttpPost("gerar-vd08")]
        public async Task<IActionResult> GerarVD08([FromForm] FileUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
            }

            try
            {
                var pdfData = await _EmailService.ConverterEGerarEmailPdfAsync(model.File, EmailType.VD08);
                return File(pdfData, "application/pdf", "Email-VD08.pdf");
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

        [HttpPost("gerar-vd09")]
        public async Task<IActionResult> GerarVD09([FromForm] FileUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
            }

            try
            {
                var pdfData = await _EmailService.ConverterEGerarEmailPdfAsync(model.File, EmailType.VD09);
                return File(pdfData, "application/pdf", "Email-VD09.pdf");
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

        [HttpPost("gerar-vida17")]
        public async Task<IActionResult> GerarVIDA17([FromForm] FileUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
            }

            try
            {
                var pdfData = await _EmailService.ConverterEGerarEmailPdfAsync(model.File, EmailType.VIDA17);
                return File(pdfData, "application/pdf", "Email-VIDA17.pdf");
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

        [HttpPost("gerar-vida18")]
        public async Task<IActionResult> GerarVIDA18([FromForm] FileUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
            }

            try
            {
                var pdfData = await _EmailService.ConverterEGerarEmailPdfAsync(model.File, EmailType.VIDA18);
                return File(pdfData, "application/pdf", "Email-VIDA18.pdf");
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
