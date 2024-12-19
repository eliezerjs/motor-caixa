using IntegraCVP.Application.Enums;
using IntegraCVP.Application.Interfaces;
using IntegraCVP.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace IntegraCVP.UI.Controllers
{
    [ApiController]
    [Route("api/previdencia")]
    public class PrevidenciaOutrosController : ControllerBase
    {
        private readonly IPrevidenciaOutrosService _PrevidenciaOutrosService;       
        private readonly IImportFileConverterService _dataConverterService;
        public PrevidenciaOutrosController(IPrevidenciaOutrosService PrevidenciaOutrosService,
                                    IImportFileConverterService dataConverterService)
        {
            _PrevidenciaOutrosService = PrevidenciaOutrosService;          
            _dataConverterService = dataConverterService;        
        }

        [HttpPost("gerar-pk11")]
        public async Task<IActionResult> GerarPK11([FromForm] FileUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
            }

            try
            {
                var pdfData = await _PrevidenciaOutrosService.ConverterEGerarPrevidenciaOutrosPdfAsync(model.File, PrevidenciaOutrosType.PK11);
                return File(pdfData, "application/pdf", "PrevidenciaOutros-PK11.pdf");
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

        [HttpPost("gerar-pk15")]
        public async Task<IActionResult> GerarPK15([FromForm] FileUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
            }

            try
            {
                var pdfData = await _PrevidenciaOutrosService.ConverterEGerarPrevidenciaOutrosPdfAsync(model.File, PrevidenciaOutrosType.PK15);
                return File(pdfData, "application/pdf", "PrevidenciaOutros-PK15.pdf");
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

        [HttpPost("gerar-pk35")]
        public async Task<IActionResult> GerarPK35([FromForm] FileUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
            }

            try
            {
                var pdfData = await _PrevidenciaOutrosService.ConverterEGerarPrevidenciaOutrosPdfAsync(model.File, PrevidenciaOutrosType.PK35);
                return File(pdfData, "application/pdf", "PrevidenciaOutros-PK35.pdf");
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

        [HttpPost("gerar-pk37")]
        public async Task<IActionResult> GerarPK37([FromForm] FileUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
            }

            try
            {
                var pdfData = await _PrevidenciaOutrosService.ConverterEGerarPrevidenciaOutrosPdfAsync(model.File, PrevidenciaOutrosType.PK37);
                return File(pdfData, "application/pdf", "PrevidenciaOutros-PK37.pdf");
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

        [HttpPost("gerar-pk44")]
        public async Task<IActionResult> GerarPK44([FromForm] FileUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
            }

            try
            {
                var pdfData = await _PrevidenciaOutrosService.ConverterEGerarPrevidenciaOutrosPdfAsync(model.File, PrevidenciaOutrosType.PK44);
                return File(pdfData, "application/pdf", "PrevidenciaOutros-PK44.pdf");
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

        [HttpPost("gerar-pk48")]
        public async Task<IActionResult> GerarPK48([FromForm] FileUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
            }

            try
            {
                var pdfData = await _PrevidenciaOutrosService.ConverterEGerarPrevidenciaOutrosPdfAsync(model.File, PrevidenciaOutrosType.PK48);
                return File(pdfData, "application/pdf", "PrevidenciaOutros-PK48.pdf");
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

        [HttpPost("gerar-pk49")]
        public async Task<IActionResult> GerarPK49([FromForm] FileUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
            }

            try
            {
                var pdfData = await _PrevidenciaOutrosService.ConverterEGerarPrevidenciaOutrosPdfAsync(model.File, PrevidenciaOutrosType.PK49);
                return File(pdfData, "application/pdf", "PrevidenciaOutros-PK49.pdf");
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

        [HttpPost("gerar-pk53")]
        public async Task<IActionResult> GerarPK53([FromForm] FileUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
            }

            try
            {
                var pdfData = await _PrevidenciaOutrosService.ConverterEGerarPrevidenciaOutrosPdfAsync(model.File, PrevidenciaOutrosType.PK53);
                return File(pdfData, "application/pdf", "PrevidenciaOutros-PK53.pdf");
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
