using IntegraCVP.Application.Enums;
using IntegraCVP.Application.Interfaces;
using IntegraCVP.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace IntegraCVP.UI.Controllers
{
    [ApiController]
    [Route("api/Prestamista")]
    public class PrestamistaController : ControllerBase
    {
        private readonly IPrestamistaService _PrestamistaService;       
        private readonly IImportFileConverterService _importFileConverterService;
        public PrestamistaController(IPrestamistaService PrestamistaService,
                                    IImportFileConverterService dataConverterService)
        {
            _PrestamistaService = PrestamistaService;          
            _importFileConverterService = dataConverterService;        
        }

        [HttpPost("gerar-prest01")]
        public async Task<IActionResult> GerarPrest01([FromForm] FileUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
            }

            try
            {
                var pdfData = await _PrestamistaService.ConverterEGerarPrestamistaPdfAsync(model.File, PrestamistaType.PREST01);
                return File(pdfData, "application/pdf", "Prestamista-PREST01.pdf");
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
