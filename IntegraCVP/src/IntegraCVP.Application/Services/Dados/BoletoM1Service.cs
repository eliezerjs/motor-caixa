using IntegraCVP.Application.Enums;

namespace IntegraCVP.Application.Services
{
    public partial class BoletoM1Service
    {
        public List<(string Key, float X, float Y, float FontSize)> GetCamposVD02()
        {
            return new List<(string Key, float X, float Y, float FontSize)>
            {
                ("AGENCIA", 45, 133, 8),
                ("APOLICE", 125, 133, 8),
                ("FATURA", 197, 133, 8),
                ("PERIODO", 260, 133, 8),
                ("EMISSAO", 407, 133, 8),
                ("VENCIMENT", 481, 133, 8),
                ("ESTIPULANTE", 45, 157, 8),
                ("ENDERECO", 260, 157, 8),
                ("CEP", 45, 182, 8),
                ("CIDADE", 154, 182, 8),
                ("UF", 407, 182, 8),
                ("CNPJ", 453, 182, 8),
                ("ESTIPULANTE", 45, 205, 8),
                ("ENDERECO", 260, 205, 8),
                ("CEP", 45, 230, 8),
                ("CIDADE", 154, 230, 8),
                ("UF", 407, 230, 8),
                ("CNPJ", 453, 230, 8),
                ("NVIDAS", 45, 252, 8),
                ("CAPITAL", 154, 252, 8),
                ("IOF", 310, 252, 8),
                ("PREMIO", 407, 252, 8),
                ("NUMDOCTO", 470, 378, 8),
                ("AGENCIA", 470, 398, 8),
                ("VENCIMENT", 470, 398, 8),
                ("NSNUMERO", 370, 418, 8),
                ("VALDOCTO", 470, 418, 8),
                ("PARCELA", 443, 534, 8),
                ("VENCIMENT", 494, 534, 8),
                ("AGENCIA", 443, 552, 8),
                ("DTDOCTO", 45, 568, 8),
                ("CODDOC", 135, 568, 8),
                ("AGENCIA", 219, 568, 8),
                ("AGENCIA", 340, 568, 8),
                ("AGENCIA", 370, 568, 8),
                ("NSNUMERO", 443, 568, 8),
                ("AGENCIA", 300, 585, 8),
                ("AGENCIA", 370, 585, 8),
                ("VALDOCTO", 443, 585, 8),
                ("AGENCIA", 443, 602, 8),
                ("AGENCIA", 443, 617, 8),
                ("AGENCIA", 443, 633, 8),
                ("AGENCIA", 443, 650, 8),
                ("VALDOCTO", 443, 666, 8)
            };
        }

        public List<(string Key, float X, float Y, float FontSize)> GetCamposVIDA25()
        {
            return new List<(string Key, float X, float Y, float FontSize)>
            {
                ("AGENCIA", 45, 133, 8),
                ("APOLICE", 125, 133, 8),
                ("FATURA", 197, 133, 8),
                ("PERIODO", 260, 133, 8),
                ("EMISSAO", 407, 133, 8),
                ("VENCIMENT", 481, 133, 8),
                ("ESTIPULANTE", 45, 157, 8),
                ("ENDERECO", 260, 157, 8),
                ("CEP", 45, 182, 8),
                ("CIDADE", 154, 182, 8),
                ("UF", 407, 182, 8),
                ("CNPJ", 453, 182, 8),
                ("ESTIPULANTE", 45, 205, 8),
                ("ENDERECO", 260, 205, 8),
                ("CEP", 45, 230, 8),
                ("CIDADE", 154, 230, 8),
                ("UF", 407, 230, 8),
                ("CNPJ", 453, 230, 8),
                ("NVIDAS", 45, 252, 8),
                ("CAPITAL", 154, 252, 8),
                ("IOF", 310, 252, 8),
                ("PREMIO", 407, 252, 8),
                ("NUMDOCTO", 470, 378, 8),
                ("AGENCIA", 470, 398, 8),
                ("VENCIMENT", 470, 398, 8),
                ("NSNUMERO", 370, 418, 8),
                ("VALDOCTO", 470, 418, 8),
                ("PARCELA", 443, 534, 8),
                ("VENCIMENT", 494, 534, 8),
                ("AGENCIA", 443, 552, 8),
                ("DTDOCTO", 45, 568, 8),
                ("CODDOC", 135, 568, 8),
                ("AGENCIA", 219, 568, 8),
                ("AGENCIA", 340, 568, 8),
                ("AGENCIA", 370, 568, 8),
                ("NSNUMERO", 443, 568, 8),
                ("AGENCIA", 300, 585, 8),
                ("AGENCIA", 370, 585, 8),
                ("VALDOCTO", 443, 585, 8),
                ("AGENCIA", 443, 602, 8),
                ("AGENCIA", 443, 617, 8),
                ("AGENCIA", 443, 633, 8),
                ("AGENCIA", 443, 650, 8),
                ("VALDOCTO", 443, 666, 8)
            };
        }

        public string GetImagePath(BoletoM1Type tipo, string folder)
        {
            return Path.Combine(AppContext.BaseDirectory, "Resources", folder, $"{tipo}.jpg");
        }
    }
}
