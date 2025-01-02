using IntegraCVP.Application.Enums;
using IntegraCVP.Application.Interfaces.Capas;
using Microsoft.AspNetCore.Http;

namespace IntegraCVP.Application.Services.Capas;

public class CapaFacSimplesPBService : ICapaFacSimplesPBService
{
    public Task<byte[]> ConverterEGerarPdfAsync(IFormFile file, CapaFacPBType tipo)
    {
        throw new NotImplementedException();
    }

    public byte[] GerarCapaFacSimplesPB(Dictionary<string, string> dadosBoleto, CapaFacPBType tipo)
    {
        throw new NotImplementedException();
    }
}
