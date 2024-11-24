namespace IntegraCVP.Application.Interfaces
{
    public interface IBoletoService
    {
        /*Registros abaixo s�o identicos*/
        byte[] GerarBoletoVD02Pdf(Dictionary<string, string> dadosBoleto);
        byte[] GerarBoletoVIDA25Pdf(Dictionary<string, string> dadosBoleto);

        /*Registros acima s�o identicos*/

        /*Registros abaixo s�o identicos*/
        byte[] GerarBoletoVA18Pdf(Dictionary<string, string> dadosBoleto);
        //byte[] GerarBoletoVA24Pdf(Dictionary<string, string> dadosBoleto);
        //byte[] GerarBoletoVIDA23Pdf(Dictionary<string, string> dadosBoleto);
        //byte[] GerarBoletoVIDA24Pdf(Dictionary<string, string> dadosBoleto);

        /*Registros acima s�o identicos*/

    }
}
