using IntegraCVP.Application.Enums;

namespace IntegraCVP.Application.Services
{
    public partial class BoasVindasService
    {
        public List<(string Key, float X, float Y, float FontSize, bool IsBold)> GetCamposVIDA05()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("SEGURADO", 59, 250, 8, false),
                ("NOME_PRODUTO", 59, 288, 8, false),
                ("NUM_CERTIF", 59, 320, 8, false),
                ("DT_VIG", 59, 356, 8, false),
                ("CUSTO", 60, 470, 8, false),
                ("OPCAO_PAG", 60, 510, 8, false)
            };
        }

        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetCamposVIDA07()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("SEGURADO", 100, 174, 11, false),
                ("NUM_CERTIF", 195, 324, 13, false),               
            };
        }

        public string GetImagePath(BoasVindasType tipo, string folder)
        {
            return Path.Combine(AppContext.BaseDirectory, "Resources", folder, $"{tipo}.jpg");
        }
    }
}
