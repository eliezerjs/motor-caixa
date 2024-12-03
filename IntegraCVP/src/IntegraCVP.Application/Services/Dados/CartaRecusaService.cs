using IntegraCVP.Application.Enums;

namespace IntegraCVP.Application.Services
{
    public partial class CartaRecusaService
    {
        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetVIDA01()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("NOME_CLIENTE", 77, 140, 8, false),
                ("NUM_PROPOSTA", 382, 166, 7, false),
                ("DATA_DECLINIO", 115, 177, 7, false),
                ("COD_PRODUTO", 310, 728, 7, false),
                ("COD_SUSEP", 55, 738, 7, false)
            };
        }

        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetVIDA02()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("NOME_CLIENTE", 77, 140, 8, false),
                ("NUM_PROPOSTA", 382, 166, 7, false),
                ("DATA_DECLINIO", 115, 177, 7, false),
                ("COD_PRODUTO", 344, 728, 7, false),
                ("COD_SUSEP", 55, 738, 7, false)
            };
        }

        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetVIDA03()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("NOME_CLIENTE", 77, 140, 8, false),
                ("NUM_PROPOSTA", 382, 166, 7, false),
                ("DATA_DECLINIO", 115, 177, 7, false),
                ("COD_PRODUTO", 297, 740, 7, false),
                ("COD_SUSEP", 373, 740, 7, false)
            };
        }

        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetVIDA04()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("NOME_CLIENTE", 77, 140, 8, false),
                ("NUM_PROPOSTA", 382, 166, 7, false),
                ("DATA_DECLINIO", 115, 176, 7, false),
                ("COD_PRODUTO", 296, 736, 7, false),
                ("COD_SUSEP", 373, 736, 7, false)
            };
        }

        public string GetImagePath(CartaRecusaType tipo, string folder)
        {
            return System.IO.Path.Combine(AppContext.BaseDirectory, "Resources", folder, $"{tipo}.jpg");
        }
    }
}
