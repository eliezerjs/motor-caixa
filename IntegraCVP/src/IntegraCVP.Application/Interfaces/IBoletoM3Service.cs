using IntegraCVP.Application.Enums;
using Microsoft.AspNetCore.Http;

namespace IntegraCVP.Application.Interfaces
{
    public interface IBoletoM3Service
    {
        byte[] GerarBoletoM3(Dictionary<string, string> dadosBoleto, BoletoM3Type tipo);

        Task<byte[]> ConverterEGerarPdfAsync(IFormFile file, BoletoM3Type tipo);

    }
}
