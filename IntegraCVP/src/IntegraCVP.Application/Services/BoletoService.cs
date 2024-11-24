using IntegraCVP.Application.Interfaces;
using iText.Barcodes;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout.Element;
using System.Text.RegularExpressions;


namespace IntegraCVP.Application.Services
{
    public class BoletoService : IBoletoService
    {
 
        public byte[] GerarBoletoVA18Pdf(Dictionary<string, string> dadosBoleto)
        {
            // Caminho da imagem de fundo
            string imagePath = System.IO.Path.Combine(AppContext.BaseDirectory, "Resources", "Seguro_Grupo", "VA18.jpg");

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

            DesenharCampo("PRODUTO", 57, 68);
            DesenharCampo("COD_PROD", 306, 69);
            DesenharCampo("PROC_SUSEP", 392, 69);

            DesenharCampo("NOME_CLIENTE", 55, 110);
            DesenharCampo("CPF", 327, 110);
            DesenharCampo("DT_NASC", 453, 110);


            DesenharCampo("NUMDOCTO", 470, 405);

            DesenharCampo("CEDENTE", 359, 424);
            DesenharCampo("DTVENCTO", 467, 424);

            DesenharCampo("NSNUMERO", 368, 444);
            DesenharCampo("VALDOCTO", 465, 444);

            DesenharCampo("NUMOBJETO", 371, 480);

            DesenharCampo("PARCELA", 440, 528);
            DesenharCampo("DTVENCTO", 492, 528);

            DesenharCampo("CEDENTE", 440, 546);

            DesenharCampo("DTDOCTO", 46, 563);
            DesenharCampo("NUMDOCTO", 133, 563);
            DesenharCampo("DTPROCESS", 370, 563);
            DesenharCampo("NSNUMERO", 440, 563);

            DesenharCampo("", 46, 580);
            DesenharCampo("", 299, 580);
            DesenharCampo("VALOR", 370, 580);

            DesenharCampo("VALDOCTO", 440, 580);

            DesenharCampo("", 440, 595);

            DesenharCampo("ABATIMENTO", 440, 610);

            DesenharCampo("", 440, 627);

            DesenharCampo("", 440, 643);

            DesenharCampo("VALDOCTO", 440, 660);


            // Gera e adiciona o código de barras
            if (dadosBoleto.ContainsKey("NUMCDBARRA"))
            {
                string codigoPadronizado = MontarCodigoBarra(
                   dadosBoleto["NUMCDBARRA"],
                   ObterFatorVencimento(dadosBoleto["DTVENCTO"]),
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

        public byte[] GerarBoletoVA24Pdf(Dictionary<string, string> dadosBoleto)
        {
            // Caminho da imagem de fundo
            string imagePath = System.IO.Path.Combine(AppContext.BaseDirectory, "Resources", "Seguro_Grupo", "VA24.jpg");

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

            DesenharCampo("PRODUTO", 57, 87);
            DesenharCampo("COD_PROD", 308, 87);
            DesenharCampo("PROC_SUSEP", 394, 87);

            DesenharCampo("NOME_CLIENTE", 56, 132);
            DesenharCampo("CPF", 327, 132);
            DesenharCampo("DT_NASC", 454, 132);


            DesenharCampo("NUMDOCTO", 470, 364);

            DesenharCampo("CEDENTE", 359, 384);
            DesenharCampo("DTVENCTO", 467, 384);

            DesenharCampo("NSNUMERO", 368, 404);
            DesenharCampo("VALDOCTO", 465, 404);

            DesenharCampo("NUMOBJETO", 371, 440);

            DesenharCampo("PARCELA", 440, 491);
            DesenharCampo("DTVENCTO", 492, 491);

            DesenharCampo("CEDENTE", 440, 509);

            DesenharCampo("DTDOCTO", 46, 526);
            DesenharCampo("NUMDOCTO", 133, 526);
            DesenharCampo("DTPROCESS", 370, 526);
            DesenharCampo("NSNUMERO", 440, 526);

            DesenharCampo("", 46, 543);
            DesenharCampo("", 299, 543);
            DesenharCampo("VALOR", 370, 543);
            DesenharCampo("VALDOCTO", 440, 543);

            DesenharCampo("", 440, 558);

            DesenharCampo("ABATIMENTO", 440, 574);

            DesenharCampo("", 440, 590);

            DesenharCampo("", 440, 607);

            DesenharCampo("VALDOCTO", 440, 623);


            // Gera e adiciona o código de barras
            if (dadosBoleto.ContainsKey("NUMCDBARRA"))
            {
                string codigoPadronizado = MontarCodigoBarra(
                   dadosBoleto["NUMCDBARRA"],
                   ObterFatorVencimento(dadosBoleto["DTVENCTO"]),
                   ConverterValor(dadosBoleto["VALOR"])
               );


                var barcode = new Barcode128(pdfDocument);
                barcode.SetCode(codigoPadronizado);
                barcode.SetBarHeight(30); // Altura das barras
                barcode.SetX(1f); // Largura das barras
                var barcodeImage = new iText.Layout.Element.Image(barcode.CreateFormXObject(pdfDocument));
                barcodeImage.SetFixedPosition(50, 178); // Ajuste a posição
                document.Add(barcodeImage);
            }

            document.Close();
            return pdfStream.ToArray();
        }

        public byte[] GerarBoletoVD02Pdf(Dictionary<string, string> dadosBoleto)
        {
            // Caminho da imagem de fundo
            string imagePath = System.IO.Path.Combine(AppContext.BaseDirectory, "Resources", "Boleto", "VD02.jpg");
            string fontPath = System.IO.Path.Combine(AppContext.BaseDirectory, "Resources", "Fonts", "ARIAL.TTF");
            var font = PdfFontFactory.CreateFont(fontPath, iText.IO.Font.PdfEncodings.IDENTITY_H);

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
            image.SetFixedPosition(0, 0); 
            document.Add(image);

            // Função auxiliar para adicionar texto
            void DesenharCampo(string chave, float x, float y)
            {
                if (dadosBoleto.ContainsKey(chave))
                {
                    var text = new Paragraph(dadosBoleto[chave])
                        .SetFontSize(8)
                        .SetFont(font)
                        .SetFixedPosition(x, pdfPage.GetPageSize().GetHeight() - y, 200);
                    document.Add(text);
                }
            }

            void DesenharCampoManual(string campo, float x, float y)
            {
                var text = new Paragraph(campo)
                    .SetFontSize(8)
                    .SetFont(font)
                    .SetFixedPosition(x, pdfPage.GetPageSize().GetHeight() - y, 200);
                document.Add(text);
            }


            // Campos a desenhar
            DesenharCampo("AGENCIA", 45, 133);
            DesenharCampo("APOLICE", 125, 133);
            DesenharCampo("FATURA", 197, 133);
            DesenharCampo("PERIODO", 260, 133);
            DesenharCampo("EMISSAO", 407, 133);
            DesenharCampo("VENCIMENT", 481, 133);

            DesenharCampo("ESTIPULANTE", 45, 157);
            DesenharCampo("ENDERECO", 260, 157);

            DesenharCampo("CEP", 45, 182);
            DesenharCampo("CIDADE", 154, 182);
            DesenharCampo("UF", 407, 182);
            DesenharCampo("CNPJ", 453, 182);

            DesenharCampo("ESTIPULANTE", 45, 205);
            DesenharCampo("ENDERECO", 260, 205);

            DesenharCampo("CEP", 45, 230);
            DesenharCampo("CIDADE", 154, 230);
            DesenharCampo("UF", 407, 230);
            DesenharCampo("CNPJ", 453, 230);

            DesenharCampo("NVIDAS", 45, 252);
            DesenharCampo("CAPITAL", 154, 252);
            DesenharCampo("IOF", 310, 252);
            DesenharCampo("PREMIO", 407, 252);

            DesenharCampo("NUMDOCTO", 470, 378);

            DesenharCampo("AGENCIA", 470, 398);
            DesenharCampo("VENCIMENT", 470, 398);

            DesenharCampo("NSNUMERO", 370, 418);
            DesenharCampo("VALDOCTO", 470, 418);

            //DesenharCampo("AGENCIA", 375, 455);

            DesenharCampo("PARCELA", 443, 534);
            DesenharCampo("VENCIMENT", 494, 534);

            DesenharCampo("AGENCIA", 443, 552);

            DesenharCampo("DTDOCTO", 45, 568);
            DesenharCampo("CODDOC", 135, 568);
            DesenharCampo("AGENCIA", 219, 568);
            DesenharCampo("AGENCIA", 340, 568);
            DesenharCampo("AGENCIA", 370, 568);
            DesenharCampo("NSNUMERO", 443, 568);

            var carteira = CalcularCodigoCedente(dadosBoleto);

            //DesenharCampoManual(carteira, 45, 585);
            //DesenharCampo("AGENCIA", 45, 585);

            //DesenharCampo("AGENCIA", 180, 585);

            string especieMoeda = ObterEspecieMoeda(ObterEspecieMoedaDoCodigoBarra(dadosBoleto["NUMCDBARRA"]));
            DesenharCampoManual(especieMoeda, 262, 585);

            DesenharCampo("AGENCIA", 300, 585);
            DesenharCampo("AGENCIA", 370, 585);
            DesenharCampo("VALDOCTO", 443, 585);

            DesenharCampo("AGENCIA", 443, 602);

            DesenharCampo("AGENCIA", 443, 617);

            DesenharCampo("AGENCIA", 443, 633);

            DesenharCampo("AGENCIA", 443, 650);

            DesenharCampo("VALDOCTO", 443, 666);



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

        public byte[] GerarBoletoVIDA23Pdf(Dictionary<string, string> dadosBoleto)
        {
            // Caminho da imagem de fundo
            string imagePath = System.IO.Path.Combine(AppContext.BaseDirectory, "Resources", "Seguro_Grupo", "VIDA23.jpg");

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

            DesenharCampo("PRODUTO", 57, 68);
            DesenharCampo("COD_PROD", 307, 68);
            DesenharCampo("PROC_SUSEP", 393, 68);

            DesenharCampo("NOME_CLIENTE", 55, 110);
            DesenharCampo("CPF", 327, 110);
            DesenharCampo("DT_NASC", 453, 110);


            DesenharCampo("NUMDOCTO", 470, 390);

            DesenharCampo("CEDENTE", 359, 410);
            DesenharCampo("DTVENCTO", 467, 410);

            DesenharCampo("NSNUMERO", 368, 430);
            DesenharCampo("VALDOCTO", 465, 430);

            DesenharCampo("NUMOBJETO", 371, 463);

            DesenharCampo("PARCELA", 440, 514);
            DesenharCampo("DTVENCTO", 492, 514);

            DesenharCampo("CEDENTE", 440, 532);

            DesenharCampo("DTDOCTO", 46, 549);
            DesenharCampo("NUMDOCTO", 133, 549);
            DesenharCampo("DTPROCESS", 370, 549);
            DesenharCampo("NSNUMERO", 440, 549);

            DesenharCampo("", 46, 566);
            DesenharCampo("", 299, 566);
            DesenharCampo("VALOR", 370, 566);

            DesenharCampo("VALDOCTO", 440, 566);

            DesenharCampo("", 440, 581);

            DesenharCampo("ABATIMENTO", 440, 597);

            DesenharCampo("", 440, 613);

            DesenharCampo("", 440, 630);

            DesenharCampo("VALDOCTO", 440, 646);


            // Gera e adiciona o código de barras
            if (dadosBoleto.ContainsKey("NUMCDBARRA"))
            {
                string codigoPadronizado = MontarCodigoBarra(
                   dadosBoleto["NUMCDBARRA"],
                   ObterFatorVencimento(dadosBoleto["DTVENCTO"]),
                   ConverterValor(dadosBoleto["VALOR"])
               );


                var barcode = new Barcode128(pdfDocument);
                barcode.SetCode(codigoPadronizado);
                barcode.SetBarHeight(30); // Altura das barras
                barcode.SetX(1f); // Largura das barras
                var barcodeImage = new iText.Layout.Element.Image(barcode.CreateFormXObject(pdfDocument));
                barcodeImage.SetFixedPosition(50, 145); // Ajuste a posição
                document.Add(barcodeImage);
            }

            document.Close();
            return pdfStream.ToArray();
        }

        public byte[] GerarBoletoVIDA24Pdf(Dictionary<string, string> dadosBoleto)
        {
            // Caminho da imagem de fundo
            string imagePath = System.IO.Path.Combine(AppContext.BaseDirectory, "Resources", "Seguro_Grupo", "VIDA24.jpg");

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

            DesenharCampo("PRODUTO", 57, 81);
            DesenharCampo("COD_PROD", 308, 81);
            DesenharCampo("PROC_SUSEP", 394, 81);

            DesenharCampo("NOME_CLIENTE", 56, 129);
            DesenharCampo("CPF", 327, 129);
            DesenharCampo("DT_NASC", 454, 129);


            DesenharCampo("NUMDOCTO", 470, 459);

            DesenharCampo("CEDENTE", 359, 479);
            DesenharCampo("DTVENCTO", 467, 479);

            DesenharCampo("NSNUMERO", 368, 499);
            DesenharCampo("VALDOCTO", 465, 499);

            DesenharCampo("NUMOBJETO", 371, 535);

            DesenharCampo("PARCELA", 440, 586);
            DesenharCampo("DTVENCTO", 492, 586);

            DesenharCampo("CEDENTE", 440, 604);

            DesenharCampo("DTDOCTO", 46, 621);
            DesenharCampo("NUMDOCTO", 133, 621);
            DesenharCampo("DTPROCESS", 370, 621);
            DesenharCampo("NSNUMERO", 440, 621);

            DesenharCampo("", 46, 638);
            DesenharCampo("", 299, 638);
            DesenharCampo("VALOR", 370, 638);
            DesenharCampo("VALDOCTO", 440, 638);

            DesenharCampo("PARCELA", 440, 653);

            DesenharCampo("PARCELA", 440, 669);

            DesenharCampo("PARCELA", 440, 685);

            DesenharCampo("PARCELA", 440, 701);

            DesenharCampo("VALDOCTO", 440, 718);


            // Gera e adiciona o código de barras
            if (dadosBoleto.ContainsKey("NUMCDBARRA"))
            {
                string codigoPadronizado = MontarCodigoBarra(
                   dadosBoleto["NUMCDBARRA"],
                   ObterFatorVencimento(dadosBoleto["DTVENCTO"]),
                   ConverterValor(dadosBoleto["VALOR"])
               );


                var barcode = new Barcode128(pdfDocument);
                barcode.SetCode(codigoPadronizado);
                barcode.SetBarHeight(30); // Altura das barras
                barcode.SetX(1f); // Largura das barras
                var barcodeImage = new iText.Layout.Element.Image(barcode.CreateFormXObject(pdfDocument));
                barcodeImage.SetFixedPosition(50, 78); // Ajuste a posição
                document.Add(barcodeImage);
            }

            document.Close();
            return pdfStream.ToArray();
        }

        //Pasta Seguro
        public byte[] GerarBoletoSeguro(Dictionary<string, string> dadosBoleto, string filename)
        {
            // Caminho da imagem de fundo
            string imagePath = System.IO.Path.Combine(AppContext.BaseDirectory, "Resources", "Seguro", filename + ".jpg");

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
                        .SetFontSize(8)
                        .SetFixedPosition(x, pdfPage.GetPageSize().GetHeight() - y, 200);
                    document.Add(text);
                }
            }

            // Campos a desenhar
            var colunaInicial = 47;
            var linha1 = 186;
            DesenharCampo("AGENCIA", colunaInicial, linha1);
            DesenharCampo("APOLICE", 138, linha1);
            DesenharCampo("FATURA", 210, linha1);
            DesenharCampo("PERIODO", 282, linha1);
            DesenharCampo("EMISSAO", 409, linha1);
            DesenharCampo("VENCIMENT", 484, linha1);

            var linha2 = 217;
            DesenharCampo("ESTIPULANTE", colunaInicial, linha2);
            DesenharCampo("CNPJ1", 340, linha2);

            var linha3 = 251;
            DesenharCampo("ENDERE1", colunaInicial, linha3);

            var linha4 = 284;
            DesenharCampo("CEP1", colunaInicial, linha4);
            DesenharCampo("CIDADE1", 157, linha4);
            DesenharCampo("EST1", 488, linha4);

            var linha5 = 317;
            DesenharCampo("SUBESTIPULANTE", colunaInicial, linha5);
            DesenharCampo("CNPJ2", 396, linha5);

            var linha6 = 350;
            DesenharCampo("ENDERE2", colunaInicial, linha6);

            var linha7 = 385;
            DesenharCampo("CEP2", colunaInicial, linha7);
            DesenharCampo("CIDADE2", 156, linha7);
            DesenharCampo("EST2", 480, linha7);

            var linha8 = 418;
            DesenharCampo("NVIDAS", colunaInicial, linha8);
            DesenharCampo("CAPITAL", 156, linha8);
            DesenharCampo("IOF", 312, linha8);
            DesenharCampo("PREMIO", 400, linha8);

            document.Close();
            return pdfStream.ToArray();
        }

        //Pasta Seguro_Grupo
        public byte[] GerarBoletoVA18Pdf(Dictionary<string, string> dadosBoleto)
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
            DesenharCampo("AGENCIA", 45, 135);
            DesenharCampo("APOLICE", 125, 135);
            DesenharCampo("FATURA", 197, 135);
            DesenharCampo("PERIODO", 260, 135);
            DesenharCampo("EMISSAO", 407, 135);
            DesenharCampo("VENCIMENT", 481, 135);

            DesenharCampo("ESTIPULANTE", 45, 159);
            DesenharCampo("ENDERECO", 260, 159);

            DesenharCampo("CEP", 45, 182);
            DesenharCampo("CIDADE", 154, 182);
            DesenharCampo("UF", 407, 182);
            DesenharCampo("CNPJ1", 453, 182);

            DesenharCampo("ESTIPULANTE", 45, 205);
            DesenharCampo("ENDERECO", 260, 205);

            DesenharCampo("CEP", 45, 230);
            DesenharCampo("CIDADE", 154, 230);
            DesenharCampo("UF", 407, 230);
            DesenharCampo("CNPJ1", 453, 230);

            DesenharCampo("NVIDAS", 45, 252);
            DesenharCampo("CAPITAL", 154, 252);
            DesenharCampo("IOF", 310, 252);
            DesenharCampo("PREMIO", 407, 252);

            DesenharCampo("NUMDOCTO", 465, 380);

            DesenharCampo("AGENCIA", 365, 398);
            DesenharCampo("VENCIMENT", 470, 398);

            DesenharCampo("NSNUMERO", 370, 419);
            DesenharCampo("VALDOCTO", 468, 419);

            DesenharCampo("AGENCIA", 375, 455);


            DesenharCampo("PARCELA", 443, 535);
            DesenharCampo("VENCIMENT", 490, 535);

            DesenharCampo("AGENCIA", 443, 552);

            DesenharCampo("AGENCIA", 45, 570);
            DesenharCampo("CODDOC", 135, 570);
            DesenharCampo("AGENCIA", 219, 570);
            DesenharCampo("AGENCIA", 340, 570);
            DesenharCampo("AGENCIA", 370, 570);
            DesenharCampo("NSNUMERO", 443, 570);

            DesenharCampo("AGENCIA", 45, 585);
            DesenharCampo("AGENCIA", 180, 585);
            DesenharCampo("AGENCIA", 262, 585);
            DesenharCampo("AGENCIA", 300, 585);
            DesenharCampo("AGENCIA", 370, 585);
            DesenharCampo("VALDOCTO", 443, 585);

            DesenharCampo("AGENCIA", 443, 602);

            DesenharCampo("AGENCIA", 443, 617);

            DesenharCampo("AGENCIA", 443, 633);

            DesenharCampo("AGENCIA", 443, 650);

            DesenharCampo("VALDOCTO", 443, 666);



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

        /// <summary>
        /// Extrai a espécie de moeda do campo CODBARRA.
        /// </summary>
        /// <param name="codigoBarra">Código de barras completo do boleto.</param>
        /// <returns>Código da moeda como string.</returns>
        private string ObterEspecieMoedaDoCodigoBarra(string codigoBarra)
        {
            if (string.IsNullOrEmpty(codigoBarra) || codigoBarra.Length < 4)
            {
                throw new ArgumentException("Código de barras inválido ou vazio.");
            }

            // A quarta posição indica a espécie de moeda
            return codigoBarra[3].ToString();
        }

        /// <summary>
        /// Retorna a sigla da espécie de moeda com base no código.
        /// </summary>
        /// <param name="codigoMoeda">Código da moeda (e.g., "9").</param>
        /// <returns>Sigla da moeda ou "UNK" caso o código não seja reconhecido.</returns>
        private string ObterEspecieMoeda(string codigoMoeda)
        {
            // Dicionário com os códigos e siglas das espécies de moeda
            var especiesMoeda = new Dictionary<string, string>
            {
                { "9", "BRL" }, // Real Brasileiro
                { "1", "USD" }, // Dólar Americano
                { "2", "EUR" }, // Euro
                { "3", "ARS" }, // Peso Argentino
                { "4", "GBP" }, // Libra Esterlina
                { "5", "CLP" }, // Peso Chileno
                { "6", "JPY" }, // Iene Japonês
                { "7", "CAD" }, // Dólar Canadense
                { "8", "AUD" }, // Dólar Australiano
            };

            return especiesMoeda.TryGetValue(codigoMoeda, out var sigla)
                ? sigla
                : "UNK"; // Sigla padrão para moeda desconhecida
        }

        /// <summary>
        /// Calcula o Código Cedente com base nos dados do boleto.
        /// </summary>
        /// <param name="dadosBoleto">Dicionário com os dados do boleto.</param>
        /// <returns>O Código Cedente como string.</returns>
        private string CalcularCodigoCedente(Dictionary<string, string> dadosBoleto)
        {
            // Valida se os dados necessários estão presentes
            if (!dadosBoleto.ContainsKey("AGENCIA") || string.IsNullOrWhiteSpace(dadosBoleto["AGENCIA"]))
                throw new ArgumentException("O campo 'AGENCIA' é obrigatório para calcular o Código Cedente.");

            if (!dadosBoleto.ContainsKey("NUMCDBARRA") || string.IsNullOrWhiteSpace(dadosBoleto["NUMCDBARRA"]))
                throw new ArgumentException("O campo 'NUMCDBARRA' é obrigatório para calcular o Código Cedente.");

            // Extrai os dados necessários
            string agencia = dadosBoleto["AGENCIA"].Trim();
            string numCdBarra = dadosBoleto["NUMCDBARRA"].Replace(" ", "").Trim();

            // Regra de cálculo: combina a Agência com os 6 primeiros dígitos do Nosso Número
            // (ajuste conforme o padrão do banco em uso)
            string nossoNumero = numCdBarra.Substring(0, 6); // Exemplo: pega os 6 primeiros dígitos

            // Concatena os valores para formar o Código Cedente
            string codigoCedente = $"{agencia}-{nossoNumero}";

            return codigoCedente;
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
