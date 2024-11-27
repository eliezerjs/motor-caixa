using IntegraCVP.Application.Enums;

namespace IntegraCVP.Application.Services
{
    public partial class BoletoM3Service
    {
        public List<(string Key, float X, float Y, float FontSize)> GetCamposSeguro()
        {
            return new List<(string Key, float X, float Y, float FontSize)>
            {
                ("AGENCIA", 47, 186, 8),
                ("APOLICE", 138, 186, 8),
                ("FATURA", 210, 186, 8),
                ("PERIODO", 282, 186, 8),
                ("EMISSAO", 409, 186, 8),
                ("VENCIMENT", 484, 186, 8),
                ("ESTIPULANTE", 47, 217, 8),
                ("CNPJ1", 340, 217, 8),
                ("ENDERE1", 47, 251, 8),
                ("CEP1", 47, 284, 8),
                ("CIDADE1", 157, 284, 8),
                ("EST1", 488, 284, 8),
                ("SUBESTIPULANTE", 47, 317, 8),
                ("CNPJ2", 396, 317, 8),
                ("ENDERE2", 47, 350, 8),
                ("CEP2", 47, 385, 8),
                ("CIDADE2", 156, 385, 8),
                ("EST2", 480, 385, 8),
                ("NVIDAS", 47, 418, 8),
                ("CAPITAL", 156, 418, 8),
                ("IOF", 312, 418, 8),
                ("PREMIO", 400, 418, 8)
            };
        }

        public string GetImagePath(BoletoM3Type tipo, string folder)
        {
            return Path.Combine(AppContext.BaseDirectory, "Resources", folder, $"{tipo}.jpg");
        }
    }
}
