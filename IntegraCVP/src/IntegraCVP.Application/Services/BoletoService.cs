﻿using IntegraCVP.Application.Interfaces;
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
using Org.BouncyCastle.Crypto;
using System.Runtime.ConstrainedExecution;


namespace IntegraCVP.Application.Services
{
    public class BoletoService : IBoletoService
    {
        public byte[] GerarBoleto2Pdf(Dictionary<string, string> dadosBoleto)
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

        public byte[] GerarBoleto25Pdf(Dictionary<string, string> dadosBoleto)
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

        public byte[] GerarBoleto18Pdf(Dictionary<string, string> dadosBoleto)
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

            DesenharCampo("PRODUTO", 57, 69);
            DesenharCampo("COD_PROD", 310, 69);
            DesenharCampo("PROC_SUSEP", 410, 69);

            DesenharCampo("NOME_CLIENTE", 55, 110);
            DesenharCampo("CPF", 340, 110);
            DesenharCampo("DT_NASC", 453, 110);




            DesenharCampo("NUMDOCTO", 470, 400);

            DesenharCampo("DTVENCTO", 350, 425);
            DesenharCampo("DTVENCTO", 468, 425);

            DesenharCampo("NSNUMERO", 380, 445);
            DesenharCampo("DTVENCTO", 465, 445);

            DesenharCampo("DTVENCTO", 371, 480);

            DesenharCampo("DTVENCTO", 440, 528);
            DesenharCampo("DTVENCTO", 492, 528);

            DesenharCampo("NSNUMERO", 443, 545);

            DesenharCampo("DTVENCTO", 443, 562);

            DesenharCampo("DTVENCTO", 443, 580);

            DesenharCampo("DTVENCTO", 443, 595);

            DesenharCampo("DTVENCTO", 443, 610);

            DesenharCampo("DTVENCTO", 443, 627);

            DesenharCampo("DTVENCTO", 443, 643);

            DesenharCampo("DTVENCTO", 443, 660);

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
