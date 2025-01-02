using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using IntegraCVP.Application.Interfaces;

namespace IntegraCVP.Application.Services
{
    public partial class ImportFilePrevConverterService : IImportFilePrevConverterService
    {
        public async Task<List<Dictionary<string, string>>> ProcessDataAsync(Stream dataStream)
        {
            var resultadoAgrupado = new Dictionary<string, Dictionary<string, string>>();

            using (var reader = new StreamReader(dataStream, Encoding.GetEncoding("ISO-8859-1")))
            {
                string line;

                while ((line = await reader.ReadLineAsync()) != null)
                {
                    if (line.Length < 2)
                        continue;

                    string recordType = line.Substring(0, 2);

                    if (Layouts.TryGetValue(recordType, out var layout))
                    {
                        var record = new Dictionary<string, string>();

                        foreach (var field in layout)
                        {
                            if (line.Length >= field.Offset + field.Size - 1)
                            {
                                record[field.Name] = line.Substring(field.Offset - 1, field.Size).Trim();
                            }
                            else
                            {
                                record[field.Name] = string.Empty;
                            }
                        }

                        record["RecordType"] = recordType;

                        // Extrair o valor de NO_CERTIFICADO com base na posição e tamanho definidos
                        string noCertificado = line.Length >= 3 + 15 - 1
                            ? line.Substring(3 - 1, 15).Trim()
                            : string.Empty;

                        if (!string.IsNullOrWhiteSpace(noCertificado))
                        {
                            if (!resultadoAgrupado.ContainsKey(noCertificado))
                            {
                                resultadoAgrupado[noCertificado] = new Dictionary<string, string>();
                            }

                            foreach (var campo in record)
                            {
                                if (!resultadoAgrupado[noCertificado].ContainsKey(campo.Key))
                                {
                                    resultadoAgrupado[noCertificado][campo.Key] = campo.Value;
                                }
                            }
                        }
                    }
                }
            }

            return resultadoAgrupado.Values.ToList();
        }
    }
}
