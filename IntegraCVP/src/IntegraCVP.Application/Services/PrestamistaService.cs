using IntegraCVP.Application.Interfaces;
using System.Text.RegularExpressions;
using iText.Barcodes;
using iText.Kernel.Pdf;
using iText.Kernel.Geom;
using iText.Kernel.Colors;
using iText.Kernel.Pdf.Canvas;
using iText.Layout.Element;


namespace IntegraCVP.Application.Services
{
    public class PrestamistaService : IPrestamistaService
    {
        public byte[] GerarBoasVindasPdf(Dictionary<string, string> dados)
        {

            // Caminho da imagem de fundo
            string imagePath = System.IO.Path.Combine(AppContext.BaseDirectory, "Resources", "Prestamista", "PREST01.jpg");

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
            DesenharCampo("SEGURADO", 59, 250);
            DesenharCampo("NOME_PRODUTO", 59, 288);
            DesenharCampo("NUM_CERTIF", 59, 320);
            DesenharCampo("DT_VIG", 59, 356);

            DesenharCampo("CUSTO", 60, 470);
            DesenharCampo("OPCAO_PAG", 60, 510);

            document.Close();
            return pdfStream.ToArray();
        }

        public byte[] GerarBoasVindasQuinzeP1Pdf(Dictionary<string, string> dados)
        {

            // Caminho da imagem de fundo
            string imagePath = System.IO.Path.Combine(AppContext.BaseDirectory, "Resources", "Prestamista", "PREST15P1.jpg");

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
            DesenharCampo("SEGURADO", 59, 250);
            DesenharCampo("ENDERECO", 59, 288);
            DesenharCampo("CIDADE", 59, 320);
            DesenharCampo("UF", 59, 356);

            document.Close();
            return pdfStream.ToArray();
        }

        public byte[] GerarBoasVindasQuinzeP2Pdf(Dictionary<string, string> dados)
        {

            // Caminho da imagem de fundo
            string imagePath = System.IO.Path.Combine(AppContext.BaseDirectory, "Resources", "Prestamista", "PREST15P2.jpg");

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
            DesenharCampo("SEGURADO", 59, 250);
            DesenharCampo("NOME_PRODUTO", 59, 288);
            DesenharCampo("NUM_CERTIF", 59, 320);
            DesenharCampo("DT_VIG", 59, 356);

            DesenharCampo("CUSTO", 60, 470);
            DesenharCampo("OPCAO_PAG", 60, 510);

            document.Close();
            return pdfStream.ToArray();
        }

        public byte[] GerarBoasVindas15Pdf()
        {

            // Caminho da imagem de fundo
            string imagePath = System.IO.Path.Combine(AppContext.BaseDirectory, "Resources", "Prestamista", "PREST15.jpg");

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

                var text = new Paragraph(chave)
                    .SetFontSize(9)
                    .SetFixedPosition(x, pdfPage.GetPageSize().GetHeight() - y, 200);
                document.Add(text);

            }

            // Campos a desenhar
            DesenharCampo("@NOME", 56, 250);
            DesenharCampo("@PRODUTO", 56, 288);
            DesenharCampo("@CERTIFICADO", 56, 320);
            DesenharCampo("@VIGENCIA", 56, 356);

            DesenharCampo("@PERIODICIDADE_PGTO", 56, 470);
            DesenharCampo("@FORMA_PGTO", 56, 510);


            document.Close();
            return pdfStream.ToArray();
        }

    }
}
