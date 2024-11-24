using IntegraCVP.Application.Interfaces;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout.Element;

namespace IntegraCVP.Application.Services
{
    public class EmailService : IEmailService
    {
        public byte[] GerarEmailVidaExclusivaPdf(Dictionary<string, string> dados, string filename)
        {
            // Caminho da imagem de fundo
            string imagePath = System.IO.Path.Combine(AppContext.BaseDirectory, "Resources", "Email", filename + ".jpg");

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
                if (dados.ContainsKey(chave))
                {
                    var text = new Paragraph(dados[chave])
                        .SetFontSize(9)
                        .SetFixedPosition(x, pdfPage.GetPageSize().GetHeight() - y, 200);
                    document.Add(text);
                }
            }

            // Campos a desenhar
            DesenharCampo("SEGURADO", 59, 150);
            DesenharCampo("NUM_CERTIF", 59, 300);
            DesenharCampo("COD_PRODUTO", 59, 320);
            DesenharCampo("COD_SUSEP", 59, 350);

            document.Close();
            return pdfStream.ToArray();
        }

        public byte[] GerarEmailSegurosPdf(Dictionary<string, string> dados, string filename)
        {
            // Caminho da imagem de fundo
            string imagePath = System.IO.Path.Combine(AppContext.BaseDirectory, "Resources", "Email", filename + ".jog");

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
                if (dados.ContainsKey(chave))
                {
                    var text = new Paragraph(dados[chave])
                        .SetFontSize(9)
                        .SetFixedPosition(x, pdfPage.GetPageSize().GetHeight() - y, 200);
                    document.Add(text);
                }
            }

            // Campos a desenhar
            DesenharCampo("COD_PRODUTO", 59, 288);
            DesenharCampo("COD_SUSEP", 59, 356);

            document.Close();
            return pdfStream.ToArray();
        }
    }
}
