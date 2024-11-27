using IntegraCVP.Application.Enums;
using Microsoft.AspNetCore.Http;

namespace IntegraCVP.Application.Interfaces
{
    public interface IBoasVindasService
    {
        byte[] GerarDocumentoBoasVindas(Dictionary<string, string> dadosBoleto, BoasVindasType tipo);
        Task<byte[]> ConverterEGerarBoasVindasPdfAsync(IFormFile file, BoasVindasType tipo);
    }
}
