using IntegraCVP.Application.Enums;
using Microsoft.AspNetCore.Http;

namespace IntegraCVP.Application.Interfaces.Capas;

public interface ICapaFacSimplesPBService
{
    byte[] GerarCapaFacSimplesPB(Dictionary<string, string> dadosBoleto, CapaFacPBType tipo);

    Task<byte[]> ConverterEGerarPdfAsync(IFormFile file, CapaFacPBType tipo);
}
