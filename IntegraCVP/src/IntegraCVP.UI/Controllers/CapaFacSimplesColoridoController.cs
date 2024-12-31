using IntegraCVP.Application.Enums;
using IntegraCVP.Application.Interfaces;
using IntegraCVP.Application.Interfaces.Capas;
using Microsoft.AspNetCore.Mvc;

namespace IntegraCVP.UI.Controllers;

[ApiController]
[Route("api/capa-fac-colorido")]
public class CapaFacSimplesColoridoController : ControllerBase
{
    private readonly ICapaFacSimplesColoridoService _capaFacSimplesColoridoService;
    private readonly IImportFileConverterService _dataConverterService;
    public CapaFacSimplesColoridoController(ICapaFacSimplesColoridoService capaFacSimplesColoridoService,
                                            IImportFileConverterService dataConverterService)
    {
        _capaFacSimplesColoridoService = capaFacSimplesColoridoService;
        _dataConverterService = dataConverterService;
    }

    [HttpPost("gerar-va44")]
    public async Task<IActionResult> GerarVA44([FromForm] FileUploadModel model)
    {
        if (model.File == null || model.File.Length == 0)
        {
            return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
        }

        try
        {
            var pdfData = await _capaFacSimplesColoridoService.ConverterEGerarPdfAsync(model.File, CapaFacColoridoType.VA44);
            return File(pdfData, "application/pdf", "Capa-fac-colorido-va44.pdf");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao processar a capa: {ex.Message}");
        }
    }
}
