using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace IntegraCVP.Application.Interfaces
{
    public interface IImportFileConverterService
    {        
        string ConvertToJson(Stream fileStream);

        string ConvertFileToJson(Stream fileStream);
    }
}
