using IntegraCVP.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IntegraCVP.UI.Controllers
{
    [ApiController]
    [Route("api/boleto")]
    public class BoletoController : ControllerBase
    {
        private readonly IBoletoService _boletoService;
        private readonly IBoletoV2Service _boletoV2Service;


        public BoletoController(IBoletoService boletoService, IBoletoV2Service boletoV2Service)
        {
            _boletoService = boletoService;
            _boletoV2Service = boletoV2Service;

        }

        [HttpGet("gerar-boletov2")]
        public IActionResult GerarBoletoV2()
        {
            byte[] pdfData = _boletoV2Service.GerarBoletoPdf();
            return File(pdfData, "application/pdf", "Boleto.pdf");
        }


    }
}
