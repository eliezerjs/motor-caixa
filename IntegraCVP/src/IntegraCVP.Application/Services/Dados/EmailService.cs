using IntegraCVP.Application.Enums;

namespace IntegraCVP.Application.Services
{
    public partial class EmailService
    {
        public List<(string Key, float X, float Y, float FontSize, string FontColor)> GetEmailSeguros()
        {
            return new List<(string Key, float X, float Y, float FontSize, string FontColor)>
            {
                ("SEGURADO", 80, 199, 10, "Black"),
                ("COD_PRODUTO", 357, 748, 6, "Black"),
                ("COD_SUSEP", 442, 748, 6, "Black"),
                ("COD_SUSEPCAP", 314, 766, 6, "Black")
            };
        }

        public List<(string Key, float X, float Y, float FontSize, string FontColor)> GetEmailVidaExclusiva()
        {
            return new List<(string Key, float X, float Y, float FontSize, string FontColor)>
            {
                ("SEGURADO", 100, 174, 11, "Black"),
                ("NUM_CERTIF", 195, 324, 13, "Black"),
                ("COD_PRODUTO", 377, 787, 5, "White"),
                ("COD_SUSEP", 427, 787, 5, "White"),
                ("COD_SUSEPCAP", 100, 799, 5, "White")
            };
        }

        public List<(string Key, float X, float Y, float FontSize, string FontColor)> GetVIDA18()
        {
            return new List<(string Key, float X, float Y, float FontSize, string FontColor)>
            {
                ("SEGURADO", 80, 199, 10, "Black"),
                ("COD_PRODUTO", 359, 748, 6, "Black"),
                ("COD_SUSEP", 444, 748, 6, "Black"),
                ("COD_SUSEPCAP", 350, 768, 6, "Black")
            };
        }

        public string GetImagePath(EmailType tipo, string folder)
        {
            return System.IO.Path.Combine(AppContext.BaseDirectory, "Resources", folder, $"{tipo}.jpg");
        }
    }
}
