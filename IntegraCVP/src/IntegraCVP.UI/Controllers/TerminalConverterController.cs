using IntegraCVP.Application.Enums;
using IntegraCVP.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.IO.Compression;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Text;

namespace IntegraCVP.UI.Controllers
{
    [ApiController]
    [Route("api/data-converter")]
    public class TerminalConverterController : ControllerBase
    {
        private readonly ITerminalConverterService _terminalConverterService;
        
        public TerminalConverterController(ITerminalConverterService terminalConverterService)
        {
            _terminalConverterService = terminalConverterService;
        }

        [HttpPost("converter-e-gerar")]
        public async Task<IActionResult> ConverterEGerar([FromForm] FileUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
            }

            try
            {
                var zipFile = await _terminalConverterService.ConverterEGerarZipAsync(model.File);
                return File(zipFile, "application/zip", "VIDA.zip");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao processar os dados: {ex.Message}");
            }
        }

        [HttpPost("converter-prestamista")]
        public async Task<IActionResult> ConverterPrestamista([FromForm] FileUploadModel model)
        {
            if (model.File == null || model.File.Length == 0)
            {
                return BadRequest("Nenhum arquivo foi enviado ou o arquivo está vazio.");
            }

            try
            {
                // Converte o arquivo em JSON formatado
                var jsonResult = await ConvertMultiLineJsonFileToJsonAsync(model.File.OpenReadStream());

                // Retorna o JSON diretamente como resposta
                return new ContentResult
                {
                    Content = jsonResult,
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
                return StatusCode(500, $"Erro ao processar os dados: {ex.Message}");
            }
        }

        private async Task<string> ConvertMultiLineJsonFileToJsonAsync(Stream dataStream)
        {
            var consolidatedRecords = new List<object>();
            var buffer = new StringBuilder();

            using (var reader = new StreamReader(dataStream, Encoding.GetEncoding("ISO-8859-1"), true, 8192))
            {
                string line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    buffer.AppendLine(line);

                    if (buffer.Length > 4096) // Processar linhas em blocos
                    {
                        ProcessBuffer(buffer, consolidatedRecords);
                    }
                }

                // Processa o restante no buffer
                if (buffer.Length > 0)
                {
                    ProcessBuffer(buffer, consolidatedRecords);
                }
            }

            // Serializa os registros consolidados para JSON formatado
            var jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true, // JSON legível
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull // Ignorar valores nulos
            };

            return JsonSerializer.Serialize(consolidatedRecords, jsonOptions);
        }

        private void ProcessBuffer(StringBuilder buffer, List<object> consolidatedRecords)
        {
            var lines = buffer.ToString().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            buffer.Clear();

            Parallel.ForEach(lines, line =>
            {
                try
                {
                    var record = JsonSerializer.Deserialize<object>(line);
                    lock (consolidatedRecords) // Lock necessário para evitar race conditions
                    {
                        consolidatedRecords.Add(record);
                    }
                }
                catch (JsonException)
                {
                    // Ignorar linhas inválidas
                }
            });
        }



    }
}
