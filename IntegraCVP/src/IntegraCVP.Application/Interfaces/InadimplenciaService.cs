using IntegraCVP.Application.Enums;
using Microsoft.AspNetCore.Http;

namespace IntegraCVP.Application.Interfaces
{
    public interface IInadimplenciaService
    {
        byte[] GerarDocumentoInadimplencia(Dictionary<string, string> dadosBoleto, InadimplenciaType tipo);
        Task<byte[]> ConverterEGerarInadimplenciaPdfAsync(IFormFile file, InadimplenciaType tipo);
    }
}
