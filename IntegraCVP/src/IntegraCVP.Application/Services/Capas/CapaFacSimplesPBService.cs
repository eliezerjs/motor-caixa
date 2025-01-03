using IntegraCVP.Application.Dtos;
using IntegraCVP.Application.Enums;
using IntegraCVP.Application.Helper;
using IntegraCVP.Application.Interfaces;
using IntegraCVP.Application.Interfaces.Capas;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace IntegraCVP.Application.Services.Capas;

public partial class CapaFacSimplesPBService : ICapaFacSimplesPBService
{
    private readonly IImportFileConverterService _dataConverterService;
    public CapaFacSimplesPBService(IImportFileConverterService dataConverterService)
    {
        _dataConverterService = dataConverterService;
    }

    public async Task<byte[]> ConverterEGerarPdfAsync(IFormFile file, CapaFacPBType tipo)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("O arquivo enviado está vazio ou é inválido.");

        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        memoryStream.Position = 0;

        var jsonResult = _dataConverterService.ConvertToJson(memoryStream);

        var capaPb = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(jsonResult);

        if (capaPb == null || !capaPb.Any())
            throw new ArgumentException("O arquivo não contém dados válidos.");

        var capasPbFiltradas = capaPb
            .Where(e => e.ContainsKey("TIPO_DADO") && e["TIPO_DADO"] == tipo.ToString())
            .ToList();

        if (!capasPbFiltradas.Any())
            throw new ArgumentException($"Nenhum dado do tipo {tipo} foi encontrado no arquivo.");

        return GerarCapaFacSimplesPB(capasPbFiltradas.FirstOrDefault(), tipo);
    }
    public byte[] GerarCapaFacSimplesPB(Dictionary<string, string> dados, CapaFacPBType tipo)
    {
        string imagePath = GetImagePath("Capas");

        var campos = tipo switch
        {
            CapaFacPBType.VIDA01 => GetCamposVIDA01(),
            CapaFacPBType.VIDA02 => GetCamposVIDA02(),
            _ => throw new ArgumentException("Tipo de CAPA inválido.")
        };

        using var pdfStream = new MemoryStream();
        var (document, pdfDocument, pdfPage) = PdfHelper.InitializePdfDocument(imagePath, pdfStream);

        foreach (var (key, x, y, fontSize, isBold) in campos)
        {
            document.AddTextField(dados, key, x, y, fontSize, isBold, pdfPage);
        }

        if (dados.TryGetValue("NUMCDBARRA", out var codigoDeBarras))
        {
            string especieMoeda = PdfHelper.ObterEspecieMoeda(
                PdfHelper.ObterEspecieMoedaDoCodigoBarra(codigoDeBarras)
            );
            document.AddTextField(especieMoeda, 262, 585, 8, false, pdfPage);

            string codigoPadronizado = PdfHelper.MontarCodigoBarra(
                codigoDeBarras,
                PdfHelper.ObterFatorVencimento(dados["VENCIMENT"]),
                PdfHelper.ConverterValor(dados["VALOR"])
            );

            document.AddBarcode(pdfDocument, codigoPadronizado, 50, 77);
        }

        document.Close();
        return pdfStream.ToArray();
    }
}
//    public byte[] GerarCapaFacSimplesPB(Dictionary<string, string> dados, CapaFacPBType tipo)
//    {
//        //tratar dados json 

//        string campos = "";
//        int contaPedacos = 0;
//        int contaRegistros = 0;
//        int contaVa = 0;
//        string eLinha, eChave;
//        string formulario = "";
//        bool achou = false;

//        using var memoryStream = new MemoryStream();

//        using StreamReader reader = new(memoryStream);
//        while (!reader.EndOfStream)
//        {
//            eLinha = reader.ReadLine();
//            eLinha = eLinha.Replace("'", " ");
//            eLinha = eLinha.Replace("\"", " ");
//            eChave = eLinha.Length >= 5 ? eLinha[..5] : "";

//            if (eChave == "%%EOF")
//            {
//                contaVa = 0;
//                achou = false;
//            }

//            if (contaVa == 2)
//            {
//                string[] partes = eLinha.Split('|');
//                var linha = new DadoArquivoTextoDto
//                {
//                    PRODUTO = partes.Length > 0 ? partes[0] : "",
//                    COD_PROD = partes.Length > 1 ? partes[1] : "",
//                    PROC_SUSEP = partes.Length > 2 ? partes[2] : "",
//                    NOME_CLIENTE = partes.Length > 3 ? partes[3] : "",
//                    CPF = partes.Length > 4 ? partes[4] : "",
//                    DT_NASC = partes.Length > 5 ? partes[5] : "",
//                    PARCELA = partes.Length > 6 ? partes[6] : "",
//                    NUMCDBARRA = partes.Length > 7 ? partes[7] : "",
//                    DTVENCTO = partes.Length > 8 ? partes[8] : "",
//                    NCEDENTE = partes.Length > 9 ? partes[9] : "",
//                    CEDENTE = partes.Length > 10 ? partes[10] : "",
//                    DTDOCTO = partes.Length > 11 ? partes[11] : "",
//                    NUMDOCTO = partes.Length > 12 ? partes[12] : "",
//                    DTPROCESS = partes.Length > 13 ? partes[13] : "",
//                    NSNUMERO = partes.Length > 14 ? partes[14] : "",
//                    VALDOCTO = partes.Length > 15 ? partes[15] : "",
//                    MENSA1 = partes.Length > 16 ? partes[16] : "",
//                    MENSA2 = partes.Length > 17 ? partes[17] : "",
//                    MENSA3 = partes.Length > 18 ? partes[18] : "",
//                    MENSA4 = partes.Length > 19 ? partes[19] : "",
//                    MENSA5 = partes.Length > 20 ? partes[20] : "",
//                    MENSA6 = partes.Length > 21 ? partes[21] : "",
//                    MENSA7 = partes.Length > 22 ? partes[22] : "",
//                    VALCOBRADO = partes.Length > 23 ? partes[23] : "",
//                    CODBARRA = partes.Length > 24 ? partes[24] : "",
//                    SACADOR = partes.Length > 25 ? partes[25] : "",
//                    END_SAC = partes.Length > 26 ? partes[26] : "",
//                    CEP_SAC = partes.Length > 27 ? partes[27] : "",
//                    CGCCPFSUB = partes.Length > 28 ? partes[28] : "",
//                    DTPOSTAGEM = partes.Length > 29 ? partes[29] : "",
//                    NUMOBJETO = partes.Length > 30 ? partes[30] : "",
//                    DESTINATARIO = partes.Length > 31 ? partes[31] : "",
//                    ENDERECO = partes.Length > 32 ? partes[32] : "",
//                    BAIRRO = partes.Length > 33 ? partes[33] : "",
//                    CIDADE = partes.Length > 34 ? partes[34] : "",
//                    UF = partes.Length > 35 ? partes[35] : "",
//                    CEP = partes.Length > 36 ? partes[36] : "",
//                    REMETENTE = partes.Length > 37 ? partes[37] : "",
//                    REMET_ENDERECO = partes.Length > 38 ? partes[38] : "",
//                    REMET_BAIRRO = partes.Length > 39 ? partes[39] : "",
//                    REMET_CIDADE = partes.Length > 40 ? partes[40] : "",
//                    REMET_UF = partes.Length > 41 ? partes[41] : "",
//                    REMET_CEP = partes.Length > 42 ? partes[42] : "",
//                    PARCELA1 = partes.Length > 43 ? partes[43] : "",
//                    PARCELA2 = partes.Length > 44 ? partes[44] : "",
//                    PARCELA3 = partes.Length > 45 ? partes[45] : "",
//                    PARCELA4 = partes.Length > 46 ? partes[46] : "",
//                    PARCELA5 = partes.Length > 47 ? partes[47] : "",
//                    TEXTO01 = partes.Length > 48 ? partes[48] : "",
//                    TEXTO02 = partes.Length > 49 ? partes[49] : "",
//                    TEXTO03 = partes.Length > 50 ? partes[50] : "",
//                    TEXTO11 = partes.Length > 51 ? partes[51] : "",
//                    TEXTO12 = partes.Length > 52 ? partes[52] : "",
//                    TEXTO13 = partes.Length > 53 ? partes[53] : "",
//                    TEXTO14 = partes.Length > 54 ? partes[54] : "",
//                    TEXTO15 = partes.Length > 55 ? partes[55] : "",
//                    TEXTO21 = partes.Length > 56 ? partes[56] : "",
//                    TEXTO22 = partes.Length > 57 ? partes[57] : "",
//                    TEXTO23 = partes.Length > 58 ? partes[58] : "",
//                    TEXTO24 = partes.Length > 59 ? partes[59] : "",
//                    TEXTO25 = partes.Length > 60 ? partes[60] : "",
//                    TEXTO31 = partes.Length > 61 ? partes[61] : "",
//                    TEXTO32 = partes.Length > 62 ? partes[62] : "",
//                    TEXTO33 = partes.Length > 63 ? partes[63] : "",
//                    TEXTO34 = partes.Length > 64 ? partes[64] : "",
//                    TEXTO35 = partes.Length > 65 ? partes[65] : "",
//                    TEXTO41 = partes.Length > 66 ? partes[66] : "",
//                    TEXTO42 = partes.Length > 67 ? partes[67] : "",
//                    TEXTO43 = partes.Length > 68 ? partes[68] : "",
//                    TEXTO44 = partes.Length > 69 ? partes[69] : "",
//                    TEXTO45 = partes.Length > 70 ? partes[70] : "",
//                    TEXTO51 = partes.Length > 71 ? partes[71] : "",
//                    TEXTO52 = partes.Length > 72 ? partes[72] : "",
//                    TEXTO53 = partes.Length > 73 ? partes[73] : "",
//                    TEXTO54 = partes.Length > 74 ? partes[74] : "",
//                    TEXTO55 = partes.Length > 75 ? partes[75] : "",
//                    TEXTO91 = partes.Length > 76 ? partes[76] : "",
//                    TEXTO92 = partes.Length > 77 ? partes[77] : "",
//                    TEXTO93 = partes.Length > 78 ? partes[78] : "",
//                    TEXTO94 = partes.Length > 79 ? partes[79] : "",
//                    TEXTO95 = partes.Length > 80 ? partes[80] : "",
//                    VALOR = partes.Length > 81 ? partes[81] : "",
//                    VALOR1 = partes.Length > 82 ? partes[82] : "",
//                    VL_MORTE = partes.Length > 83 ? partes[83] : "",
//                    VL_ACID = partes.Length > 84 ? partes[84] : "",
//                    VL_INVAL = partes.Length > 85 ? partes[85] : "",
//                    VL_CUSTO = partes.Length > 86 ? partes[86] : "",
//                    CODIGO_CIF = partes.Length > 87 ? partes[87] : "",
//                    POSTNET = partes.Length > 88 ? partes[88] : "",
//                    NUM_CERTIFICADO = partes.Length > 89 ? partes[89] : "",
//                    LINHACOMPLETA = eLinha,
//                    FORM = formulario
//                };
//                contaRegistros++;
//            }

//            if (eChave.ToUpper() == "(VA18")
//            {
//                formulario = "VA18";
//                contaVa++;
//                achou = true;
//            }
//            else if (eChave.ToUpper() == "(VA24")
//            {
//                formulario = "VA24";
//                contaVa++;
//                achou = true;
//            }
//            else if (eLinha.ToUpper().Contains("(VIDA23.DBM)"))
//            {
//                formulario = "VIDA23";
//                contaVa++;
//                achou = true;
//            }
//            else if (eLinha.ToUpper().Contains("(VIDA24.DBM)"))
//            {
//                formulario = "VIDA24";
//                contaVa++;
//                achou = true;
//            }
//            else if (eChave == "PRODU" && achou)
//            {
//                contaVa++;
//            }

//            return null;
//        }
//        return null;
//    }
//}
