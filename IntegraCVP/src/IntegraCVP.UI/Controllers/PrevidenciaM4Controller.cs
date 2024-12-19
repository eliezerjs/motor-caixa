using IntegraCVP.Application.Enums;
using IntegraCVP.Application.Interfaces;
using IntegraCVP.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace IntegraCVP.UI.Controllers
{
    [ApiController]
    [Route("api/previdencia")]
    public class PrevidenciaM4Controller : ControllerBase
    {
        private readonly IPrevidenciaM4Service _PrevidenciaM4Service;       
        private readonly IImportFileConverterService _dataConverterService;
        public PrevidenciaM4Controller(IPrevidenciaM4Service PrevidenciaM4Service,
                                    IImportFileConverterService dataConverterService)
        {
            _PrevidenciaM4Service = PrevidenciaM4Service;          
            _dataConverterService = dataConverterService;        
        }

        [HttpPost("gerar-pk05")]
        public async Task<IActionResult> GerarPK05([FromForm] FileUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
            }

            try
            {
                var pdfData = await _PrevidenciaM4Service.ConverterEGerarPrevidenciaM4PdfAsync(model.File, PrevidenciaM4Type.PK05);
                return File(pdfData, "application/pdf", "PrevidenciaM4-PK05.pdf");
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

        [HttpPost("gerar-pk06")]
        public async Task<IActionResult> GerarPK06([FromForm] FileUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
            }

            try
            {
                var pdfData = await _PrevidenciaM4Service.ConverterEGerarPrevidenciaM4PdfAsync(model.File, PrevidenciaM4Type.PK06);
                return File(pdfData, "application/pdf", "PrevidenciaM4-PK06.pdf");
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
