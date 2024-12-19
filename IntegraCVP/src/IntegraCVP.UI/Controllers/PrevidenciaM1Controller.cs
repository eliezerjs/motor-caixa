using IntegraCVP.Application.Enums;
using IntegraCVP.Application.Interfaces;
using IntegraCVP.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace IntegraCVP.UI.Controllers
{
    [ApiController]
    [Route("api/previdencia")]
    public class PrevidenciaM1Controller : ControllerBase
    {
        private readonly IPrevidenciaM1Service _PrevidenciaM1Service;       
        private readonly IImportFileConverterService _dataConverterService;
        public PrevidenciaM1Controller(IPrevidenciaM1Service PrevidenciaM1Service,
                                    IImportFileConverterService dataConverterService)
        {
            _PrevidenciaM1Service = PrevidenciaM1Service;          
            _dataConverterService = dataConverterService;        
        }

        [HttpPost("gerar-pk28")]
        public async Task<IActionResult> GerarPK28([FromForm] FileUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
            }

            try
            {
                var pdfData = await _PrevidenciaM1Service.ConverterEGerarPrevidenciaM1PdfAsync(model.File, PrevidenciaM1Type.PK28);
                return File(pdfData, "application/pdf", "PrevidenciaM1-PK28.pdf");
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

        [HttpPost("gerar-pk29")]
        public async Task<IActionResult> GerarPK29([FromForm] FileUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
            }

            try
            {
                var pdfData = await _PrevidenciaM1Service.ConverterEGerarPrevidenciaM1PdfAsync(model.File, PrevidenciaM1Type.PK29);
                return File(pdfData, "application/pdf", "PrevidenciaM1-PK29.pdf");
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

        [HttpPost("gerar-pk30")]
        public async Task<IActionResult> GerarPK30([FromForm] FileUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
            }

            try
            {
                var pdfData = await _PrevidenciaM1Service.ConverterEGerarPrevidenciaM1PdfAsync(model.File, PrevidenciaM1Type.PK30);
                return File(pdfData, "application/pdf", "PrevidenciaM1-PK30.pdf");
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

        [HttpPost("gerar-pk31")]
        public async Task<IActionResult> GerarPK31([FromForm] FileUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
            }

            try
            {
                var pdfData = await _PrevidenciaM1Service.ConverterEGerarPrevidenciaM1PdfAsync(model.File, PrevidenciaM1Type.PK31);
                return File(pdfData, "application/pdf", "PrevidenciaM1-PK31.pdf");
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

        [HttpPost("gerar-pk32")]
        public async Task<IActionResult> GerarPK32([FromForm] FileUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
            }

            try
            {
                var pdfData = await _PrevidenciaM1Service.ConverterEGerarPrevidenciaM1PdfAsync(model.File, PrevidenciaM1Type.PK32);
                return File(pdfData, "application/pdf", "PrevidenciaM1-PK32.pdf");
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

        [HttpPost("gerar-pk33")]
        public async Task<IActionResult> GerarPK33([FromForm] FileUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
            }

            try
            {
                var pdfData = await _PrevidenciaM1Service.ConverterEGerarPrevidenciaM1PdfAsync(model.File, PrevidenciaM1Type.PK33);
                return File(pdfData, "application/pdf", "PrevidenciaM1-PK33.pdf");
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

        [HttpPost("gerar-pk34")]
        public async Task<IActionResult> GerarPK34([FromForm] FileUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
            }

            try
            {
                var pdfData = await _PrevidenciaM1Service.ConverterEGerarPrevidenciaM1PdfAsync(model.File, PrevidenciaM1Type.PK34);
                return File(pdfData, "application/pdf", "PrevidenciaM1-PK34.pdf");
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

        [HttpPost("gerar-pk36")]
        public async Task<IActionResult> GerarPK36([FromForm] FileUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
            }

            try
            {
                var pdfData = await _PrevidenciaM1Service.ConverterEGerarPrevidenciaM1PdfAsync(model.File, PrevidenciaM1Type.PK36);
                return File(pdfData, "application/pdf", "PrevidenciaM1-PK36.pdf");
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

        [HttpPost("gerar-pk47")]
        public async Task<IActionResult> GerarPK47([FromForm] FileUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
            }

            try
            {
                var pdfData = await _PrevidenciaM1Service.ConverterEGerarPrevidenciaM1PdfAsync(model.File, PrevidenciaM1Type.PK47);
                return File(pdfData, "application/pdf", "PrevidenciaM1-PK47.pdf");
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
