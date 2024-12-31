using IntegraCVP.Application.Enums;
using Microsoft.AspNetCore.Http;

namespace IntegraCVP.Application.Interfaces.Capas;

public interface ICapaFacARService
{
    byte[] GerarCapaFacAR(Dictionary<string, string> dadosBoleto, CapaFacARType tipo);

    Task<byte[]> ConverterEGerarPdfAsync(IFormFile file, CapaFacARType tipo);
}
