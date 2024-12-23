using IntegraCVP.Application.Enums;

namespace IntegraCVP.Application.Services
{
    public partial class PrevidenciaM1Service
    {
        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetPK28()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("BOLETO_DT_VENCTO",190, 477, 8, false),

                ("BOLETO_DS_TEXTO", 70, 490, 8, false),
                ("BOLETO_NO_CERTIFICADO", 320, 490, 8, false),

                ("BOLETO_VENCIMENTO", 155, 645, 8, false)

            };
        }

        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetPK29()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("BOLETO_DT_VENCTO",190, 513, 8, false),

                ("BOLETO_DS_TEXTO", 70, 525, 8, false),
                ("BOLETO_NO_CERTIFICADO", 320, 525, 8, false),

                ("BOLETO_VENCIMENTO", 120, 657, 8, false)
            };
        }

        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetPK30()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("BOLETO_DT_VENCTO",190, 535, 8, false),

                ("BOLETO_DS_TEXTO", 80, 546, 8, false),
                ("BOLETO_NO_CERTIFICADO", 320, 546, 8, false),

                ("BOLETO_VENCIMENTO", 160, 703, 8, false)
            };
        }

        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetPK31()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("BOLETO_DT_VENCTO",190, 529, 8, false),

                ("BOLETO_DS_TEXTO", 75, 540, 8, false),
                ("BOLETO_NO_CERTIFICADO", 330, 540, 8, false),

                ("BOLETO_VENCIMENTO", 160, 693, 8, false)
            };
        }

        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetPK32()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("BOLETO_DT_VENCTO",190, 450, 8, false),

                ("BOLETO_DS_TEXTO", 80, 462, 8, false),
                ("BOLETO_NO_CERTIFICADO", 320, 462, 8, false),

                ("BOLETO_VENCIMENTO", 160, 641, 8, false)
            };
        }

        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetPK33()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("BOLETO_DT_VENCTO",190, 487, 8, false),

                ("BOLETO_DS_TEXTO", 80, 497, 8, false),
                ("BOLETO_NO_CERTIFICADO", 320, 498, 8, false),

                ("BOLETO_VENCIMENTO", 170, 643, 8, false)
            };
        }

        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetPK34()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("BOLETO_DT_VENCTO",190, 527, 8, false),

                ("BOLETO_DS_TEXTO", 80, 5439, 8, false),
                ("BOLETO_NO_CERTIFICADO", 320, 539, 8, false),

                ("BOLETO_VENCIMENTO", 160, 659, 8, false)
            };
        }

        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetPK36()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("BOLETO_DT_VENCTO",190, 496, 8, false),

                ("BOLETO_DS_TEXTO", 80, 507, 8, false),
                ("BOLETO_NO_CERTIFICADO", 320, 507, 8, false),

                ("BOLETO_VENCIMENTO", 160, 652, 8, false)
            };
        }

        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetPK47()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                 ("BOLETO_DT_VENCTO",100, 532, 8, false),

                ("BOLETO_DS_TEXTO", 80, 543, 8, false),
                ("BOLETO_NO_CERTIFICADO", 320, 543, 8, false),

                ("BOLETO_VENCIMENTO", 160, 712, 8, false)
            };
        }

        public string GetImagePath(PrevidenciaM1Type tipo, string folder)
        {
            return System.IO.Path.Combine(AppContext.BaseDirectory, "Resources", folder, $"{tipo}.jpg");
        }
    }
}
