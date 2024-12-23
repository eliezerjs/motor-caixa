using IntegraCVP.Application.Enums;

namespace IntegraCVP.Application.Services
{
    public partial class PrevidenciaM2Service
    {
        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetPK08()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("BOLETO_DT_VENCTO",382, 485, 8, false),

                ("BOLETO_DS_TEXTO", 70, 560, 8, false),
                ("BOLETO_NO_CERTIFICADO", 230, 560, 8, false),
                ("BOLETO_VENCIMENTO", 405, 560, 8, false)
            };
        }

        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetPK09()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("BOLETO_DT_VENCTO",380, 449, 8, false),

                ("BOLETO_DS_TEXTO", 70, 527, 8, false),
                ("BOLETO_NO_CERTIFICADO", 235, 527, 8, false),
                ("BOLETO_VENCIMENTO", 420, 527, 8, false)
            };
        }

        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetPK10()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("BOLETO_DT_VENCTO",387, 429, 8, false),

                ("BOLETO_DS_TEXTO", 70, 540, 8, false),
                ("BOLETO_NO_CERTIFICADO", 215, 540, 8, false),
                ("PARTICIPANTE_COD_EMPRESA", 435, 540, 8, false),
                ("PARTICIPANTE_SEQ_GERACAO", 495, 540, 7, false)
            };
        }

        public string GetImagePath(PrevidenciaM2Type tipo, string folder)
        {
            return System.IO.Path.Combine(AppContext.BaseDirectory, "Resources", folder, $"{tipo}.jpg");
        }
    }
}
