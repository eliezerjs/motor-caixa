using IntegraCVP.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace IntegraCVP.UI.Controllers
{
    [ApiController]
    [Route("api/data-converter")]
    public class DataConverterController : ControllerBase
    {
        private readonly IDataConverterService _dataConverterService;

        public DataConverterController(IDataConverterService dataConverterService)
        {
            _dataConverterService = dataConverterService;
        }

        [HttpPost("converter")]
        public async Task<IActionResult> Converter([FromForm] FileUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
            }

            using var memoryStream = new MemoryStream();
            await model.File.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            var jsonResult = _dataConverterService.ConvertToJson(memoryStream);
            return Ok(jsonResult);
        }
    }
}
