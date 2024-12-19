using IntegraCVP.Application.Enums;
using IntegraCVP.Application.Interfaces;
using IntegraCVP.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace IntegraCVP.UI.Controllers
{
    [ApiController]
    [Route("api/previdencia")]
    public class PrevidenciaM3Controller : ControllerBase
    {
        private readonly IPrevidenciaM3Service _PrevidenciaM3Service;       
        private readonly IImportFileConverterService _dataConverterService;
        public PrevidenciaM3Controller(IPrevidenciaM3Service PrevidenciaM3Service,
                                    IImportFileConverterService dataConverterService)
        {
            _PrevidenciaM3Service = PrevidenciaM3Service;          
            _dataConverterService = dataConverterService;        
        }

        [HttpPost("gerar-pk56")]
        public async Task<IActionResult> GerarPK56([FromForm] FileUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
            }

            try
            {
                var pdfData = await _PrevidenciaM3Service.ConverterEGerarPrevidenciaM3PdfAsync(model.File, PrevidenciaM3Type.PK56);
                return File(pdfData, "application/pdf", "PrevidenciaM3-PK56.pdf");
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

        [HttpPost("gerar-pk57")]
        public async Task<IActionResult> GerarPK57([FromForm] FileUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
            }

            try
            {
                var pdfData = await _PrevidenciaM3Service.ConverterEGerarPrevidenciaM3PdfAsync(model.File, PrevidenciaM3Type.PK57);
                return File(pdfData, "application/pdf", "PrevidenciaM3-PK57.pdf");
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

        [HttpPost("gerar-pk58")]
        public async Task<IActionResult> GerarPK58([FromForm] FileUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
            }

            try
            {
                var pdfData = await _PrevidenciaM3Service.ConverterEGerarPrevidenciaM3PdfAsync(model.File, PrevidenciaM3Type.PK58);
                return File(pdfData, "application/pdf", "PrevidenciaM3-PK58.pdf");
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
