using IntegraCVP.Application.Enums;

namespace IntegraCVP.Application.Services
{
    public partial class PrevidenciaOutrosService
    {
        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetPK11()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("NOME_CLIENTE", 77, 140, 8, false),
                ("NUM_PROPOSTA", 382, 166, 7, false),
                ("DATA_DECLINIO", 115, 177, 7, false),
                ("COD_PRODUTO", 296, 736, 7, false),
                ("COD_SUSEP", 373, 736, 7, false)
            };
        }

        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetPK15()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("BOLETO_DT_VENCTO",380, 450, 8, false),

                ("BOLETO_DS_TEXTO", 70, 527, 8, false),
                ("BOLETO_NO_CERTIFICADO", 235, 527, 8, false),
                ("BOLETO_VENCIMENTO", 420, 527, 8, false)
            };
        }

        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetPK35()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("BOLETO_DT_VENCTO",380, 450, 8, false),
                ("BOLETO_DS_TEXTO", 70, 527, 8, false),
                ("BOLETO_NO_CERTIFICADO", 235, 527, 8, false),
                ("BOLETO_VENCIMENTO", 420, 527, 8, false)
            };
        }

        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetPK37()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("NOME_CLIENTE", 77, 140, 8, false),
                ("NUM_PROPOSTA", 382, 166, 7, false),
                ("DATA_DECLINIO", 115, 177, 7, false),
                ("COD_PRODUTO", 296, 736, 7, false),
                ("COD_SUSEP", 373, 736, 7, false)
            };
        }

        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetPK44()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("NOME_CLIENTE", 77, 140, 8, false),
                ("NUM_PROPOSTA", 382, 166, 7, false),
                ("DATA_DECLINIO", 115, 177, 7, false),
                ("COD_PRODUTO", 296, 736, 7, false),
                ("COD_SUSEP", 373, 736, 7, false)
            };
        }

        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetPK48()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("NOME_CLIENTE", 77, 140, 8, false),
                ("NUM_PROPOSTA", 382, 166, 7, false),
                ("DATA_DECLINIO", 115, 177, 7, false),
                ("COD_PRODUTO", 296, 736, 7, false),
                ("COD_SUSEP", 373, 736, 7, false)
            };
        }

        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetPK49()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("NOME_CLIENTE", 77, 140, 8, false),
                ("NUM_PROPOSTA", 382, 166, 7, false),
                ("DATA_DECLINIO", 115, 177, 7, false),
                ("COD_PRODUTO", 296, 736, 7, false),
                ("COD_SUSEP", 373, 736, 7, false)
            };
        }

        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetPK53()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("NOME_CLIENTE", 77, 140, 8, false),
                ("NUM_PROPOSTA", 382, 166, 7, false),
                ("DATA_DECLINIO", 115, 177, 7, false),
                ("COD_PRODUTO", 296, 736, 7, false),
                ("COD_SUSEP", 373, 736, 7, false)
            };
        }
        public string GetImagePath(PrevidenciaOutrosType tipo, string folder)
        {
            return System.IO.Path.Combine(AppContext.BaseDirectory, "Resources", folder, $"{tipo}.jpg");
        }
    }
}
