using Ecom.Services.Search.Service.Model;

namespace Ecom.Services.Search.Service.Interface
{
    public interface IService

    {
        Task<List<SearchModel>> QueryAsync(string query);
        Task<bool> PostAsync(SearchModel model);
        Task<bool> PostBulkAsync(List<SearchModel> models);
        Task<bool> CreateIndexAsync(string index);
    }
}
