using IntegraCVP.Application.Enums;
using Microsoft.AspNetCore.Http;

namespace IntegraCVP.Application.Interfaces
{
    public interface IBoletoM4Service
    {
        Task<byte[]> ConverterEGerarPdfAsync(IFormFile file, BoletoM4Type tipo);
    }
}
