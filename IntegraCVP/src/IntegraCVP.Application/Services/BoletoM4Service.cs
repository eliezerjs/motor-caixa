using IntegraCVP.Application.Enums;
using IntegraCVP.Application.Helper;
using IntegraCVP.Application.Interfaces;

namespace IntegraCVP.Application.Services
{
    public partial class BoletoM4Service : IBoletoM4Service
    {
        public const string BoletoM4 = "BoletoM4";
        public byte[] GerarBoletoM4(Dictionary<string, string> dadosBoleto, BoletoM4Type tipo)
        {
            string imagePath = GetImagePath(tipo, BoletoM4);

            var campos = tipo switch
            {
                BoletoM4Type.VA18 => GetCamposVA18(),
                BoletoM4Type.VA24 => GetCamposVA24(),
                BoletoM4Type.VIDA23 => GetCamposVIDA23(),
                BoletoM4Type.VIDA24 => GetCamposVIDA24(),
                _ => throw new ArgumentException("Tipo de boleto inválido.")
            };

            using var pdfStream = new MemoryStream();
            var (document, pdfDocument, pdfPage) = PdfHelper.InitializePdfDocument(imagePath, pdfStream);

            foreach (var (key, x, y, fontSize) in campos)
            {
                document.AddTextField(dadosBoleto, key, x, y, fontSize, pdfPage);
            }

            if (dadosBoleto.TryGetValue("NUMCDBARRA", out var codigoDeBarras))
            {
                string codigoPadronizado = PdfHelper.MontarCodigoBarra(
                    codigoDeBarras,
                    PdfHelper.ObterFatorVencimento(dadosBoleto["DTVENCTO"]),
                    PdfHelper.ConverterValor(dadosBoleto["VALOR"])
                );

                document.AddBarcode(pdfDocument, codigoPadronizado, 50, 130);
            }

            document.Close();
            return pdfStream.ToArray();
        }
    }
}
