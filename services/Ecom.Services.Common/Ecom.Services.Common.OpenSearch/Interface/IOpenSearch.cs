namespace Ecom.Services.Common.OpenSearch.Interface
{
    public interface IOpenSearch
    {
        Task<List<T>> QueryAsync<T>(string query, string[] matchFields) where T : class;
        Task<bool> PostAsync<T>(T data) where T : class;
        Task<bool> PostBulkAsync<T>(List<T> datas) where T : class;
        Task<bool> CreateIndexAsync<T>(string index) where T : class;
    }
}
