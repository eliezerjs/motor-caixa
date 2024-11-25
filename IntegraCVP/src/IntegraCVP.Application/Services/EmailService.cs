using IntegraCVP.Application.Interfaces;
using iText.Kernel.Colors;
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

                    if (chave == "COD_PRODUTO" || chave == "COD_SUSEP" || chave == "COD_SUSEPCAP")
                    {
                        var text = new Paragraph(dados[chave])
                        .SetFontSize(5)
                        .SetFontColor(ColorConstants.WHITE)
                        .SetFixedPosition(x, pdfPage.GetPageSize().GetHeight() - y, 200);
                        document.Add(text);
                    }
                    else if (chave == "NUM_CERTIF")
                    {
                        var text = new Paragraph(dados[chave])
                        .SetFontSize(13)
                        .SetFixedPosition(x, pdfPage.GetPageSize().GetHeight() - y, 200);
                        document.Add(text);
                    }
                    else
                    {
                        var text = new Paragraph(dados[chave])
                        .SetFontSize(11)
                        .SetFixedPosition(x, pdfPage.GetPageSize().GetHeight() - y, 200);
                        document.Add(text);
                    }
                    
                }
            }

            // Campos a desenhar
            DesenharCampo("SEGURADO", 100, 174);
            DesenharCampo("NUM_CERTIF", 195, 324);
            DesenharCampo("COD_PRODUTO", 377, 787);
            DesenharCampo("COD_SUSEP", 427, 787);
            DesenharCampo("COD_SUSEPCAP", 100, 799);

            document.Close();
            return pdfStream.ToArray();
        }

        public byte[] GerarEmailSegurosPdf(Dictionary<string, string> dados, string filename)
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
                    if (chave == "COD_PRODUTO" || chave == "COD_SUSEP" || chave == "COD_SUSEPCAP")
                    {
                        var text = new Paragraph(dados[chave])
                        .SetFontSize(6)
                        .SetFixedPosition(x, pdfPage.GetPageSize().GetHeight() - y, 200);
                        document.Add(text);
                    }
                    else
                    {
                        var text = new Paragraph(dados[chave])
                        .SetFontSize(10)
                        .SetFixedPosition(x, pdfPage.GetPageSize().GetHeight() - y, 200);
                        document.Add(text);
                    }
                }
            }

            // Campos a desenhar
            DesenharCampo("SEGURADO", 80, 199);
            DesenharCampo("COD_PRODUTO", 357, 748);
            DesenharCampo("COD_SUSEP", 442, 748);
            DesenharCampo("COD_SUSEPCAP", 314, 766);

            document.Close();
            return pdfStream.ToArray();
        }

        public byte[] GerarEmailSegurosVIDA18Pdf(Dictionary<string, string> dados, string filename)
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
                    if (chave == "COD_PRODUTO" || chave == "COD_SUSEP" || chave == "COD_SUSEPCAP")
                    {
                        var text = new Paragraph(dados[chave])
                        .SetFontSize(6)
                        .SetFixedPosition(x, pdfPage.GetPageSize().GetHeight() - y, 200);
                        document.Add(text);
                    }
                    else
                    {
                        var text = new Paragraph(dados[chave])
                        .SetFontSize(10)
                        .SetFixedPosition(x, pdfPage.GetPageSize().GetHeight() - y, 200);
                        document.Add(text);
                    }
                }
            }

            // Campos a desenhar
            DesenharCampo("SEGURADO", 80, 199);
            DesenharCampo("COD_PRODUTO", 359, 748);
            DesenharCampo("COD_SUSEP", 444, 748);
            DesenharCampo("COD_SUSEPCAP", 350, 768);

            document.Close();
            return pdfStream.ToArray();
        }
    }
}
