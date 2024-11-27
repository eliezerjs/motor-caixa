using IntegraCVP.Application.Enums;

namespace IntegraCVP.Application.Interfaces
{
    public interface IBoletoM1Service
    {
        byte[] GerarBoletoM1(Dictionary<string, string> dadosBoleto, BoletoM1Type tipo);
    }
}
