namespace IntegraCVP.Application.Interfaces
{
    public interface IImportFilePrevConverterService
    {
        Task<List<Dictionary<string, string>>> ProcessDataAsync(Stream dataStream);
    }
}
