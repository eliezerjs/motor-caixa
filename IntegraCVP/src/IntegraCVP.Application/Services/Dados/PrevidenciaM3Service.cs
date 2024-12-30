using IntegraCVP.Application.Enums;

namespace IntegraCVP.Application.Services
{
    public partial class PrevidenciaM3Service
    {
        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetPK56()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("NOME_CLIENTE", 55, 97, 10, false),             
                ("CPF_CLIENTE", 325, 97, 10, false),              
                ("DATA_NASCIMENTO", 550, 97, 10, false),   
                
                ("NOME_PLANO", 55, 146, 10, false),               
                ("RAMO_PRODUTO", 325, 146, 10, false),            
                ("COD_RAMO_PRODUTO", 550, 146, 10, false),   
                
                ("MODALIDADE", 55, 177, 10, false),               
                ("NUM_PROPOSTA", 190, 177, 10, false),            
                ("NUM_CERTIFICADO", 324, 177, 10, false),         
                ("INICIO_VIGENCIA", 460, 177, 10, false),         
                ("PLANO_MENSAL", 625, 177, 10, false),    
                
                ("DEBITO_CONTA", 55, 208, 10, false),   
                
                ("BENEFICIO_SOBREVIVENCIA", 55, 251, 10, false),  
                ("PROCESSO_SUSEP", 190, 251, 10, false),         
                ("CONTRIBUICAO_PREMIO", 325, 251, 10, false),     
                ("DATA_CONCESSAO_BENEF", 460, 251, 10, false),
                
                ("REGIME_TRIBUTARIO", 55, 282, 10, false),       
                ("TABUA_ATUARIAL", 190, 282, 10, false),         
                ("EXCEDENTE_FINANCEIRO", 325, 282, 10, false), 
                
                ("NOME_CLIENTE_PROTECAO", 55, 355, 10, false),    
                ("CPF_CLIENTE_PROTECAO", 325, 355, 10, false),    
                ("DATA_NASCIMENTO_PROTECAO", 550, 355, 10, false),

                ("PROCESSO_SUSEP_PROTECAO", 190, 386, 10, false), 
                ("PRAZO_ANOS", 325, 386, 10, false),             
                ("CONTRIBUICAO_RS", 460, 386, 10, false),         
                ("BENEFICIO_RS", 625, 386, 10, false)
            };
        }

        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetPK57()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("NOME_CLIENTE", 77, 140, 8, false),
                ("NUM_PROPOSTA", 382, 166, 7, false),
                ("DATA_DECLINIO", 115, 177, 7, false),
                ("COD_PRODUTO", 296, 736, 7, false),
                ("COD_SUSEP", 373, 736, 7, false)
            };
        }

        public List<(string Key, float X, float Y, float FontSize, bool isBold)> GetPK58()
        {
            return new List<(string Key, float X, float Y, float FontSize, bool isBold)>
            {
                ("NOME_CLIENTE", 77, 140, 8, false),
                ("NUM_PROPOSTA", 382, 166, 7, false),
                ("DATA_DECLINIO", 115, 177, 7, false),
                ("COD_PRODUTO", 296, 736, 7, false),
                ("COD_SUSEP", 373, 736, 7, false)
            };
        }

        public string GetImagePath(PrevidenciaM3Type tipo, string folder)
        {
            return System.IO.Path.Combine(AppContext.BaseDirectory, "Resources", folder, $"{tipo}.jpg");
        }
    }
}
