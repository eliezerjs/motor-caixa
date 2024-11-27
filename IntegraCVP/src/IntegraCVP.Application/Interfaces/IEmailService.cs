using IntegraCVP.Application.Enums;
using Microsoft.AspNetCore.Http;

namespace IntegraCVP.Application.Interfaces
{
    public interface IEmailService
    {
        byte[] GerarDocumentoEmail(Dictionary<string, string> dadosBoleto, EmailType tipo);
        Task<byte[]> ConverterEGerarEmailPdfAsync(IFormFile file, EmailType tipo);
    }
}
