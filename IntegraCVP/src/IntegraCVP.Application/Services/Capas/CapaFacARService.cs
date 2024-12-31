using IntegraCVP.Application.Enums;
using IntegraCVP.Application.Interfaces.Capas;
using Microsoft.AspNetCore.Http;

namespace IntegraCVP.Application.Services.Capas;

public class CapaFacARService : ICapaFacARService
{
    public Task<byte[]> ConverterEGerarPdfAsync(IFormFile file, CapaFacARType tipo)
    {
        throw new NotImplementedException();
    }

    public byte[] GerarCapaFacAR(Dictionary<string, string> dadosBoleto, CapaFacARType tipo)
    {
        throw new NotImplementedException();
    }
}
