using Microsoft.AspNetCore.Http;

namespace IntegraCVP.Application.Interfaces
{
    public interface ITerminalConverterService
    {
        Task<byte[]> ConverterEGerarZipAsync(IFormFile file);
        Task<string> ConverterEGerarPrevidenciaAsync(IFormFile file);
    }
}
