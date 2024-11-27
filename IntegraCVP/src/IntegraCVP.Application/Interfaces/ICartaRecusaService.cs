using IntegraCVP.Application.Enums;
using Microsoft.AspNetCore.Http;

namespace IntegraCVP.Application.Interfaces
{
    public interface ICartaRecusaService
    {
        byte[] GerarDocumentoCartaRecusa(Dictionary<string, string> dadosBoleto, CartaRecusaType tipo);
        Task<byte[]> ConverterEGerarCartaRecusaPdfAsync(IFormFile file, CartaRecusaType tipo);
    }
}
