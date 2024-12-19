using IntegraCVP.Application.Enums;
using Microsoft.AspNetCore.Http;

namespace IntegraCVP.Application.Interfaces
{
    public interface IPrevidenciaM4Service
    {        
        byte[] GerarDocumentoPrevidenciaM4(Dictionary<string, string> dadosBoleto, PrevidenciaM4Type tipo);
        Task<byte[]> ConverterEGerarPrevidenciaM4PdfAsync(IFormFile file, PrevidenciaM4Type tipo);

    }
}
