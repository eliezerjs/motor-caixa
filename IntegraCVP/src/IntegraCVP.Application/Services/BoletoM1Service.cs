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
        private readonly IImportFileConverterService _dataConverterService;

        public BoletoM1Service(IImportFileConverterService dataConverterService)
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
        public byte[] GerarBoletoM1(Dictionary<string, string> dadosBoleto, BoletoM1Type tipo)
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
