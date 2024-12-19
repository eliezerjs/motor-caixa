using IntegraCVP.Application.Enums;
using IntegraCVP.Application.Interfaces;
using IntegraCVP.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace IntegraCVP.UI.Controllers
{
    [ApiController]
    [Route("api/previdencia")]
    public class PrevidenciaM6Controller : ControllerBase
    {
        private readonly IPrevidenciaM6Service _PrevidenciaM6Service;       
        private readonly IImportFileConverterService _dataConverterService;
        public PrevidenciaM6Controller(IPrevidenciaM6Service PrevidenciaM6Service,
                                    IImportFileConverterService dataConverterService)
        {
            _PrevidenciaM6Service = PrevidenciaM6Service;          
            _dataConverterService = dataConverterService;        
        }

        [HttpPost("gerar-pk17")]
        public async Task<IActionResult> GerarPK17([FromForm] FileUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
            }

            try
            {
                var pdfData = await _PrevidenciaM6Service.ConverterEGerarPrevidenciaM6PdfAsync(model.File, PrevidenciaM6Type.PK17);
                return File(pdfData, "application/pdf", "PrevidenciaM6-PK17.pdf");
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

        [HttpPost("gerar-pk21")]
        public async Task<IActionResult> GerarPK21([FromForm] FileUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
            }

            try
            {
                var pdfData = await _PrevidenciaM6Service.ConverterEGerarPrevidenciaM6PdfAsync(model.File, PrevidenciaM6Type.PK21);
                return File(pdfData, "application/pdf", "PrevidenciaM6-PK21.pdf");
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
