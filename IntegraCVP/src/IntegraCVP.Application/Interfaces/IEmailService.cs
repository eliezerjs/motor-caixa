using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraCVP.Application.Interfaces
{
    public interface IEmailService
    {
        byte[] GerarEmailVidaExclusivaPdf(Dictionary<string, string> dados, string filename);
        byte[] GerarEmailSegurosPdf(Dictionary<string, string> dados, string filename);
        byte[] GerarEmailSegurosVIDA18Pdf(Dictionary<string, string> dados, string filename);
    }
}
