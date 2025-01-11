using Ecom.Services.Search.Service.Model;

namespace Ecom.Services.Search.Service.Interface
{
    public interface IService

    {
        Task<List<SearchServiceModel>> QueryAsync(string query);
        Task<bool> PostAsync(SearchServiceModel model);
        Task<bool> PostBulkAsync(List<SearchServiceModel> models);
        Task<bool> CreateIndexAsync(string index);
    }
}
