using IntegraCVP.Application.Enums;
using IntegraCVP.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.IO.Compression;

namespace IntegraCVP.UI.Controllers
{
    [ApiController]
    [Route("api/data-converter")]
    public class TerminalConverterController : ControllerBase
    {
        private readonly ITerminalConverterService _terminalConverterService;
        
        public TerminalConverterController(ITerminalConverterService terminalConverterService)
        {
            _terminalConverterService = terminalConverterService;
        }

        [HttpPost("converter-e-gerar")]
        public async Task<IActionResult> ConverterEGerar([FromForm] FileUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
            }

            try
            {
                var zipFile = await _terminalConverterService.ConverterEGerarZipAsync(model.File);
                return File(zipFile, "application/zip", "VIDA.zip");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao processar os dados: {ex.Message}");
            }
        }
    }
}
