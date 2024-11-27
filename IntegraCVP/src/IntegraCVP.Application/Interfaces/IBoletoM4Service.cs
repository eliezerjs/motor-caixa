using IntegraCVP.Application.Enums;

namespace IntegraCVP.Application.Interfaces
{
    public interface IBoletoM4Service
    {
        byte[] GerarBoletoM4(Dictionary<string, string> dadosBoleto, BoletoM4Type tipo);        

    }
}
