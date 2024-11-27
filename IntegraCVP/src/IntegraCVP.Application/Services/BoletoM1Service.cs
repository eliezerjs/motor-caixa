using System.Text.Json;
using IntegraCVP.Application.Enums;
using IntegraCVP.Application.Helper;
using IntegraCVP.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace IntegraCVP.Application.Services
{
    public partial class BoletoM1Service : IBoletoM1Service
    {
        public const string BoletoM1 = "BoletoM1";
        private readonly IReturnDataConverterService _dataConverterService;

        public BoletoM1Service(IReturnDataConverterService dataConverterService)
        {
            _dataConverterService = dataConverterService;
        }

        public async Task<byte[]> ConverterEGerarPdfAsync(IFormFile file, BoletoM1Type tipo)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("O arquivo enviado está vazio ou é inválido.");

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            var jsonResult = _dataConverterService.ConvertToJson(memoryStream);

            var boletoData = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(jsonResult);

            if (boletoData == null || !boletoData.Any())
                throw new ArgumentException("O arquivo não contém dados válidos.");

            var boletosFiltrados = boletoData
                .Where(b => b.ContainsKey("TIPO_DADO") && b["TIPO_DADO"] == tipo.ToString())
                .ToList();

            if (!boletosFiltrados.Any())
                throw new ArgumentException($"Nenhum dado do tipo {tipo} foi encontrado no arquivo.");

            return GerarBoletoM1(boletosFiltrados.FirstOrDefault(), tipo);
        }
        private byte[] GerarBoletoM1(Dictionary<string, string> dadosBoleto, BoletoM1Type tipo)
        {
            string imagePath = GetImagePath(tipo, BoletoM1);
            
            var campos = tipo switch
            {
                BoletoM1Type.VD02 => GetCamposVD02(),
                BoletoM1Type.VIDA25 => GetCamposVIDA25(),                
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
                string especieMoeda = PdfHelper.ObterEspecieMoeda(
                    PdfHelper.ObterEspecieMoedaDoCodigoBarra(codigoDeBarras)
                );
                document.AddTextField(especieMoeda, 262, 585, 8, pdfPage);

                string codigoPadronizado = PdfHelper.MontarCodigoBarra(
                    codigoDeBarras,
                    PdfHelper.ObterFatorVencimento(dadosBoleto["VENCIMENT"]),
                    PdfHelper.ConverterValor(dadosBoleto["VALOR"])
                );

                document.AddBarcode(pdfDocument, codigoPadronizado, 50, 130);
            }

            document.Close();
            return pdfStream.ToArray();
        }

        
        //public byte[] GerarBoletoVIDA23Pdf(Dictionary<string, string> dadosBoleto)
        //{
        //    // Caminho da imagem de fundo
        //    string imagePath = System.IO.Path.Combine(AppContext.BaseDirectory, "Resources", "Seguro_Grupo", "VIDA23.jpg");

        //    if (!File.Exists(imagePath))
        //    {
        //        throw new FileNotFoundException($"A imagem de fundo não foi encontrada no caminho: {imagePath}");
        //    }

        //    using var pdfStream = new MemoryStream();
        //    var writer = new PdfWriter(pdfStream);
        //    var pdfDocument = new PdfDocument(writer);
        //    var document = new iText.Layout.Document(pdfDocument);
        //    var pdfPage = pdfDocument.AddNewPage(PageSize.A4);

        //    // Adiciona a imagem de fundo
        //    byte[] imageBytes = File.ReadAllBytes(imagePath);
        //    var imageData = iText.IO.Image.ImageDataFactory.Create(imageBytes);
        //    var image = new iText.Layout.Element.Image(imageData);
        //    image.ScaleToFit(pdfDocument.GetDefaultPageSize().GetWidth(), pdfDocument.GetDefaultPageSize().GetHeight());
        //    image.SetFixedPosition(0, 0); // Define a posição
        //    document.Add(image);

        //    // Função auxiliar para adicionar texto
        //    void DesenharCampo(string chave, float x, float y)
        //    {
        //        if (dadosBoleto.ContainsKey(chave))
        //        {
        //            var text = new Paragraph(dadosBoleto[chave])
        //                .SetFontSize(9)
        //                .SetFixedPosition(x, pdfPage.GetPageSize().GetHeight() - y, 200);
        //            document.Add(text);
        //        }
        //    }

        //    // Campos a desenhar

        //    DesenharCampo("PRODUTO", 57, 68);
        //    DesenharCampo("COD_PROD", 307, 68);
        //    DesenharCampo("PROC_SUSEP", 393, 68);

        //    DesenharCampo("NOME_CLIENTE", 55, 110);
        //    DesenharCampo("CPF", 327, 110);
        //    DesenharCampo("DT_NASC", 453, 110);


        //    DesenharCampo("NUMDOCTO", 470, 390);

        //    DesenharCampo("CEDENTE", 359, 410);
        //    DesenharCampo("DTVENCTO", 467, 410);

        //    DesenharCampo("NSNUMERO", 368, 430);
        //    DesenharCampo("VALDOCTO", 465, 430);

        //    DesenharCampo("NUMOBJETO", 371, 463);

        //    DesenharCampo("PARCELA", 440, 514);
        //    DesenharCampo("DTVENCTO", 492, 514);

        //    DesenharCampo("CEDENTE", 440, 532);

        //    DesenharCampo("DTDOCTO", 46, 549);
        //    DesenharCampo("NUMDOCTO", 133, 549);
        //    DesenharCampo("DTPROCESS", 370, 549);
        //    DesenharCampo("NSNUMERO", 440, 549);

        //    DesenharCampo("", 46, 566);
        //    DesenharCampo("", 299, 566);
        //    DesenharCampo("VALOR", 370, 566);

        //    DesenharCampo("VALDOCTO", 440, 566);

        //    DesenharCampo("", 440, 581);

        //    DesenharCampo("ABATIMENTO", 440, 597);

        //    DesenharCampo("", 440, 613);

        //    DesenharCampo("", 440, 630);

        //    DesenharCampo("VALDOCTO", 440, 646);


        //    // Gera e adiciona o código de barras
        //    if (dadosBoleto.ContainsKey("NUMCDBARRA"))
        //    {
        //        string codigoPadronizado = MontarCodigoBarra(
        //           dadosBoleto["NUMCDBARRA"],
        //           ObterFatorVencimento(dadosBoleto["DTVENCTO"]),
        //           ConverterValor(dadosBoleto["VALOR"])
        //       );


        //        var barcode = new Barcode128(pdfDocument);
        //        barcode.SetCode(codigoPadronizado);
        //        barcode.SetBarHeight(30); // Altura das barras
        //        barcode.SetX(1f); // Largura das barras
        //        var barcodeImage = new iText.Layout.Element.Image(barcode.CreateFormXObject(pdfDocument));
        //        barcodeImage.SetFixedPosition(50, 145); // Ajuste a posição
        //        document.Add(barcodeImage);
        //    }

        //    document.Close();
        //    return pdfStream.ToArray();
        //}

        //public byte[] GerarBoletoVIDA24Pdf(Dictionary<string, string> dadosBoleto)
        //{
        //    // Caminho da imagem de fundo
        //    string imagePath = System.IO.Path.Combine(AppContext.BaseDirectory, "Resources", "Seguro_Grupo", "VIDA24.jpg");

        //    if (!File.Exists(imagePath))
        //    {
        //        throw new FileNotFoundException($"A imagem de fundo não foi encontrada no caminho: {imagePath}");
        //    }

        //    using var pdfStream = new MemoryStream();
        //    var writer = new PdfWriter(pdfStream);
        //    var pdfDocument = new PdfDocument(writer);
        //    var document = new iText.Layout.Document(pdfDocument);
        //    var pdfPage = pdfDocument.AddNewPage(PageSize.A4);

        //    // Adiciona a imagem de fundo
        //    byte[] imageBytes = File.ReadAllBytes(imagePath);
        //    var imageData = iText.IO.Image.ImageDataFactory.Create(imageBytes);
        //    var image = new iText.Layout.Element.Image(imageData);
        //    image.ScaleToFit(pdfDocument.GetDefaultPageSize().GetWidth(), pdfDocument.GetDefaultPageSize().GetHeight());
        //    image.SetFixedPosition(0, 0); // Define a posição
        //    document.Add(image);

        //    // Função auxiliar para adicionar texto
        //    void DesenharCampo(string chave, float x, float y)
        //    {
        //        if (dadosBoleto.ContainsKey(chave))
        //        {
        //            var text = new Paragraph(dadosBoleto[chave])
        //                .SetFontSize(9)
        //                .SetFixedPosition(x, pdfPage.GetPageSize().GetHeight() - y, 200);
        //            document.Add(text);
        //        }
        //    }

        //    // Campos a desenhar

        //    DesenharCampo("PRODUTO", 57, 81);
        //    DesenharCampo("COD_PROD", 308, 81);
        //    DesenharCampo("PROC_SUSEP", 394, 81);

        //    DesenharCampo("NOME_CLIENTE", 56, 129);
        //    DesenharCampo("CPF", 327, 129);
        //    DesenharCampo("DT_NASC", 454, 129);


        //    DesenharCampo("NUMDOCTO", 470, 459);

        //    DesenharCampo("CEDENTE", 359, 479);
        //    DesenharCampo("DTVENCTO", 467, 479);

        //    DesenharCampo("NSNUMERO", 368, 499);
        //    DesenharCampo("VALDOCTO", 465, 499);

        //    DesenharCampo("NUMOBJETO", 371, 535);

        //    DesenharCampo("PARCELA", 440, 586);
        //    DesenharCampo("DTVENCTO", 492, 586);

        //    DesenharCampo("CEDENTE", 440, 604);

        //    DesenharCampo("DTDOCTO", 46, 621);
        //    DesenharCampo("NUMDOCTO", 133, 621);
        //    DesenharCampo("DTPROCESS", 370, 621);
        //    DesenharCampo("NSNUMERO", 440, 621);

        //    DesenharCampo("", 46, 638);
        //    DesenharCampo("", 299, 638);
        //    DesenharCampo("VALOR", 370, 638);
        //    DesenharCampo("VALDOCTO", 440, 638);

        //    DesenharCampo("PARCELA", 440, 653);

        //    DesenharCampo("PARCELA", 440, 669);

        //    DesenharCampo("PARCELA", 440, 685);

        //    DesenharCampo("PARCELA", 440, 701);

        //    DesenharCampo("VALDOCTO", 440, 718);


        //    // Gera e adiciona o código de barras
        //    if (dadosBoleto.ContainsKey("NUMCDBARRA"))
        //    {
        //        string codigoPadronizado = MontarCodigoBarra(
        //           dadosBoleto["NUMCDBARRA"],
        //           ObterFatorVencimento(dadosBoleto["DTVENCTO"]),
        //           ConverterValor(dadosBoleto["VALOR"])
        //       );


        //        var barcode = new Barcode128(pdfDocument);
        //        barcode.SetCode(codigoPadronizado);
        //        barcode.SetBarHeight(30); 
        //        barcode.SetX(1f); 
        //        var barcodeImage = new iText.Layout.Element.Image(barcode.CreateFormXObject(pdfDocument));
        //        barcodeImage.SetFixedPosition(50, 78);
        //        document.Add(barcodeImage);
        //    }

        //    document.Close();
        //    return pdfStream.ToArray();
        //}
        
        ////CartaRecusa
        //public byte[] GerarBoletoVIDA01Pdf(Dictionary<string, string> dadosBoleto)
        //{
        //    // Caminho da imagem de fundo
        //    string imagePath = System.IO.Path.Combine(AppContext.BaseDirectory, "Resources", "CartaRecusa", "VIDA01.jpg");

        //    if (!File.Exists(imagePath))
        //    {
        //        throw new FileNotFoundException($"A imagem de fundo não foi encontrada no caminho: {imagePath}");
        //    }

        //    using var pdfStream = new MemoryStream();
        //    var writer = new PdfWriter(pdfStream);
        //    var pdfDocument = new PdfDocument(writer);
        //    var document = new iText.Layout.Document(pdfDocument);
        //    var pdfPage = pdfDocument.AddNewPage(PageSize.A4);

        //    // Adiciona a imagem de fundo
        //    byte[] imageBytes = File.ReadAllBytes(imagePath);
        //    var imageData = iText.IO.Image.ImageDataFactory.Create(imageBytes);
        //    var image = new iText.Layout.Element.Image(imageData);
        //    image.ScaleToFit(pdfDocument.GetDefaultPageSize().GetWidth(), pdfDocument.GetDefaultPageSize().GetHeight());
        //    image.SetFixedPosition(0, 0); // Define a posição
        //    document.Add(image);

        //    // Função auxiliar para adicionar texto
        //    void DesenharCampo(string chave, float x, float y)
        //    {
        //        if (dadosBoleto.ContainsKey(chave))
        //        {
        //            var text = new Paragraph(dadosBoleto[chave])
        //                .SetFontSize(7)
        //                .SetFixedPosition(x, pdfPage.GetPageSize().GetHeight() - y, 200);
        //            document.Add(text);
        //        }
        //    }

        //    void DesenharCampoManual(string campo, float x, float y)
        //    {
        //        var text = new Paragraph(campo)
        //            .SetFontSize(8)

        //            .SetFixedPosition(x, pdfPage.GetPageSize().GetHeight() - y, 200);
        //        document.Add(text);
        //    }

        //    // Campos a desenhar

        //    //DesenharCampo("NOME_CLIENTE", 57, 81);
        //    DesenharCampoManual("Luana", 77, 140);

        //    DesenharCampo("NUM_PROPOSTA", 382, 166);

        //    DesenharCampo("DATA_DECLINIO", 115, 177);

        //    DesenharCampo("COD_PRODUTO", 310, 728);

        //    DesenharCampo("COD_SUSEP", 55, 738);
        //    DesenharCampo("", 145, 738);


        //    document.Close();
        //    return pdfStream.ToArray();
        //}
        //public byte[] GerarBoletoVIDA02Pdf(Dictionary<string, string> dadosBoleto)
        //{
        //    // Caminho da imagem de fundo
        //    string imagePath = System.IO.Path.Combine(AppContext.BaseDirectory, "Resources", "CartaRecusa", "VIDA02.jpg");

        //    if (!File.Exists(imagePath))
        //    {
        //        throw new FileNotFoundException($"A imagem de fundo não foi encontrada no caminho: {imagePath}");
        //    }

        //    using var pdfStream = new MemoryStream();
        //    var writer = new PdfWriter(pdfStream);
        //    var pdfDocument = new PdfDocument(writer);
        //    var document = new iText.Layout.Document(pdfDocument);
        //    var pdfPage = pdfDocument.AddNewPage(PageSize.A4);

        //    // Adiciona a imagem de fundo
        //    byte[] imageBytes = File.ReadAllBytes(imagePath);
        //    var imageData = iText.IO.Image.ImageDataFactory.Create(imageBytes);
        //    var image = new iText.Layout.Element.Image(imageData);
        //    image.ScaleToFit(pdfDocument.GetDefaultPageSize().GetWidth(), pdfDocument.GetDefaultPageSize().GetHeight());
        //    image.SetFixedPosition(0, 0); // Define a posição
        //    document.Add(image);

        //    // Função auxiliar para adicionar texto
        //    void DesenharCampo(string chave, float x, float y)
        //    {
        //        if (dadosBoleto.ContainsKey(chave))
        //        {
        //            var text = new Paragraph(dadosBoleto[chave])
        //                .SetFontSize(7)
        //                .SetFixedPosition(x, pdfPage.GetPageSize().GetHeight() - y, 200);
        //            document.Add(text);
        //        }
        //    }

        //    void DesenharCampoManual(string campo, float x, float y)
        //    {
        //        var text = new Paragraph(campo)
        //            .SetFontSize(8)

        //            .SetFixedPosition(x, pdfPage.GetPageSize().GetHeight() - y, 200);
        //        document.Add(text);
        //    }

        //    // Campos a desenhar

        //    //DesenharCampo("NOME_CLIENTE", 57, 81);
        //    DesenharCampoManual("Luana", 77, 140);

        //    DesenharCampo("NUM_PROPOSTA", 382, 166);

        //    DesenharCampo("DATA_DECLINIO", 115, 177);

        //    DesenharCampo("COD_PRODUTO", 344, 728);

        //    DesenharCampo("COD_SUSEP", 55, 738);


        //    document.Close();
        //    return pdfStream.ToArray();
        //}
        //public byte[] GerarBoletoVIDA03Pdf(Dictionary<string, string> dadosBoleto)
        //{
        //    // Caminho da imagem de fundo
        //    string imagePath = System.IO.Path.Combine(AppContext.BaseDirectory, "Resources", "CartaRecusa", "VIDA03.jpg");

        //    if (!File.Exists(imagePath))
        //    {
        //        throw new FileNotFoundException($"A imagem de fundo não foi encontrada no caminho: {imagePath}");
        //    }

        //    using var pdfStream = new MemoryStream();
        //    var writer = new PdfWriter(pdfStream);
        //    var pdfDocument = new PdfDocument(writer);
        //    var document = new iText.Layout.Document(pdfDocument);
        //    var pdfPage = pdfDocument.AddNewPage(PageSize.A4);

        //    // Adiciona a imagem de fundo
        //    byte[] imageBytes = File.ReadAllBytes(imagePath);
        //    var imageData = iText.IO.Image.ImageDataFactory.Create(imageBytes);
        //    var image = new iText.Layout.Element.Image(imageData);
        //    image.ScaleToFit(pdfDocument.GetDefaultPageSize().GetWidth(), pdfDocument.GetDefaultPageSize().GetHeight());
        //    image.SetFixedPosition(0, 0); // Define a posição
        //    document.Add(image);

        //    // Função auxiliar para adicionar texto
        //    void DesenharCampo(string chave, float x, float y)
        //    {
        //        if (dadosBoleto.ContainsKey(chave))
        //        {
        //            var text = new Paragraph(dadosBoleto[chave])
        //                .SetFontSize(7)
        //                .SetFixedPosition(x, pdfPage.GetPageSize().GetHeight() - y, 200);
        //            document.Add(text);
        //        }
        //    }

        //    void DesenharCampoManual(string campo, float x, float y)
        //    {
        //        var text = new Paragraph(campo)
        //            .SetFontSize(8)

        //            .SetFixedPosition(x, pdfPage.GetPageSize().GetHeight() - y, 200);
        //        document.Add(text);
        //    }

        //    // Campos a desenhar

        //    //DesenharCampo("NOME_CLIENTE", 57, 81);
        //    DesenharCampoManual("Luana", 77, 140);

        //    DesenharCampo("NUM_PROPOSTA", 382, 166);

        //    DesenharCampo("DATA_DECLINIO", 115, 177);

        //    DesenharCampo("COD_PRODUTO", 297, 740);

        //    DesenharCampo("COD_SUSEP", 373, 740);


        //    document.Close();
        //    return pdfStream.ToArray();
        //}
        //public byte[] GerarBoletoVIDA04Pdf(Dictionary<string, string> dadosBoleto)
        //{
        //    // Caminho da imagem de fundo
        //    string imagePath = System.IO.Path.Combine(AppContext.BaseDirectory, "Resources", "CartaRecusa", "VIDA04.jpg");

        //    if (!File.Exists(imagePath))
        //    {
        //        throw new FileNotFoundException($"A imagem de fundo não foi encontrada no caminho: {imagePath}");
        //    }

        //    using var pdfStream = new MemoryStream();
        //    var writer = new PdfWriter(pdfStream);
        //    var pdfDocument = new PdfDocument(writer);
        //    var document = new iText.Layout.Document(pdfDocument);
        //    var pdfPage = pdfDocument.AddNewPage(PageSize.A4);

        //    // Adiciona a imagem de fundo
        //    byte[] imageBytes = File.ReadAllBytes(imagePath);
        //    var imageData = iText.IO.Image.ImageDataFactory.Create(imageBytes);
        //    var image = new iText.Layout.Element.Image(imageData);
        //    image.ScaleToFit(pdfDocument.GetDefaultPageSize().GetWidth(), pdfDocument.GetDefaultPageSize().GetHeight());
        //    image.SetFixedPosition(0, 0); // Define a posição
        //    document.Add(image);

        //    // Função auxiliar para adicionar texto
        //    void DesenharCampo(string chave, float x, float y)
        //    {
        //        if (dadosBoleto.ContainsKey(chave))
        //        {
        //            var text = new Paragraph(dadosBoleto[chave])
        //                .SetFontSize(7)
        //                .SetFixedPosition(x, pdfPage.GetPageSize().GetHeight() - y, 200);
        //            document.Add(text);
        //        }
        //    }

        //    void DesenharCampoManual(string campo, float x, float y)
        //    {
        //        var text = new Paragraph(campo)
        //            .SetFontSize(8)

        //            .SetFixedPosition(x, pdfPage.GetPageSize().GetHeight() - y, 200);
        //        document.Add(text);
        //    }

        //    // Campos a desenhar

        //    //DesenharCampo("NOME_CLIENTE", 57, 81);
        //    DesenharCampoManual("Luana", 77, 140);

        //    DesenharCampo("NUM_PROPOSTA", 382, 166);

        //    DesenharCampo("DATA_DECLINIO", 115, 176);

        //    DesenharCampo("COD_PRODUTO", 296, 736);

        //    DesenharCampo("COD_SUSEP", 373, 736);


        //    document.Close();
        //    return pdfStream.ToArray();
        //}

        ////CartaRecusa
        //public byte[] GerarBoletoVD33Pdf(Dictionary<string, string> dadosBoleto)
        //{
        //    // Caminho da imagem de fundo
        //    string imagePath = System.IO.Path.Combine(AppContext.BaseDirectory, "Resources", "Inadimplente", "VD33.jpg");

        //    if (!File.Exists(imagePath))
        //    {
        //        throw new FileNotFoundException($"A imagem de fundo não foi encontrada no caminho: {imagePath}");
        //    }

        //    using var pdfStream = new MemoryStream();
        //    var writer = new PdfWriter(pdfStream);
        //    var pdfDocument = new PdfDocument(writer);
        //    var document = new iText.Layout.Document(pdfDocument);
        //    var pdfPage = pdfDocument.AddNewPage(PageSize.A4);

        //    // Adiciona a imagem de fundo
        //    byte[] imageBytes = File.ReadAllBytes(imagePath);
        //    var imageData = iText.IO.Image.ImageDataFactory.Create(imageBytes);
        //    var image = new iText.Layout.Element.Image(imageData);
        //    image.ScaleToFit(pdfDocument.GetDefaultPageSize().GetWidth(), pdfDocument.GetDefaultPageSize().GetHeight());
        //    image.SetFixedPosition(0, 0); // Define a posição
        //    document.Add(image);

        //    // Função auxiliar para adicionar texto
        //    void DesenharCampo(string chave, float x, float y)
        //    {
        //        if (dadosBoleto.ContainsKey(chave))
        //        {
        //            var text = new Paragraph(dadosBoleto[chave])
        //                .SetFontSize(7)
        //                .SetFixedPosition(x, pdfPage.GetPageSize().GetHeight() - y, 200);
        //            document.Add(text);
        //        }
        //    }

        //    void DesenharCampoManual(string campo, float x, float y)
        //    {
        //        var text = new Paragraph(campo)
        //            .SetFontSize(8)

        //            .SetFixedPosition(x, pdfPage.GetPageSize().GetHeight() - y, 200);
        //        document.Add(text);
        //    }

        //    // Campos a desenhar

        //    //DesenharCampo("NOME_CLIENTE", 57, 81);
        //    DesenharCampoManual("Luana", 382, 166);

        //    DesenharCampo("DATA_DECLINIO", 115, 176);

        //    DesenharCampo("COD_PRODUTO", 296, 736);

        //    DesenharCampo("COD_SUSEP", 373, 736);


        //    document.Close();
        //    return pdfStream.ToArray();
        //}

        ////Pasta Seguro
        //public byte[] GerarBoletoSeguro(Dictionary<string, string> dadosBoleto, string filename)
        //{
        //    // Caminho da imagem de fundo
        //    string imagePath = System.IO.Path.Combine(AppContext.BaseDirectory, "Resources", "Seguro", filename + ".jpg");

        //    if (!File.Exists(imagePath))
        //    {
        //        throw new FileNotFoundException($"A imagem de fundo não foi encontrada no caminho: {imagePath}");
        //    }

        //    using var pdfStream = new MemoryStream();
        //    var writer = new PdfWriter(pdfStream);
        //    var pdfDocument = new PdfDocument(writer);
        //    var document = new iText.Layout.Document(pdfDocument);
        //    var pdfPage = pdfDocument.AddNewPage(PageSize.A4);

        //    // Adiciona a imagem de fundo
        //    byte[] imageBytes = File.ReadAllBytes(imagePath);
        //    var imageData = iText.IO.Image.ImageDataFactory.Create(imageBytes);
        //    var image = new iText.Layout.Element.Image(imageData);
        //    image.ScaleToFit(pdfDocument.GetDefaultPageSize().GetWidth(), pdfDocument.GetDefaultPageSize().GetHeight());
        //    image.SetFixedPosition(0, 0); // Define a posição
        //    document.Add(image);

        //    // Função auxiliar para adicionar texto
        //    void DesenharCampo(string chave, float x, float y)
        //    {
        //        if (dadosBoleto.ContainsKey(chave))
        //        {
        //            var text = new Paragraph(dadosBoleto[chave])
        //                .SetFontSize(8)
        //                .SetFixedPosition(x, pdfPage.GetPageSize().GetHeight() - y, 200);
        //            document.Add(text);
        //        }
        //    }

        //    // Campos a desenhar
        //    var colunaInicial = 47;
        //    var linha1 = 186;
        //    DesenharCampo("AGENCIA", colunaInicial, linha1);
        //    DesenharCampo("APOLICE", 138, linha1);
        //    DesenharCampo("FATURA", 210, linha1);
        //    DesenharCampo("PERIODO", 282, linha1);
        //    DesenharCampo("EMISSAO", 409, linha1);
        //    DesenharCampo("VENCIMENT", 484, linha1);

        //    var linha2 = 217;
        //    DesenharCampo("ESTIPULANTE", colunaInicial, linha2);
        //    DesenharCampo("CNPJ1", 340, linha2);

        //    var linha3 = 251;
        //    DesenharCampo("ENDERE1", colunaInicial, linha3);

        //    var linha4 = 284;
        //    DesenharCampo("CEP1", colunaInicial, linha4);
        //    DesenharCampo("CIDADE1", 157, linha4);
        //    DesenharCampo("EST1", 488, linha4);

        //    var linha5 = 317;
        //    DesenharCampo("SUBESTIPULANTE", colunaInicial, linha5);
        //    DesenharCampo("CNPJ2", 396, linha5);

        //    var linha6 = 350;
        //    DesenharCampo("ENDERE2", colunaInicial, linha6);

        //    var linha7 = 385;
        //    DesenharCampo("CEP2", colunaInicial, linha7);
        //    DesenharCampo("CIDADE2", 156, linha7);
        //    DesenharCampo("EST2", 480, linha7);

        //    var linha8 = 418;
        //    DesenharCampo("NVIDAS", colunaInicial, linha8);
        //    DesenharCampo("CAPITAL", 156, linha8);
        //    DesenharCampo("IOF", 312, linha8);
        //    DesenharCampo("PREMIO", 400, linha8);

        //    document.Close();
        //    return pdfStream.ToArray();
        //}

    }
}
