using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegraCVP.Application.Enums;
using Microsoft.AspNetCore.Http;

namespace IntegraCVP.Application.Interfaces
{
    public interface IEmailService
    {
        async Task<byte[]> ConverterEGerarEmailPdfAsync(IFormFile file, EmailType tipo);
        //byte[] GerarEmailVidaExclusivaPdf(Dictionary<string, string> dados, string filename);
        //byte[] GerarEmailSegurosPdf(Dictionary<string, string> dados, string filename);
        //byte[] GerarEmailSegurosVIDA18Pdf(Dictionary<string, string> dados, string filename);
    }
}
