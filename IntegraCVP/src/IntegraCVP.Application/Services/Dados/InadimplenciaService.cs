using IntegraCVP.Application.Enums;

namespace IntegraCVP.Application.Services
{
    public partial class InadimplenciaService
    {
        public List<(string Key, float X, float Y, float FontSize)> GetVD33()
        {
            return new List<(string Key, float X, float Y, float FontSize)>
            {
                ("NOME_CLIENTE", 77, 140, 8),
                ("NUM_PROPOSTA", 382, 166, 7),
                ("DATA_DECLINIO", 115, 177, 7),
                ("COD_PRODUTO", 296, 736, 7),
                ("COD_SUSEP", 373, 736, 7)
            };
        }

        public string GetImagePath(InadimplenciaType tipo, string folder)
        {
            return System.IO.Path.Combine(AppContext.BaseDirectory, "Resources", folder, $"{tipo}.jpg");
        }
    }
}
