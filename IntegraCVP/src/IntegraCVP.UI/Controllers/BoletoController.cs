using IntegraCVP.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IntegraCVP.UI.Controllers
{
    [ApiController]
    [Route("api/boleto")]
    public class BoletoController : ControllerBase
    {
        private readonly IBoletoService _boletoService;

        public BoletoController(IBoletoService boletoService)
        {
            _boletoService = boletoService;
        }

        [HttpGet("gerar")]
        public IActionResult GerarBoleto()
        {
            byte[] pdfData = _boletoService.GerarBoletoPdf();
            return File(pdfData, "application/pdf", "Boleto.pdf");
        }
    }
}
