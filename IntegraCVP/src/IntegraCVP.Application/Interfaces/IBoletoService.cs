namespace IntegraCVP.Application.Interfaces
{
    public interface IBoletoService
    {
        byte[] GerarBoletoPdf(Dictionary<string, string> dadosBoleto);
    }
}
