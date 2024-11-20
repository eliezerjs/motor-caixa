using IntegraCVP.Application.Interfaces;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using System.IO;

namespace IntegraCVP.Application.Services
{
    public class BoletoService : IBoletoService
    {
        public byte[] GerarBoletoPdf()
        {
            // Defina o caminho relativo para a imagem de fundo com base no diretório da aplicação
            string imagePath = Path.Combine(AppContext.BaseDirectory, "Resources", "boleto_fundo.jpg");

            // Verifique se o arquivo existe para evitar exceções
            if (!File.Exists(imagePath))
            {
                throw new FileNotFoundException($"A imagem de fundo não foi encontrada no caminho: {imagePath}");
            }

            // Cria um novo documento PDF
            using var pdfDocument = new PdfDocument();
            var pdfPage = pdfDocument.AddPage();

            // Carrega a imagem de fundo
            using var imageStream = new MemoryStream(File.ReadAllBytes(imagePath));
            var xImage = XImage.FromStream(() => imageStream);

            // Define o tamanho da página de acordo com a imagem
            pdfPage.Width = XUnit.FromPoint(xImage.PixelWidth);
            pdfPage.Height = XUnit.FromPoint(xImage.PixelHeight);

            // Cria o objeto gráfico para desenhar na página
            var graphics = XGraphics.FromPdfPage(pdfPage);

            // Desenha a imagem de fundo na página
            graphics.DrawImage(xImage, 0, 0, pdfPage.Width, pdfPage.Height);

            // Configurações de fonte
            var font = new XFont("Arial", 14, XFontStyle.Regular);

            // Campos parametrizáveis
            graphics.DrawString("@AGENCIA", font, XBrushes.Black, new XPoint(90, 270));
            graphics.DrawString("@APOLICE", font, XBrushes.Black, new XPoint(263, 270));
            graphics.DrawString("@FATURA", font, XBrushes.Black, new XPoint(410, 270));
            graphics.DrawString("@PERIODO", font, XBrushes.Black, new XPoint(540, 270));
            graphics.DrawString("@EMISSAO", font, XBrushes.Black, new XPoint(850, 270));
            graphics.DrawString("@VENCIMENTO", font, XBrushes.Black, new XPoint(1005, 270));

            graphics.DrawString("@ESTIPULANTE", font, XBrushes.Black, new XPoint(90, 319));
            graphics.DrawString("@ENDERECO", font, XBrushes.Black, new XPoint(540, 320));

            graphics.DrawString("@CEP", font, XBrushes.Black, new XPoint(90, 370));
            graphics.DrawString("@CIDADE", font, XBrushes.Black, new XPoint(320, 370));
            graphics.DrawString("@ESTADO", font, XBrushes.Black, new XPoint(850, 370));
            graphics.DrawString("@CNPJ", font, XBrushes.Black, new XPoint(940, 370));

            graphics.DrawString("@SUB_ESTIPULANTE", font, XBrushes.Black, new XPoint(90, 420));
            graphics.DrawString("@ENDERECO", font, XBrushes.Black, new XPoint(540, 420));

            graphics.DrawString("@CEP", font, XBrushes.Black, new XPoint(90, 467));
            graphics.DrawString("@CIDADE", font, XBrushes.Black, new XPoint(320, 467));
            graphics.DrawString("@ESTADO", font, XBrushes.Black, new XPoint(850, 467));
            graphics.DrawString("@CNPJ", font, XBrushes.Black, new XPoint(940, 467));

            graphics.DrawString("@NVIAS", font, XBrushes.Black, new XPoint(90, 517));
            graphics.DrawString("@CAPITAL", font, XBrushes.Black, new XPoint(320, 517));
            graphics.DrawString("@IOF", font, XBrushes.Black, new XPoint(645, 517));
            graphics.DrawString("@PREMIOS", font, XBrushes.Black, new XPoint(850, 517));

            graphics.DrawString("@NDOCUMENTO", font, XBrushes.Black, new XPoint(970, 779));

            graphics.DrawString("@AG_BENEFICIARIO", font, XBrushes.Black, new XPoint(755, 819));
            graphics.DrawString("@VENCIMENTO", font, XBrushes.Black, new XPoint(970, 819));

            graphics.DrawString("@PAGADOR", font, XBrushes.Black, new XPoint(93, 861));
            graphics.DrawString("@NNUMERO", font, XBrushes.Black, new XPoint(777, 861));
            graphics.DrawString("@VTITULO", font, XBrushes.Black, new XPoint(970, 861));

            graphics.DrawString("@AUTMECANICA", font, XBrushes.Black, new XPoint(777, 937));

            graphics.DrawString("@PARCELA", font, XBrushes.Black, new XPoint(920, 1103));
            graphics.DrawString("@VENCIMENTO", font, XBrushes.Black, new XPoint(1029, 1103));

            graphics.DrawString("@AGENCIA", font, XBrushes.Black, new XPoint(920, 1141));

            graphics.DrawString("@DATA_doc", font, XBrushes.Black, new XPoint(93, 1177));
            graphics.DrawString("@NDOCUMENTO", font, XBrushes.Black, new XPoint(279, 1177));
            graphics.DrawString("@ESPECIE_DOC", font, XBrushes.Black, new XPoint(452, 1177));
            graphics.DrawString("@ACEITE", font, XBrushes.Black, new XPoint(700, 1177));
            graphics.DrawString("@DATA_PROCES", font, XBrushes.Black, new XPoint(777, 1177));
            graphics.DrawString("@NOSS_NUM", font, XBrushes.Black, new XPoint(920, 1177));

            graphics.DrawString("@USO_BANCO", font, XBrushes.Black, new XPoint(93, 1212));
            graphics.DrawString("@CARTEIRA", font, XBrushes.Black, new XPoint(370, 1212));
            graphics.DrawString("@ESPECIE", font, XBrushes.Black, new XPoint(542, 1212));
            graphics.DrawString("@QUANT_MOEDA", font, XBrushes.Black, new XPoint(620, 1212));
            graphics.DrawString("@VALOR", font, XBrushes.Black, new XPoint(775, 1212));
            graphics.DrawString("@VALOR_DOC", font, XBrushes.Black, new XPoint(920, 1212));

            graphics.DrawString("@DESCONTO", font, XBrushes.Black, new XPoint(920, 1242));

            graphics.DrawString("@DEDUCOES", font, XBrushes.Black, new XPoint(920, 1276));

            graphics.DrawString("@MULTA", font, XBrushes.Black, new XPoint(920, 1309));

            graphics.DrawString("@ACRECIMOS", font, XBrushes.Black, new XPoint(920, 1345));

            graphics.DrawString("@VALOR_COBRADO", font, XBrushes.Black, new XPoint(920, 1378));

            graphics.DrawString("@VALOR_COBRADO", font, XBrushes.Black, new XPoint(93, 1493));

            // Salva o PDF em um array de bytes
            using var pdfStream = new MemoryStream();
            pdfDocument.Save(pdfStream, false);
            return pdfStream.ToArray();
        }
    }
}
