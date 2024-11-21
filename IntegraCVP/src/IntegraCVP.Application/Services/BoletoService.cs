using IntegraCVP.Application.Interfaces;
using iText.Commons.Datastructures;
using QRCoder;
using System.Drawing;
using System.IO;
using SkiaSharp;
using System.Text.RegularExpressions;
using iText.Barcodes;
using iText.Kernel.Pdf;
using System.Reflection.Metadata;
using iText.Kernel.Geom;
using iText.Kernel.Colors;
using iText.Kernel.Pdf.Canvas;
using iText.IO.Codec;
using iText.IO.Image;
using iText.Layout.Element;


namespace IntegraCVP.Application.Services
{
    public class BoletoService : IBoletoService
    {
        public byte[] GerarBoletoPdf(Dictionary<string, string> dadosBoleto)
        {
            // Caminho da imagem de fundo
            string imagePath = System.IO.Path.Combine(AppContext.BaseDirectory, "Resources", "Boleto", "VD02.jpg");

            if (!File.Exists(imagePath))
            {
                throw new FileNotFoundException($"A imagem de fundo não foi encontrada no caminho: {imagePath}");
            }

            using var pdfStream = new MemoryStream();
            var writer = new PdfWriter(pdfStream);
            var pdfDocument = new PdfDocument(writer);
            var document = new iText.Layout.Document(pdfDocument);
            var pdfPage = pdfDocument.AddNewPage(PageSize.A4);

            // Adiciona a imagem de fundo
            byte[] imageBytes = File.ReadAllBytes(imagePath);
            var imageData = iText.IO.Image.ImageDataFactory.Create(imageBytes);
            var image = new iText.Layout.Element.Image(imageData);
            image.ScaleToFit(pdfDocument.GetDefaultPageSize().GetWidth(), pdfDocument.GetDefaultPageSize().GetHeight());
            image.SetFixedPosition(0, 0); // Define a posição
            document.Add(image);

            // Função auxiliar para adicionar texto
            void DesenharCampo(string chave, float x, float y)
            {
                if (dadosBoleto.ContainsKey(chave))
                {
                    var text = new Paragraph(dadosBoleto[chave])
                        .SetFontSize(9)
                        .SetFixedPosition(x, pdfPage.GetPageSize().GetHeight() - y, 200);
                    document.Add(text);
                }
            }

            // Campos a desenhar
            DesenharCampo("AGENCIA", 45, 136);
            DesenharCampo("APOLICE", 125, 136);
            DesenharCampo("FATURA", 197, 136);
            DesenharCampo("PERIODO", 260, 136);
            DesenharCampo("EMISSAO", 407, 136);
            DesenharCampo("VENCIMENT", 450, 136);

            DesenharCampo("ESTIPULANTE", 100, 600);
            DesenharCampo("ENDERECO_ESTIPULANTE", 100, 580);
            DesenharCampo("CNPJ_ESTIPULANTE", 400, 580);
            DesenharCampo("CIDADE_ESTADO_ESTIPULANTE", 700, 580);
            DesenharCampo("NUM_VIDAS", 100, 540);
            DesenharCampo("CAPITAL_SEGURADO", 400, 540);
            DesenharCampo("IOF", 700, 540);
            DesenharCampo("PREMIO", 900, 540);

            // Gera e adiciona o código de barras
            if (dadosBoleto.ContainsKey("NUMCDBARRA"))
            {
                string codigoPadronizado = MontarCodigoBarra(
                   dadosBoleto["NUMCDBARRA"],
                   ObterFatorVencimento(dadosBoleto["VENCIMENT"]),
                   ConverterValor(dadosBoleto["VALOR"])
               );

                
                var barcode = new Barcode128(pdfDocument);
                barcode.SetCode(codigoPadronizado);
                barcode.SetBarHeight(30); // Altura das barras
                barcode.SetX(1f); // Largura das barras
                var barcodeImage = new iText.Layout.Element.Image(barcode.CreateFormXObject(pdfDocument));
                barcodeImage.SetFixedPosition(50, 130); // Ajuste a posição
                document.Add(barcodeImage);
            }

            document.Close();
            return pdfStream.ToArray();
        }

        private string MontarCodigoBarra(string codigoExpandido, string fatorVencimento, string valorBoleto)
        {
            // Remove caracteres inválidos e monta os 44 dígitos
            string codigoLimpo = Regex.Replace(codigoExpandido, @"\D", "");

            if (codigoLimpo.Length < 25)
                throw new InvalidOperationException("O código expandido é inválido.");

            // Banco e moeda
            string bancoMoeda = codigoLimpo.Substring(0, 4);

            // Livre uso do banco (25 dígitos)
            string livreUso = codigoLimpo.Substring(4, 25);

            // Monta o código base
            string codigoBase = $"{bancoMoeda}0{fatorVencimento}{valorBoleto.PadLeft(10, '0')}{livreUso}";

            // Calcula DV e monta o código final
            return CalcularDigitoVerificador(codigoBase);
        }

        private string ObterFatorVencimento(string dataVencimento)
        {
            DateTime baseDate = new DateTime(1997, 10, 7);
            DateTime vencimento = DateTime.Parse(dataVencimento);
            return ((vencimento - baseDate).Days).ToString().PadLeft(4, '0');
        }

        private string ConverterValor(string valor)
        {
            // Remove separadores de milhar e vírgula para centavos
            string valorLimpo = Regex.Replace(valor, @"[.,]", "");
            return valorLimpo.PadLeft(10, '0');
        }

        private string CalcularDigitoVerificador(string codigo)
        {
            int peso = 2;
            int soma = 0;

            for (int i = codigo.Length - 1; i >= 0; i--)
            {
                int num = int.Parse(codigo[i].ToString());
                soma += num * peso;
                peso = peso == 9 ? 2 : peso + 1;
            }

            int resto = soma % 11;
            int dv = resto == 0 || resto == 1 ? 1 : 11 - resto;

            return codigo.Substring(0, 4) + dv + codigo.Substring(5);
        }

        public byte[] GerarCodigoBarra(string codigoBarra)
        {
            using var pdfStream = new MemoryStream();

            // Inicializa o PdfWriter e PdfDocument
            var pdfWriter = new PdfWriter(pdfStream);
            var pdfDocument = new PdfDocument(pdfWriter);

            // Adiciona uma nova página ao documento PDF
            var page = pdfDocument.AddNewPage();
            var canvas = new PdfCanvas(page);

            // Configura o código de barras (Barcode128)
            var barcode = new Barcode128(pdfDocument);
            barcode.SetCode(codigoBarra);
            barcode.SetBarHeight(50); // Define a altura das barras
            barcode.SetX(1.5f); // Define a largura das barras

            // Desenha o código de barras no canvas
            barcode.PlaceBarcode(canvas, new DeviceRgb(0, 0, 0), new DeviceRgb(0, 0, 0));

            // Fecha o documento PDF
            pdfDocument.Close();

            // Retorna o PDF como byte array
            return pdfStream.ToArray();
        }
    }
}
