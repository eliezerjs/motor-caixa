using System;
using System.Collections.Generic;
using System.IO;
using IntegraCVP.Application.Interfaces;
using Newtonsoft.Json;

namespace IntegraCVP.Application.Services
{
    public class DataConverterService : IDataConverterService
    {
        public string ConvertToJson(Stream fileStream)
        {
            var sections = new List<Dictionary<string, string>>();
            Dictionary<string, string> currentSection = null;
            List<string> headers = null;

            using var reader = new StreamReader(fileStream);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (line.Contains("STARTDBM")) // Identifica o início de uma nova seção
                {
                    if (currentSection != null)
                    {
                        sections.Add(currentSection);
                    }
                    currentSection = new Dictionary<string, string>();
                    headers = null; // Reinicia os cabeçalhos para a nova seção
                }
                else if (line.Contains("|") && headers == null) // Captura os cabeçalhos apenas uma vez
                {
                    headers = new List<string>(line.Split('|', StringSplitOptions.RemoveEmptyEntries));
                }
                else if (currentSection != null && headers != null && line.Contains("|")) // Processa linhas de dados
                {
                    var values = line.Split('|', StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < headers.Count; i++)
                    {
                        if (i < values.Length)
                        {
                            string key = headers[i].Trim();
                            string value = values[i].Trim();

                            if (!string.IsNullOrEmpty(key))
                            {
                                currentSection[key] = value;
                            }
                        }
                    }
                }
            }

            if (currentSection != null)
            {
                sections.Add(currentSection);
            }

            return JsonConvert.SerializeObject(sections, Formatting.Indented);
        }
    }
}
