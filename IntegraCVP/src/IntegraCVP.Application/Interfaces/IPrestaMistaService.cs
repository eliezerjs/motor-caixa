namespace IntegraCVP.Application.Interfaces
{
    public interface IPrestamistaService
    {
        byte[] GerarBoasVindasPdf(Dictionary<string, string> dados);
        byte[] GerarBoasVindasQuinzeP1Pdf(Dictionary<string, string> dados);
        byte[] GerarBoasVindasQuinzeP2Pdf(Dictionary<string, string> dados);
    }
}
