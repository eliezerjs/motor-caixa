using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraCVP.Application.Interfaces
{
    public interface IDataConverterService
    {
        string ConvertToJson(Stream fileStream);
    }
}
