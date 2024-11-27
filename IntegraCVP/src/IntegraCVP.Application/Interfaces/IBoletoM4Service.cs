using IntegraCVP.Application.Enums;
using Microsoft.AspNetCore.Http;

namespace IntegraCVP.Application.Interfaces
{
    public interface IBoletoM4Service
    {
        byte[] GerarBoletoM4(Dictionary<string, string> dadosBoleto, BoletoM4Type tipo);
        Task<byte[]> ConverterEGerarPdfAsync(IFormFile file, BoletoM4Type tipo);
    }
}
