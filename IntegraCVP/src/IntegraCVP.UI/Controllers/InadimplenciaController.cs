using IntegraCVP.Application.Enums;
using IntegraCVP.Application.Interfaces;
using IntegraCVP.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace IntegraCVP.UI.Controllers
{
    [ApiController]
    [Route("api/inadimplencia")]
    public class InadimplenciaController : ControllerBase
    {
        private readonly IInadimplenciaService _InadimplenciaService;       
        private readonly IImportFileConverterService _dataConverterService;
        public InadimplenciaController(IInadimplenciaService InadimplenciaService,
                                    IImportFileConverterService dataConverterService)
        {
            _InadimplenciaService = InadimplenciaService;          
            _dataConverterService = dataConverterService;        
        }

        [HttpPost("gerar-vd33")]
        public async Task<IActionResult> GerarVD02([FromForm] FileUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
            }

            try
            {
                var pdfData = await _InadimplenciaService.ConverterEGerarInadimplenciaPdfAsync(model.File, InadimplenciaType.VD33);
                return File(pdfData, "application/pdf", "Inadimplencia-VD33.pdf");
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

        [HttpPost("gerar-vida73")]
        public async Task<IActionResult> GerarVIDA73([FromForm] FileUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
            }

            try
            {
                var pdfData = await _InadimplenciaService.ConverterEGerarInadimplenciaPdfAsync(model.File, InadimplenciaType.VIDA73);
                return File(pdfData, "application/pdf", "Inadimplencia-VIDA73.pdf");
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
