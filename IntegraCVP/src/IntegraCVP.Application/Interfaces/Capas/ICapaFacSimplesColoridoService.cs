using IntegraCVP.Application.Enums;
using Microsoft.AspNetCore.Http;

namespace IntegraCVP.Application.Interfaces.Capas
{
    public interface ICapaFacSimplesColoridoService
    {
        byte[] GerarCapaFacSimplesColorido(Dictionary<string, string> dadosBoleto, CapaFacColoridoType tipo);

        Task<byte[]> ConverterEGerarPdfAsync(IFormFile file, CapaFacColoridoType tipo);
    }
}
