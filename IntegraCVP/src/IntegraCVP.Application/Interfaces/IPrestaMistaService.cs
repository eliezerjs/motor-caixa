using IntegraCVP.Application.Enums;
using Microsoft.AspNetCore.Http;

namespace IntegraCVP.Application.Interfaces
{
    public interface IPrestamistaService
    {        
        byte[] GerarDocumentoPrestamista(Dictionary<string, string> dadosBoleto, PrestamistaType tipo);
        Task<byte[]> ConverterEGerarPrestamistaPdfAsync(IFormFile file, PrestamistaType tipo);

    }
}
