using IntegraCVP.Application.Enums;
using IntegraCVP.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IntegraCVP.UI.Controllers
{
    [ApiController]
    [Route("api/carta-recusa")]
    public class CartaRecusaController : ControllerBase
    {
        private readonly ICartaRecusaService _CartaRecusaService;       
        private readonly IImportFileConverterService _dataConverterService;
        public CartaRecusaController(ICartaRecusaService CartaRecusaService,
                                    IImportFileConverterService dataConverterService)
        {
            _CartaRecusaService = CartaRecusaService;          
            _dataConverterService = dataConverterService;        
        }

        [HttpPost("gerar-vida01")]
        public async Task<IActionResult> GerarVIDA01([FromForm] FileUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
            }

            try
            {
                var pdfData = await _CartaRecusaService.ConverterEGerarCartaRecusaPdfAsync(model.File, CartaRecusaType.VIDA01);
                return File(pdfData, "application/pdf", "CartaRecusa-VIDA01.pdf");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao processar o documento: {ex.Message}");
            }
        }

        [HttpPost("gerar-vida02")]
        public async Task<IActionResult> GerarVIDA02([FromForm] FileUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
            }

            try
            {
                var pdfData = await _CartaRecusaService.ConverterEGerarCartaRecusaPdfAsync(model.File, CartaRecusaType.VIDA02);
                return File(pdfData, "application/pdf", "CartaRecusa-VIDA02.pdf");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao processar o documento: {ex.Message}");
            }
        }

        [HttpPost("gerar-vida03")]
        public async Task<IActionResult> GerarVIDA03([FromForm] FileUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
            }

            try
            {
                var pdfData = await _CartaRecusaService.ConverterEGerarCartaRecusaPdfAsync(model.File, CartaRecusaType.VIDA03);
                return File(pdfData, "application/pdf", "CartaRecusa-VIDA03.pdf");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao processar o documento: {ex.Message}");
            }
        }

        [HttpPost("gerar-vida04")]
        public async Task<IActionResult> GerarVIDA04([FromForm] FileUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
            }

            try
            {
                var pdfData = await _CartaRecusaService.ConverterEGerarCartaRecusaPdfAsync(model.File, CartaRecusaType.VIDA04);
                return File(pdfData, "application/pdf", "CartaRecusa-VIDA04.pdf");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao processar o documento: {ex.Message}");
            }
        }
    }
}
