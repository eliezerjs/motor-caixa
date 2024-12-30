using IntegraCVP.Application.Enums;
using IntegraCVP.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

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
                    ContentType = "application/json; charset=utf-8",
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

            using (var reader = new StreamReader(dataStream, Encoding.GetEncoding("ISO-8859-1")))
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

                if (buffer.Length > 0)
                {
                    ProcessBuffer(buffer, consolidatedRecords);
                }
            }

            var jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
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
                    if (record != null)
                    {
                        lock (consolidatedRecords)
                        {
                            consolidatedRecords.Add(record);
                        }
                    }
                }
                catch (JsonException)
                {
                    // Ignorar linhas inválidas
                }
            });
        }

        [Produces("application/json")]
        [HttpGet("buscar-arquivo-por-numero-linhas")]
        public IActionResult BuscarArquivoPorNumeroNasLinhas([FromQuery] string directoryPath, [FromQuery] string numero)
        {
            if (string.IsNullOrWhiteSpace(directoryPath) || string.IsNullOrWhiteSpace(numero) || numero.Length != 2)
            {
                return BadRequest("Caminho do diretório ou número inválido.");
            }

            try
            {
                if (!Directory.Exists(directoryPath))
                {
                    return NotFound("O diretório especificado não existe.");
                }

                var matchingFiles = new List<string>();

                foreach (var filePath in Directory.GetFiles(directoryPath))
                {
                    try
                    {
                        using var reader = new StreamReader(filePath);

                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (line.StartsWith(numero))
                            {
                                matchingFiles.Add(Path.GetFileName(filePath));
                                break; // Encontrou uma linha correspondente, pode parar de ler o arquivo
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Continuar mesmo se um arquivo falhar
                        Console.WriteLine($"Erro ao ler o arquivo {filePath}: {ex.Message}");
                    }
                }

                if (matchingFiles.Any())
                {
                    return Ok(new { Message = matchingFiles.FirstOrDefault()});
                }
                else
                {
                    return Ok(new { Message = "Não Encontrado" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao processar os arquivos: {ex.Message}");
            }
        }

    }





}
