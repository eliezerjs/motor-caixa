using IntegraCVP.Application.Enums;
using iText.Forms.Form.Element;
using Microsoft.AspNetCore.Http;

namespace IntegraCVP.Application.Interfaces
{
    public interface IBoletoM1Service
    {
        //byte[] GerarBoletoM1(Dictionary<string, string> dadosBoleto, BoletoM1Type tipo);

        Task<byte[]> ConverterEGerarPdfAsync(IFormFile file, BoletoM1Type tipo);
    }
}
