using IntegraCVP.Application.Enums;

namespace IntegraCVP.Application.Services
{
    public partial class BoasVindasService
    {
        public List<(string Key, float X, float Y, float FontSize, bool IsBold)> GetCamposVIDA05()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("", 67, 55, 8, false),

                ("SEGURADO", 55, 71, 8, false),

                ("SEGURADO", 59, 225, 8, false),

                ("NOME_PRODUTO", 59, 253, 8, false),

                ("NUM_CERTIF", 59, 288, 8, false),

                ("DT_VIG", 59, 320, 8, false),

                ("CUSTO", 60, 465, 8, false),

                ("OPCAO_PAG", 60, 475, 8, false),

                ("COD_PRODUTO", 380, 748, 7, false),

                ("COD_SUSEP", 465, 748, 7, false)
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
