using IntegraCVP.Application.Enums;

namespace IntegraCVP.Application.Services
{
    public partial class BoletoM4Service
    {
        public List<(string Key, float X, float Y, float FontSize)> GetCamposVA18()
        {
            return new List<(string Key, float X, float Y, float FontSize)>
            {
                ("PRODUTO", 57, 68, 9),
                ("COD_PROD", 306, 69, 9),
                ("PROC_SUSEP", 392, 69, 9),
                ("NOME_CLIENTE", 55, 110, 9),
                ("CPF", 327, 110, 9),
                ("DT_NASC", 453, 110, 9),
                ("NUMDOCTO", 470, 405, 9),
                ("CEDENTE", 359, 424, 9),
                ("DTVENCTO", 467, 424, 9),
                ("NSNUMERO", 368, 444, 9),
                ("VALDOCTO", 465, 444, 9),
                ("NUMOBJETO", 371, 480, 9),
                ("PARCELA", 440, 528, 9),
                ("DTVENCTO", 492, 528, 9),
                ("CEDENTE", 440, 546, 9),
                ("DTDOCTO", 46, 563, 9),
                ("NUMDOCTO", 133, 563, 9),
                ("DTPROCESS", 370, 563, 9),
                ("NSNUMERO", 440, 563, 9),
                ("VALOR", 370, 580, 9),
                ("VALDOCTO", 440, 580, 9),
                ("ABATIMENTO", 440, 610, 9),
                ("VALDOCTO", 440, 660, 9)
            };
        }

        public List<(string Key, float X, float Y, float FontSize)> GetCamposVA24()
        {
            return new List<(string Key, float X, float Y, float FontSize)>
            {
                ("PRODUTO", 57, 87, 9),
                ("COD_PROD", 308, 87, 9),
                ("PROC_SUSEP", 394, 87, 9),
                ("NOME_CLIENTE", 56, 132, 9),
                ("CPF", 327, 132, 9),
                ("DT_NASC", 454, 132, 9),
                ("NUMDOCTO", 470, 364, 9),
                ("CEDENTE", 359, 384, 9),
                ("DTVENCTO", 467, 384, 9),
                ("NSNUMERO", 368, 404, 9),
                ("VALDOCTO", 465, 404, 9),
                ("NUMOBJETO", 371, 440, 9),
                ("PARCELA", 440, 491, 9),
                ("DTVENCTO", 492, 491, 9),
                ("CEDENTE", 440, 509, 9),
                ("DTDOCTO", 46, 526, 9),
                ("NUMDOCTO", 133, 526, 9),
                ("DTPROCESS", 370, 526, 9),
                ("NSNUMERO", 440, 526, 9),
                ("VALOR", 370, 543, 9),
                ("VALDOCTO", 440, 543, 9),
                ("ABATIMENTO", 440, 574, 9),
                ("VALDOCTO", 440, 623, 9)
            };
        }

        public List<(string Key, float X, float Y, float FontSize)> GetCamposVIDA23()
        {
            return new List<(string Key, float X, float Y, float FontSize)>
            {
                ("PRODUTO", 57, 68, 9),
                ("COD_PROD", 307, 68, 9),
                ("PROC_SUSEP", 393, 68, 9),
                ("NOME_CLIENTE", 55, 110, 9),
                ("CPF", 327, 110, 9),
                ("DT_NASC", 453, 110, 9),
                ("NUMDOCTO", 470, 390, 9),
                ("CEDENTE", 359, 410, 9),
                ("DTVENCTO", 467, 410, 9),
                ("NSNUMERO", 368, 430, 9),
                ("VALDOCTO", 465, 430, 9),
                ("NUMOBJETO", 371, 463, 9),
                ("PARCELA", 440, 514, 9),
                ("DTVENCTO", 492, 514, 9),
                ("CEDENTE", 440, 532, 9),
                ("DTDOCTO", 46, 549, 9),
                ("NUMDOCTO", 133, 549, 9),
                ("DTPROCESS", 370, 549, 9),
                ("NSNUMERO", 440, 549, 9),
                ("VALOR", 370, 566, 9),
                ("VALDOCTO", 440, 566, 9),
                ("ABATIMENTO", 440, 597, 9),
                ("VALDOCTO", 440, 646, 9)
            };
        }

        public List<(string Key, float X, float Y, float FontSize)> GetCamposVIDA24()
        {
            return new List<(string Key, float X, float Y, float FontSize)>
            {
                ("PRODUTO", 57, 81, 9),
                ("COD_PROD", 308, 81, 9),
                ("PROC_SUSEP", 394, 81, 9),
                ("NOME_CLIENTE", 56, 129, 9),
                ("CPF", 327, 129, 9),
                ("DT_NASC", 454, 129, 9),
                ("NUMDOCTO", 470, 459, 9),
                ("CEDENTE", 359, 479, 9),
                ("DTVENCTO", 467, 479, 9),
                ("NSNUMERO", 368, 499, 9),
                ("VALDOCTO", 465, 499, 9),
                ("NUMOBJETO", 371, 535, 9),
                ("PARCELA", 440, 586, 9),
                ("DTVENCTO", 492, 586, 9),
                ("CEDENTE", 440, 604, 9),
                ("DTDOCTO", 46, 621, 9),
                ("NUMDOCTO", 133, 621, 9),
                ("DTPROCESS", 370, 621, 9),
                ("NSNUMERO", 440, 621, 9),
                ("VALOR", 370, 638, 9),
                ("VALDOCTO", 440, 638, 9),
                ("PARCELA", 440, 653, 9),
                ("PARCELA", 440, 669, 9),
                ("PARCELA", 440, 685, 9),
                ("PARCELA", 440, 701, 9),
                ("VALDOCTO", 440, 718, 9)
            };
        }
        public string GetImagePath(BoletoM4Type tipo, string folder)
        {
            return System.IO.Path.Combine(AppContext.BaseDirectory, "Resources", folder, $"{tipo}.jpg");
        }

    }
}
