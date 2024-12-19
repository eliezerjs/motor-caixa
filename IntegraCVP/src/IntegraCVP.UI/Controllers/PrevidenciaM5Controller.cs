using IntegraCVP.Application.Enums;
using IntegraCVP.Application.Interfaces;
using IntegraCVP.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace IntegraCVP.UI.Controllers
{
    [ApiController]
    [Route("api/previdencia")]
    public class PrevidenciaM5Controller : ControllerBase
    {
        private readonly IPrevidenciaM5Service _PrevidenciaM5Service;       
        private readonly IImportFileConverterService _dataConverterService;
        public PrevidenciaM5Controller(IPrevidenciaM5Service PrevidenciaM5Service,
                                    IImportFileConverterService dataConverterService)
        {
            _PrevidenciaM5Service = PrevidenciaM5Service;          
            _dataConverterService = dataConverterService;        
        }

        [HttpPost("gerar-pk12")]
        public async Task<IActionResult> GerarPK12([FromForm] FileUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
            }

            try
            {
                var pdfData = await _PrevidenciaM5Service.ConverterEGerarPrevidenciaM5PdfAsync(model.File, PrevidenciaM5Type.PK12);
                return File(pdfData, "application/pdf", "PrevidenciaM5-PK12.pdf");
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

        [HttpPost("gerar-pk13")]
        public async Task<IActionResult> GerarPK13([FromForm] FileUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
            }

            try
            {
                var pdfData = await _PrevidenciaM5Service.ConverterEGerarPrevidenciaM5PdfAsync(model.File, PrevidenciaM5Type.PK13);
                return File(pdfData, "application/pdf", "PrevidenciaM5-PK13.pdf");
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

        [HttpPost("gerar-pk14")]
        public async Task<IActionResult> GerarPK14([FromForm] FileUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
            }

            try
            {
                var pdfData = await _PrevidenciaM5Service.ConverterEGerarPrevidenciaM5PdfAsync(model.File, PrevidenciaM5Type.PK14);
                return File(pdfData, "application/pdf", "PrevidenciaM5-PK14.pdf");
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
