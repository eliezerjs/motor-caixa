using IntegraCVP.Application.Enums;
using Microsoft.AspNetCore.Http;

namespace IntegraCVP.Application.Interfaces
{
    public interface IPrevidenciaM5Service
    {        
        byte[] GerarDocumentoPrevidenciaM5(Dictionary<string, string> dadosBoleto, PrevidenciaM5Type tipo);
        Task<byte[]> ConverterEGerarPrevidenciaM5PdfAsync(IFormFile file, PrevidenciaM5Type tipo);

    }
}
