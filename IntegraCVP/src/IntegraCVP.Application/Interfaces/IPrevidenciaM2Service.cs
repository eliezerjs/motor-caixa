using IntegraCVP.Application.Enums;
using Microsoft.AspNetCore.Http;

namespace IntegraCVP.Application.Interfaces
{
    public interface IPrevidenciaM2Service
    {        
        byte[] GerarDocumentoPrevidenciaM2(Dictionary<string, string> dadosBoleto, PrevidenciaM2Type tipo);
        Task<byte[]> ConverterEGerarPrevidenciaM2PdfAsync(IFormFile file, PrevidenciaM2Type tipo);

    }
}
