using IntegraCVP.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.IO;

namespace IntegraCVP.UI.Controllers
{
    [ApiController]
    [Route("api/import-prev")]
    public class ImportFilePrevController : ControllerBase
    {
        private readonly IImportFilePrevConverterService _dataConverterService;
        public ImportFilePrevController(IImportFilePrevConverterService dataConverterService)
        {
            _dataConverterService = dataConverterService;
        }

        [HttpPost("gerar-json")]
        public async Task<IActionResult> GerarJSON([FromForm] FileUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
            }

            try
            {
                using var stream = model.File.OpenReadStream();
                var layouts = await _dataConverterService.ProcessDataAsync(stream);

                // Serializa o dicionário formatado como JSON
                var formattedJson = System.Text.Json.JsonSerializer.Serialize(layouts, new System.Text.Json.JsonSerializerOptions
                {
                    WriteIndented = true // Formata o JSON com identação
                });

                return new ContentResult
                {
                    Content = formattedJson,
                    ContentType = "application/json",
                    StatusCode = 200
                };
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao processar o documento: {ex.Message}");
            }
        }
    }
}
