using IntegraCVP.Application.Enums;
using IntegraCVP.Application.Interfaces;
using IntegraCVP.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace IntegraCVP.UI.Controllers
{
    [ApiController]
    [Route("api/boleto-m2")]
    public class BoletoM2Controller : ControllerBase
    {
        private readonly IBoletoM2Service _boletoM2Service;
        private readonly IImportFileConverterService _dataConverterService;
        public BoletoM2Controller(IBoletoM2Service boletoM2Service,
                                    IImportFileConverterService dataConverterService)
        {

            _boletoM2Service = boletoM2Service;
            _dataConverterService = dataConverterService;        
        }

        //[HttpPost("gerar-vd02")]
        //public async Task<IActionResult> GerarVD02([FromForm] FileUploadModel model)
        //{
        //    if (model.File == null || model.File.Length == 0)
        //    {
        //        return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
        //    }

        //    try
        //    {
        //        var pdfData = await _boletoM2Service.ConverterEGerarPdfAsync(model.File, BoletoM1Type.VD02);
        //        return File(pdfData, "application/pdf", "Boleto-VD02.pdf");
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Erro ao processar o boleto: {ex.Message}");
        //    }
        //}

        //[HttpPost("gerar-vida25")]
        //public async Task<IActionResult> GerarVida25([FromForm] FileUploadModel model)
        //{
        //    if (model.File == null || model.File.Length == 0)
        //    {
        //        return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
        //    }

        //    try
        //    {
        //        var pdfData = await _boletoM2Service.ConverterEGerarPdfAsync(model.File, BoletoM1Type.VIDA25);
        //        return File(pdfData, "application/pdf", "Boleto-VIDA25.pdf");
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Erro ao processar o boleto: {ex.Message}");
        //    }
        //}
    }
}
