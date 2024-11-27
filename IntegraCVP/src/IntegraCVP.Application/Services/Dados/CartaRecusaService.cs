using IntegraCVP.Application.Enums;

namespace IntegraCVP.Application.Services
{
    public partial class CartaRecusaService
    {
        public List<(string Key, float X, float Y, float FontSize)> GetVIDA01()
        {
            return new List<(string Key, float X, float Y, float FontSize)>
            {
                ("NOME_CLIENTE", 77, 140, 8),
                ("NUM_PROPOSTA", 382, 166, 7),
                ("DATA_DECLINIO", 115, 177, 7),
                ("COD_PRODUTO", 310, 728, 7),
                ("COD_SUSEP", 55, 738, 7)
            };
        }

        public List<(string Key, float X, float Y, float FontSize)> GetVIDA02()
        {
            return new List<(string Key, float X, float Y, float FontSize)>
            {
                ("NOME_CLIENTE", 77, 140, 8),
                ("NUM_PROPOSTA", 382, 166, 7),
                ("DATA_DECLINIO", 115, 177, 7),
                ("COD_PRODUTO", 344, 728, 7),
                ("COD_SUSEP", 55, 738, 7)
            };
        }

        public List<(string Key, float X, float Y, float FontSize)> GetVIDA03()
        {
            return new List<(string Key, float X, float Y, float FontSize)>
            {
                ("NOME_CLIENTE", 77, 140, 8),
                ("NUM_PROPOSTA", 382, 166, 7),
                ("DATA_DECLINIO", 115, 177, 7),
                ("COD_PRODUTO", 297, 740, 7),
                ("COD_SUSEP", 373, 740, 7)
            };
        }

        public List<(string Key, float X, float Y, float FontSize)> GetVIDA04()
        {
            return new List<(string Key, float X, float Y, float FontSize)>
            {
                ("NOME_CLIENTE", 77, 140, 8),
                ("NUM_PROPOSTA", 382, 166, 7),
                ("DATA_DECLINIO", 115, 176, 7),
                ("COD_PRODUTO", 296, 736, 7),
                ("COD_SUSEP", 373, 736, 7)
            };
        }

        public string GetImagePath(CartaRecusaType tipo, string folder)
        {
            return System.IO.Path.Combine(AppContext.BaseDirectory, "Resources", folder, $"{tipo}.jpg");
        }
    }
}
