using Ecom.Services.Search.Database.Model;

namespace Ecom.Services.Search.Database.Interface
{
    public interface IRepository
    {
        Task<List<SearchDbModel>> QueryAsync(string query);
        Task<bool> PostAsync(SearchDbModel model);
        Task<bool> PostBulkAsync(List<SearchDbModel> searchDbModels);
        Task<bool> CreateIndexAsync(string index);
    }
}
