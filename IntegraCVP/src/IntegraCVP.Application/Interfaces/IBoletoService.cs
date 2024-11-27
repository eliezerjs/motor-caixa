namespace IntegraCVP.Application.Interfaces
{
    public interface IBoletoService
    {
        //Pasta Boleto
        byte[] GerarBoletoPdf(Dictionary<string, string> dadosBoleto, string filename);        

        //Pasta Seguro
        byte[] GerarBoletoSeguro(Dictionary<string, string> dadosBoleto, string filename);

        //Pasta Seguro_Grupo
        byte[] GerarBoletoVA18Pdf(Dictionary<string, string> dadosBoleto);
        byte[] GerarBoletoVA24Pdf(Dictionary<string, string> dadosBoleto);
        byte[] GerarBoletoVIDA23Pdf(Dictionary<string, string> dadosBoleto);
        byte[] GerarBoletoVIDA24Pdf(Dictionary<string, string> dadosBoleto);

        //Pasta Carta_Declinio
        byte[] GerarBoletoVIDA01Pdf(Dictionary<string, string> dadosBoleto);
        byte[] GerarBoletoVIDA02Pdf(Dictionary<string, string> dadosBoleto);
        byte[] GerarBoletoVIDA03Pdf(Dictionary<string, string> dadosBoleto);
        byte[] GerarBoletoVIDA04Pdf(Dictionary<string, string> dadosBoleto);

        /*Registros acima s�o identicos*/

    }
}
