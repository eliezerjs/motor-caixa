using IntegraCVP.Application.Enums;
using IntegraCVP.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IntegraCVP.UI.Controllers
{
    [ApiController]
    [Route("api/boleto-m1")]
    public class BoletoM1Controller : ControllerBase
    {
        private readonly IBoletoM1Service _boletoM1Service;       
        private readonly IImportFileConverterService _dataConverterService;
        public BoletoM1Controller(IBoletoM1Service boletoM1Service,
                                    IImportFileConverterService dataConverterService)
        {
            _boletoM1Service = boletoM1Service;          
            _dataConverterService = dataConverterService;        
        }

        [HttpPost("gerar-vd02")]
        public async Task<IActionResult> GerarVD02([FromForm] FileUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado ou o arquivo est� vazio.");
            }

            try
            {
                var pdfData = await _boletoM1Service.ConverterEGerarPdfAsync(model.File, BoletoM1Type.VD02);
                return File(pdfData, "application/pdf", "Boleto-VD02.pdf");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao processar o boleto: {ex.Message}");
            }
        }

        [HttpPost("gerar-vida25")]
        public async Task<IActionResult> GerarVida25([FromForm] FileUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado ou o arquivo est� vazio.");
            }

            try
            {
                var pdfData = await _boletoM1Service.ConverterEGerarPdfAsync(model.File, BoletoM1Type.VIDA25);
                return File(pdfData, "application/pdf", "Boleto-VIDA25.pdf");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao processar o boleto: {ex.Message}");
            }
        }        
    }
}
