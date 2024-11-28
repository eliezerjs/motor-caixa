using IntegraCVP.Application.Enums;

namespace IntegraCVP.Application.Services
{
    public partial class BoletoM2Service
    {
        public List<(string Key, float X, float Y, float FontSize)> GetCamposVD32()
        {
            return new List<(string Key, float X, float Y, float FontSize)>
            {
                ("AGENCIA", 35, 107, 8),
                ("APOLICE", 120, 107, 8),
                ("FATURA", 210, 107, 8),
                ("PERIODO", 300, 107, 8),
                ("EMISSAO", 390, 107, 8),
                ("VENCIMENT", 481, 107, 8),

                ("ESTIPULANTE", 35, 133, 8),
                ("ENDERECO", 300, 133, 8),

                ("CEP", 35, 160, 8),
                ("CIDADE", 120, 160, 8),
                ("UF", 390, 160, 8),
                ("CNPJ1", 481, 160, 8),

                ("ESTIPULANTE", 35, 187, 8),
                ("ENDERECO", 300, 187, 8),

                ("CEP", 35, 214, 8),
                ("CIDADE", 120, 214, 8),
                ("UF", 390, 214, 8),
                ("CNPJ1", 481, 214, 8),

                ("NVIDAS", 35, 241, 8),
                ("CAPITAL", 120, 241, 8),
                ("IOF", 300, 241, 8),
                ("PREMIO", 538, 241, 8),

                ("NUMCDBARRA", 197, 300, 10),

                ("NUMDOCTO", 507, 329, 8),

                ("AGENCIA", 450, 353, 8),
                ("VENCIMENT", 521, 353, 8),

                ("NSNUMERO", 386, 377, 8),
                ("VALDOCTO", 537, 377, 8),

                ("NUMCDBARRA", 197, 450, 10),

                ("PARCELA", 487, 482, 8),
                ("VENCIMENT", 522, 482, 8),

                ("AGENCIA", 545, 503, 8),

                ("DTDOCTO", 50, 525, 8),
                ("CODDOC", 127, 525, 8),
                ("AGENCIA", 230, 525, 8),
                ("AGENCIA", 395, 525, 8),
                ("NSNUMERO", 478, 525, 8),

                ("AGENCIA", 315, 547, 8),
                ("VALOR", 395, 547, 8),
                ("VALDOCTO", 538, 547, 8),

                ("", 540, 568, 8),

                ("", 540, 590, 8),

                ("", 540, 611, 8),

                ("", 540, 631, 8),

                ("VALDOCTO", 538, 653, 8),

                ("SUBESTIPULANTE", 50, 672, 8),

                ("ENDERE2", 50, 680, 8),

                ("CIDADE2", 50, 687, 8),
                ("EST2", 100, 687, 8),

                ("CEP2" +
                "", 50, 695, 8),
            };
        }

        public List<(string Key, float X, float Y, float FontSize)> GetCamposVIDA26()
        {
            return new List<(string Key, float X, float Y, float FontSize)>
            {
                ("AGENCIA", 35, 133, 8),
                ("APOLICE", 125, 133, 8),
                ("FATURA", 197, 133, 8),
                ("PERIODO", 260, 133, 8),
                ("EMISSAO", 407, 133, 8),
                ("VENCIMENT", 481, 133, 8),
                ("ESTIPULANTE", 35, 157, 8),
                ("ENDERECO", 260, 157, 8),
                ("CEP", 35, 182, 8),
                ("CIDADE", 154, 182, 8),
                ("UF", 407, 182, 8),
                ("CNPJ", 353, 182, 8),
                ("ESTIPULANTE", 35, 205, 8),
                ("ENDERECO", 260, 205, 8),
                ("CEP", 35, 230, 8),
                ("CIDADE", 154, 230, 8),
                ("UF", 407, 230, 8),
                ("CNPJ", 353, 230, 8),
                ("NVIDAS", 35, 252, 8),
                ("CAPITAL", 154, 252, 8),
                ("IOF", 310, 252, 8),
                ("PREMIO", 407, 252, 8),
                ("NUMDOCTO", 470, 378, 8),
                ("AGENCIA", 470, 398, 8),
                ("VENCIMENT", 470, 398, 8),
                ("NSNUMERO", 370, 418, 8),
                ("VALDOCTO", 470, 418, 8),
                ("PARCELA", 450, 534, 8),
                ("VENCIMENT", 494, 534, 8),
                ("AGENCIA", 450, 552, 8),
                ("DTDOCTO", 35, 568, 8),
                ("CODDOC", 135, 568, 8),
                ("AGENCIA", 219, 568, 8),
                ("AGENCIA", 340, 568, 8),
                ("AGENCIA", 370, 568, 8),
                ("NSNUMERO", 450, 568, 8),
                ("AGENCIA", 300, 585, 8),
                ("AGENCIA", 370, 585, 8),
                ("VALDOCTO", 450, 585, 8),
                ("AGENCIA", 450, 602, 8),
                ("AGENCIA", 450, 617, 8),
                ("AGENCIA", 450, 633, 8),
                ("AGENCIA", 450, 650, 8),
                ("VALDOCTO", 450, 666, 8)
            };
        }

        public string GetImagePath(BoletoM2Type tipo, string folder)
        {
            return Path.Combine(AppContext.BaseDirectory, "Resources", folder, $"{tipo}.jpg");
        }
    }
}
