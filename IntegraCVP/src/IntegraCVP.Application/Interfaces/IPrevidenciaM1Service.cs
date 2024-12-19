using IntegraCVP.Application.Enums;
using Microsoft.AspNetCore.Http;

namespace IntegraCVP.Application.Interfaces
{
    public interface IPrevidenciaM1Service
    {
        byte[] GerarDocumentoPrevidenciaM1(Dictionary<string, string> dadosBoleto, PrevidenciaM1Type tipo);
        Task<byte[]> ConverterEGerarPrevidenciaM1PdfAsync(IFormFile file, PrevidenciaM1Type tipo);
    }
}
