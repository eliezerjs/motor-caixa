using IntegraCVP.Application.Enums;

namespace IntegraCVP.Application.Services
{
    public partial class PrevidenciaM5Service
    {
        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetPK12()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("PORTABILIDADE_DT_CEDENTE", 410, 243, 8, false),

                ("PORTABILIDADE_ENTIDADE_CEDENTE", 107, 257, 8, false),
                ("PORTABILIDADE_NO_CERTIFICADO", 427, 257, 8, false),

                ("PORTABILIDADE_VALOR_LIQUIDO", 113, 272, 8, false),
                ("PORTABILIDADE_ENTIDADE_CESSIONARIA", 296, 272, 8, false)
            };
        }

        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetPK13()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("BOLETO_NO_CERTIFICADO", 423, 226, 8, false),

                ("BOLETO_NO_CERTIFICADO", 200, 242, 8, false),
                ("BOLETO_NO_CERTIFICADO", 457, 242, 8, false),

                ("BOLETO_NO_CERTIFICADO", 110, 256, 8, false),
                ("BOLETO_NO_CERTIFICADO", 450, 255, 8, false)
            };
        }

        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetPK14()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("BOLETO_NO_CERTIFICADO", 385, 217, 8, false),

                ("BOLETO_NO_CERTIFICADO", 107, 231, 8, false),
                ("BOLETO_NO_CERTIFICADO", 282, 231, 8, false),
                ("BOLETO_NO_CERTIFICADO", 465, 230, 8, false),

            };
        }

        public string GetImagePath(PrevidenciaM5Type tipo, string folder)
        {
            return System.IO.Path.Combine(AppContext.BaseDirectory, "Resources", folder, $"{tipo}.jpg");
        }
    }
}
