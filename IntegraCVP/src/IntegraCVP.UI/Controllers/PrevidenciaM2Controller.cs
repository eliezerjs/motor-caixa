using IntegraCVP.Application.Enums;
using IntegraCVP.Application.Interfaces;
using IntegraCVP.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace IntegraCVP.UI.Controllers
{
    [ApiController]
    [Route("api/previdencia")]
    public class PrevidenciaM2Controller : ControllerBase
    {
        private readonly IPrevidenciaM2Service _PrevidenciaM2Service;       
        private readonly IImportFileConverterService _dataConverterService;
        public PrevidenciaM2Controller(IPrevidenciaM2Service PrevidenciaM2Service,
                                    IImportFileConverterService dataConverterService)
        {
            _PrevidenciaM2Service = PrevidenciaM2Service;          
            _dataConverterService = dataConverterService;        
        }

        [HttpPost("gerar-pk08")]
        public async Task<IActionResult> GerarPK08([FromForm] FileUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
            }

            try
            {
                var pdfData = await _PrevidenciaM2Service.ConverterEGerarPrevidenciaM2PdfAsync(model.File, PrevidenciaM2Type.PK08);
                return File(pdfData, "application/pdf", "PrevidenciaM2-PK08.pdf");
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

        [HttpPost("gerar-pk09")]
        public async Task<IActionResult> GerarPK09([FromForm] FileUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
            }

            try
            {
                var pdfData = await _PrevidenciaM2Service.ConverterEGerarPrevidenciaM2PdfAsync(model.File, PrevidenciaM2Type.PK09);
                return File(pdfData, "application/pdf", "PrevidenciaM2-PK09.pdf");
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

        [HttpPost("gerar-pk10")]
        public async Task<IActionResult> GerarPK10([FromForm] FileUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
            }

            try
            {
                var pdfData = await _PrevidenciaM2Service.ConverterEGerarPrevidenciaM2PdfAsync(model.File, PrevidenciaM2Type.PK10);
                return File(pdfData, "application/pdf", "PrevidenciaM2-PK10.pdf");
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
