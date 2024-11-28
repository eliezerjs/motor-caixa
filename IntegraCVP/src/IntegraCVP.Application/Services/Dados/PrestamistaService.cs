using IntegraCVP.Application.Enums;

namespace IntegraCVP.Application.Services
{
    public partial class PrestamistaService
    {
        public List<(string Key, float X, float Y, float FontSize)> GetCamposPrest01()
        {
            return new List<(string Key, float X, float Y, float FontSize)>
            {
                ("SEGURADO", 59, 250, 8),
                ("NOME_PRODUTO", 59, 288, 8),
                ("NUM_CERTIF", 59, 320, 8),
                ("DT_VIG", 59, 356, 8),
                ("CUSTO", 60, 470, 8),
                ("OPCAO_PAG", 60, 510, 8)
            };
        }

        public string GetImagePath(PrestamistaType tipo, string folder)
        {
            return System.IO.Path.Combine(AppContext.BaseDirectory, "Resources", folder, $"{tipo}.jpg");
        }
    }
}
