using IntegraCVP.Application.Enums;
using IntegraCVP.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IntegraCVP.UI.Controllers
{
    [ApiController]
    [Route("api/boasvindas")]
    public class BoasVindasController : ControllerBase
    {
        private readonly IBoasVindasService _BoasVindasService;       
        private readonly IImportFileConverterService _dataConverterService;
        public BoasVindasController(IBoasVindasService BoasVindasService,
                                    IImportFileConverterService dataConverterService)
        {
            _BoasVindasService = BoasVindasService;          
            _dataConverterService = dataConverterService;        
        }

        [HttpPost("gerar-vida05")]
        public async Task<IActionResult> GerarVIDA05([FromForm] FileUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
            }

            try
            {
                var pdfData = await _BoasVindasService.ConverterEGerarBoasVindasPdfAsync(model.File, BoasVindasType.VIDA05);
                return File(pdfData, "application/pdf", "BoasVindas-VIDA05.pdf");
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

        [HttpPost("gerar-vida07")]
        public async Task<IActionResult> GerarVIDA07([FromForm] FileUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
            }

            try
            {
                var pdfData = await _BoasVindasService.ConverterEGerarBoasVindasPdfAsync(model.File, BoasVindasType.VIDA07);
                return File(pdfData, "application/pdf", "BoasVindas-VIDA25.pdf");
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
