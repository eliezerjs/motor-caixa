using IntegraCVP.Application.Enums;

namespace IntegraCVP.Application.Services
{
    public partial class PrevidenciaM5Service
    {
        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetPK12()
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

        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetPK13()
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

        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetPK14()
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

        public string GetImagePath(PrevidenciaM5Type tipo, string folder)
        {
            return System.IO.Path.Combine(AppContext.BaseDirectory, "Resources", folder, $"{tipo}.jpg");
        }
    }
}
