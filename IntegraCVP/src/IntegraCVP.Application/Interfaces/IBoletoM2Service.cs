using IntegraCVP.Application.Enums;
using Microsoft.AspNetCore.Http;

namespace IntegraCVP.Application.Interfaces
{
    public interface IBoletoM2Service
    {
        byte[] GerarBoletoM2(Dictionary<string, string> dadosBoleto, BoletoM2Type tipo);

        Task<byte[]> ConverterEGerarPdfAsync(IFormFile file, BoletoM2Type tipo);
    }
}
