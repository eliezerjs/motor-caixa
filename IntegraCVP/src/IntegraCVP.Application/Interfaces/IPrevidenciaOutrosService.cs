using IntegraCVP.Application.Enums;
using Microsoft.AspNetCore.Http;

namespace IntegraCVP.Application.Interfaces
{
    public interface IPrevidenciaOutrosService
    {        
        byte[] GerarDocumentoPrevidenciaOutros(Dictionary<string, string> dadosBoleto, PrevidenciaOutrosType tipo);
        Task<byte[]> ConverterEGerarPrevidenciaOutrosPdfAsync(IFormFile file, PrevidenciaOutrosType tipo);

    }
}
