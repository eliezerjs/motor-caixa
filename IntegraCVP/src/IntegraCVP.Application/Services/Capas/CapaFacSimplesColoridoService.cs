using IntegraCVP.Application.Enums;
using IntegraCVP.Application.Interfaces.Capas;
using Microsoft.AspNetCore.Http;

namespace IntegraCVP.Application.Services.Capas
{
    public class CapaFacSimplesColoridoService : ICapaFacSimplesColoridoService
    {
        public Task<byte[]> ConverterEGerarPdfAsync(IFormFile file, CapaFacColoridoType tipo)
        {
            throw new NotImplementedException();
        }

        public byte[] GerarCapaFacSimplesColorido(Dictionary<string, string> dadosBoleto, CapaFacColoridoType tipo)
        {
            throw new NotImplementedException();
        }
    }
}
