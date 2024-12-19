using IntegraCVP.Application.Enums;
using Microsoft.AspNetCore.Http;

namespace IntegraCVP.Application.Interfaces
{
    public interface IPrevidenciaM3Service
    {        
        byte[] GerarDocumentoPrevidenciaM3(Dictionary<string, string> dadosBoleto, PrevidenciaM3Type tipo);
        Task<byte[]> ConverterEGerarPrevidenciaM3PdfAsync(IFormFile file, PrevidenciaM3Type tipo);

    }
}
