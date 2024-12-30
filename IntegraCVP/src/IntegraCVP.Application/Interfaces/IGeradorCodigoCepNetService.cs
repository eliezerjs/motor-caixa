
namespace IntegraCVP.Application.Interfaces
{
    public interface IGeradorCodigoCepNetService
    {
        string GerarCodigo(string COD_CEP, string clienteCif, string categoriaRegiao, string cnae);
    }
}
