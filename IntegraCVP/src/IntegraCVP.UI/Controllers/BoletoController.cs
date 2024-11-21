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
        private readonly ISeguroService _SeguroService;
        private readonly ISeguroGrupoService _SeguroGrupoService;

        public BoletoController(IBoletoService boletoService, IBoletoV2Service boletoV2Service, ISeguroGrupoService SeguroGrupoService, ISeguroService SeguroService)
        {
            _boletoService = boletoService;
            _boletoV2Service = boletoV2Service;
            _SeguroService = SeguroService;
            _SeguroGrupoService = SeguroGrupoService;
        }

        [HttpGet("gerar-boletov2")]
        public IActionResult GerarBoletoV2()
        {
            byte[] pdfData = _boletoV2Service.GerarBoletoPdf();
            return File(pdfData, "application/pdf", "Boleto.pdf");
        }

        [HttpGet("gerar-seguro")]
        public IActionResult GerarSeguro()
        {
            byte[] pdfData = _SeguroService.GerarBoletoPdf();
            return File(pdfData, "application/pdf", "Boleto.pdf");
        }

        [HttpGet("gerar-seguro-grupo")]
        public IActionResult GerarSeguroGrupo()
        {
            byte[] pdfData = _SeguroGrupoService.GerarBoletoPdf();
            return File(pdfData, "application/pdf", "Boleto.pdf");
        }

    }
}
