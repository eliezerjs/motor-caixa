namespace IntegraCVP.Application.Interfaces
{
    public interface IBoletoService
    {
        byte[] GerarBoleto2Pdf(Dictionary<string, string> dadosBoleto);
        byte[] GerarBoleto18Pdf(Dictionary<string, string> dadosBoleto);
        byte[] GerarBoleto25Pdf(Dictionary<string, string> dadosBoleto);
    }
}
