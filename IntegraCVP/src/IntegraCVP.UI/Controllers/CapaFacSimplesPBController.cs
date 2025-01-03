using IntegraCVP.Application.Enums;
using IntegraCVP.Application.Interfaces.Capas;
using IntegraCVP.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IntegraCVP.UI.Controllers;

[ApiController]
[Route("api/capa-fac-pb")]
public class CapaFacSimplesPBController : ControllerBase
{
    private readonly ICapaFacSimplesPBService _capaFacSimplesPBService;
    private readonly IImportFileConverterService _dataConverterService;
    public CapaFacSimplesPBController(ICapaFacSimplesPBService capaFacSimplesPBService,
                                      IImportFileConverterService dataConverterService)
    {
        _capaFacSimplesPBService = capaFacSimplesPBService;
        _dataConverterService = dataConverterService;
    }

    [HttpPost("gerar-vida01")]
    public async Task<IActionResult> GerarVIDA01([FromForm] FileUploadModel model)
    {
        if (model.File == null || model.File.Length == 0)
        {
            return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
        }

        try
        {
            var pdfData = await _capaFacSimplesPBService.ConverterEGerarPdfAsync(model.File, CapaFacPBType.VIDA01);
            return File(pdfData, "application/pdf", "Capa-fac-pb-vida01.pdf");
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
