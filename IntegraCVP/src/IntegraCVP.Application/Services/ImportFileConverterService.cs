using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using IntegraCVP.Application.Interfaces;
using Newtonsoft.Json;

namespace IntegraCVP.Application.Services
{
    public class ImportFileConverterService : IImportFileConverterService
    {
        public string ConvertToJson(Stream fileStream)
        {
            var sections = new List<Dictionary<string, string>>();
            Dictionary<string, string> currentSection = null;
            List<string> headers = null;
            string tipoDadoAtual = null; // Tipo de dado atual

            using var reader = new StreamReader(fileStream);
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                // Ignora linhas irrelevantes
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("%%") || line.StartsWith("%!"))
                {
                    continue;
                }

                // Detecta o TIPO_DADO
                if ((line.Contains(" - ") && line.ToUpper().Contains("BOLETO")) || line.Contains("VIDA02") || line.Contains("VD33") || line.Contains("VIDA05") || line.Contains("VD08") || line.Contains("VD09") || line.Contains("VIDA17") || line.Contains("VIDA18"))
                {
                    tipoDadoAtual = line.Split(" - ", StringSplitOptions.RemoveEmptyEntries).FirstOrDefault()?.Trim();
                }
                else if (line.Contains("STARTDBM")) // Início de uma nova seção
                {
                    if (currentSection != null)
                    {
                        sections.Add(currentSection);
                    }

                    currentSection = new Dictionary<string, string>
                    {
                        { "TIPO_DADO", tipoDadoAtual }
                    };

                    headers = null; // Reinicia os cabeçalhos
                }
                else if (line.Contains("|") && headers == null) // Captura os cabeçalhos
                {
                    headers = new List<string>(line.Split('|', StringSplitOptions.RemoveEmptyEntries));
                }
                else if (currentSection != null && headers != null && line.Contains("|"))
                {
                    // Processa as linhas de dados
                    var values = line.Split('|', StringSplitOptions.None);

                    for (int i = 0; i < headers.Count; i++)
                    {
                        if (i < values.Length)
                        {
                            var key = headers[i].Trim();
                            var value = values[i].Trim();

                            // Ignora valores inválidos (e.g., ") SETDBSEP")
                            if (!string.IsNullOrEmpty(key) && !value.StartsWith(")") && value != "SETDBSEP")
                            {
                                currentSection[key] = value;
                            }
                        }
                    }
                }
            }

            // Adiciona a última seção processada
            if (currentSection != null)
            {
                sections.Add(currentSection);
            }

            // Retorna o JSON gerado
            return JsonConvert.SerializeObject(sections, Formatting.Indented);
        }

    }
}
