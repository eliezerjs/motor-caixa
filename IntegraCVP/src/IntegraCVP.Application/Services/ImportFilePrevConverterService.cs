using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using IntegraCVP.Application.Interfaces;

namespace IntegraCVP.Application.Services
{
    public partial class ImportFilePrevConverterService: IImportFilePrevConverterService
    {
        public async Task<List<Dictionary<string, string>>> ProcessDataAsync(Stream dataStream)
        {
            var result = new List<Dictionary<string, string>>();

            using (var reader = new StreamReader(dataStream))
            {
                string line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    if (line.Length < 2)
                        continue;

                    string recordType = line.Substring(0, 2); // Determina o tipo de registro
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
                        result.Add(record);
                    }
                }
            }

            return result;
        }
    }
}
