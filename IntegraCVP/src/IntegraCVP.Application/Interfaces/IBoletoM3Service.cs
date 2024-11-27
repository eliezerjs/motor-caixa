namespace IntegraCVP.Application.Interfaces
{
    public interface IBoletoM3Service
    {
        byte[] GerarBoletoM3(Dictionary<string, string> dadosBoleto, string filename);        

    }
}
