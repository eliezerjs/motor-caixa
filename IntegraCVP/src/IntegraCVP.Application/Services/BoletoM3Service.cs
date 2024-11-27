using IntegraCVP.Application.Interfaces;

using System.IO;

namespace IntegraCVP.Application.Services
{
    public class BoletoM3Service : IBoletoM3Service
    {
        public byte[] GerarBoletoM3(Dictionary<string, string> dadosBoleto, string filename)
        {
            using var pdfStream = new MemoryStream();
            //pdfDocument.Save(pdfStream, false);
            return pdfStream.ToArray();
        }
    }
}
