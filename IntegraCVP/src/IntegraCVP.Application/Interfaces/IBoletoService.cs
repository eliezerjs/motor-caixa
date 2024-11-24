namespace IntegraCVP.Application.Interfaces
{
    public interface IBoletoService
    {
        //Pasta Boleto
        byte[] GerarBoletoVD02Pdf(Dictionary<string, string> dadosBoleto);
        byte[] GerarBoletoVIDA25Pdf(Dictionary<string, string> dadosBoleto);

        //Pasta Seguro
        byte[] GerarBoletoSeguro(Dictionary<string, string> dadosBoleto, string filename);

        //Pasta Seguro_Grupo
        byte[] GerarBoletoVA18Pdf(Dictionary<string, string> dadosBoleto);
        byte[] GerarBoletoVA24Pdf(Dictionary<string, string> dadosBoleto);
        byte[] GerarBoletoVIDA23Pdf(Dictionary<string, string> dadosBoleto);
        byte[] GerarBoletoVIDA24Pdf(Dictionary<string, string> dadosBoleto);

        /*Registros acima são identicos*/

    }
}
