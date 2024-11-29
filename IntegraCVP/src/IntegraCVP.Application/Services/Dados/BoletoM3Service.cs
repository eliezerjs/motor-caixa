using IntegraCVP.Application.Enums;

namespace IntegraCVP.Application.Services
{
    public partial class BoletoM3Service
    {
        public List<(string Key, float X, float Y, float FontSize)> GetCamposSeguro()
        {
            return new List<(string Key, float X, float Y, float FontSize)>
            {
                //inicio linha 1
                ("AGENCIA", 48, 182, 9),
                ("APOLICE", 138, 182, 9),
                ("FATURA", 215, 182, 9),
                ("PERIODO", 282, 182, 9),
                ("EMISSAO", 410, 182, 9),
                ("VENCIMENT", 486, 182, 9),
                //fim

                //inicio linha 2
                ("ESTIPULANTE", 48, 215, 9),
                ("CNPJ1", 346, 215, 9),

                //inicio linha 3
                ("ENDERE1", 48, 249, 9),

                //inicio linha 4
                ("CEP1", 47, 281, 9),
                ("CIDADE1", 159, 281, 9),
                ("EST1", 499, 281, 9),

                //inicio linha 5
                ("SUBESTIPULANTE", 47, 314, 8),
                ("CNPJ2", 406, 314, 9),

                //inicio linha 6
                ("ENDERE2", 47, 350, 9),
                ("CEP2", 49, 382, 9),
                ("CIDADE2", 159, 382, 9),
                ("EST2", 487, 382, 9),

                //inicio linha 7
                ("NVIDAS", 49, 415, 9),
                ("CAPITAL", 160, 415, 9),
                ("IOF", 312, 415, 9),
                ("PREMIO", 412, 415, 9)
            };
        }

        public string GetImagePath(BoletoM3Type tipo, string folder)
        {
            return Path.Combine(AppContext.BaseDirectory, "Resources", folder, $"{tipo}.jpg");
        }
    }
}
