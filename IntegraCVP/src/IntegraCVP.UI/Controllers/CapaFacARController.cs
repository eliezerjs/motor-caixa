using IntegraCVP.Application.Enums;
using IntegraCVP.Application.Interfaces.Capas;
using IntegraCVP.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IntegraCVP.UI.Controllers;

[ApiController]
[Route("api/capa-fac-ar")]
public class CapaFacARController : ControllerBase
{
    private readonly ICapaFacARService _capaFacARService;
    private readonly IImportFileConverterService _dataConverterService;
    public CapaFacARController(ICapaFacARService capaFacARService,
                               IImportFileConverterService dataConverterService)
    {
        _capaFacARService = capaFacARService;
        _dataConverterService = dataConverterService;
    }

    [HttpPost("gerar-pr02")]
    public async Task<IActionResult> GerarPR02([FromForm] FileUploadModel model)
    {
        if (model.File == null || model.File.Length == 0)
        {
            return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
        }

        try
        {
            var pdfData = await _capaFacARService.ConverterEGerarPdfAsync(model.File, CapaFacARType.PR02);
            return File(pdfData, "application/pdf", "Capa-fac-ar-pr02.pdf");
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