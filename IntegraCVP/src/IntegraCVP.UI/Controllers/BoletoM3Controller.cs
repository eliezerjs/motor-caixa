using IntegraCVP.Application.Enums;
using IntegraCVP.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IntegraCVP.UI.Controllers
{
    [ApiController]
    [Route("api/boleto-m2")]
    public class BoletoM3Controller : ControllerBase
    {
        private readonly IBoletoM3Service _boletoM3Service;

        private readonly IImportFileConverterService _dataConverterService;
        public BoletoM3Controller(IBoletoM3Service boletoM3Service,
                                
                                IImportFileConverterService dataConverterService)
        {
            _boletoM3Service = boletoM3Service;
            _dataConverterService = dataConverterService;        
        }

        [HttpPost("gerar-vd03")]
        public async Task<IActionResult> GerarVD03([FromForm] FileUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
            }

            try
            {
                var pdfData = await _boletoM3Service.ConverterEGerarPdfAsync(model.File, BoletoM3Type.VD03);
                return File(pdfData, "application/pdf", "Boleto-VD03.pdf");
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

        [HttpPost("gerar-vida27")]
        public async Task<IActionResult> GerarVIDA27([FromForm] FileUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
            }

            try
            {
                var pdfData = await _boletoM3Service.ConverterEGerarPdfAsync(model.File, BoletoM3Type.VIDA27);
                return File(pdfData, "application/pdf", "Boleto-VIDA27.pdf");
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
