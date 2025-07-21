namespace Group5_SWD392_SE1841.Repositories
{
    public interface IMasterRepo
    {
        Task<Dictionary<int, string>> GetMasterDictionaryByTypeNameAsync(string typeName);
    }
}
