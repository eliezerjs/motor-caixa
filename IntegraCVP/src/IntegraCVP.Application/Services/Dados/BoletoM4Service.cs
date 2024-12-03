using IntegraCVP.Application.Enums;

namespace IntegraCVP.Application.Services
{
    public partial class BoletoM4Service
    {
        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetCamposVA18()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("PRODUTO", 57, 68, 8, false),
                ("COD_PROD", 306, 69, 8, false),
                ("PROC_SUSEP", 392, 69, 8, false),
                ("NOME_CLIENTE", 55, 110, 8, false),
                ("CPF", 327, 110, 8, false),
                ("DT_NASC", 453, 110, 8, false),

                ("NUMDOCTO", 495, 402, 8, false),

                ("CEDENTE", 405, 423, 8, false),
                ("DTVENCTO", 507, 423, 8, false),

                ("NSNUMERO", 377, 443, 8, false),
                ("VALDOCTO", 524, 443, 8, false),

                ("NUMOBJETO", 520, 479, 8, false),

                ("PARCELA", 470, 525, 8, false),
                ("DTVENCTO", 508, 525, 8, false),

                ("CEDENTE", 495, 545, 8, false),

                ("DTDOCTO", 86, 562, 8, false),
                ("NUMDOCTO", 158, 562, 8, false),
                ("DTPROCESS", 398, 562, 8, false),
                ("NSNUMERO", 466, 562, 8, false),

                ("VALDOCTO", 524, 579, 8, false),

                ("VALDOCTO", 524, 659, 8, false)
            };
        }

        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetCamposVA24()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("PRODUTO", 57, 87, 8, false),
                ("COD_PROD", 308, 87, 8, false),
                ("PROC_SUSEP", 394, 87, 8, false),
                ("NOME_CLIENTE", 56, 132, 8, false),
                ("CPF", 327, 132, 8, false),
                ("DT_NASC", 454, 132, 8, false),
                ("NUMDOCTO", 470, 364, 8, false),
                ("CEDENTE", 359, 384, 8, false),
                ("DTVENCTO", 467, 384, 8, false),
                ("NSNUMERO", 368, 404, 8, false),
                ("VALDOCTO", 465, 404, 8, false),
                ("NUMOBJETO", 371, 440, 8, false),
                ("PARCELA", 440, 491, 8, false),
                ("DTVENCTO", 492, 491, 8, false),
                ("CEDENTE", 440, 509, 8, false),
                ("DTDOCTO", 46, 526, 8, false),
                ("NUMDOCTO", 133, 526, 8, false),
                ("DTPROCESS", 370, 526, 8, false),
                ("NSNUMERO", 440, 526, 8, false),
                ("VALOR", 370, 543, 8, false),
                ("VALDOCTO", 440, 543, 8, false),
                ("ABATIMENTO", 440, 574, 8, false),
                ("VALDOCTO", 440, 623, 8, false)
            };
        }

        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetCamposVIDA23()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("PRODUTO", 57, 68, 8, false),
                ("COD_PROD", 307, 68, 8, false),
                ("PROC_SUSEP", 393, 68, 8, false),
                ("NOME_CLIENTE", 55, 110, 8, false),
                ("CPF", 327, 110, 8, false),
                ("DT_NASC", 453, 110, 8, false),
                ("NUMDOCTO", 470, 390, 8, false),
                ("CEDENTE", 359, 410, 8, false),
                ("DTVENCTO", 467, 410, 8, false),
                ("NSNUMERO", 368, 430, 8, false),
                ("VALDOCTO", 465, 430, 8, false),
                ("NUMOBJETO", 371, 463, 8, false),
                ("PARCELA", 440, 514, 8, false),
                ("DTVENCTO", 492, 514, 8, false),
                ("CEDENTE", 440, 532, 8, false),
                ("DTDOCTO", 46, 549, 8, false),
                ("NUMDOCTO", 133, 549, 8, false),
                ("DTPROCESS", 370, 549, 8, false),
                ("NSNUMERO", 440, 549, 8, false),
                ("VALOR", 370, 566, 8, false),
                ("VALDOCTO", 440, 566, 8, false),
                ("ABATIMENTO", 440, 597, 8, false),
                ("VALDOCTO", 440, 646, 8, false)
            };
        }

        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetCamposVIDA24()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("PRODUTO", 57, 81, 8, false),
                ("COD_PROD", 308, 81, 8, false),
                ("PROC_SUSEP", 394, 81, 8, false),
                ("NOME_CLIENTE", 56, 129, 8, false),
                ("CPF", 327, 129, 8, false),
                ("DT_NASC", 454, 129, 8, false),
                ("NUMDOCTO", 470, 459, 8, false),
                ("CEDENTE", 359, 479, 8, false),
                ("DTVENCTO", 467, 479, 8, false),
                ("NSNUMERO", 368, 499, 8, false),
                ("VALDOCTO", 465, 499, 8, false),
                ("NUMOBJETO", 371, 535, 8, false),
                ("PARCELA", 440, 586, 8, false),
                ("DTVENCTO", 492, 586, 8, false),
                ("CEDENTE", 440, 604, 8, false),
                ("DTDOCTO", 46, 621, 8, false),
                ("NUMDOCTO", 133, 621, 8, false),
                ("DTPROCESS", 370, 621, 8, false),
                ("NSNUMERO", 440, 621, 8, false),
                ("VALOR", 370, 638, 8, false),
                ("VALDOCTO", 440, 638, 8, false),
                ("PARCELA", 440, 653, 8, false),
                ("PARCELA", 440, 669, 8, false),
                ("PARCELA", 440, 685, 8, false),
                ("PARCELA", 440, 701, 8, false),
                ("VALDOCTO", 440, 718, 8, false)
            };
        }
        public string GetImagePath(BoletoM4Type tipo, string folder)
        {
            return System.IO.Path.Combine(AppContext.BaseDirectory, "Resources", folder, $"{tipo}.jpg");
        }

    }
}
