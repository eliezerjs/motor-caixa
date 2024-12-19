using IntegraCVP.Application.Enums;
using Microsoft.AspNetCore.Http;

namespace IntegraCVP.Application.Interfaces
{
    public interface IPrevidenciaM6Service
    {        
        byte[] GerarDocumentoPrevidenciaM6(Dictionary<string, string> dadosBoleto, PrevidenciaM6Type tipo);
        Task<byte[]> ConverterEGerarPrevidenciaM6PdfAsync(IFormFile file, PrevidenciaM6Type tipo);

    }
}
