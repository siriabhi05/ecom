using Ecom.Services.Common.OpenSearch.Interface;
using Ecom.Services.Search.Service.Interface;
using Ecom.Services.Search.Service.Model;

namespace Ecom.Services.Search.Service
{
    public class Service(IOpenSearch openSearch) : IService
    {
        public async Task<bool> CreateIndexAsync(string index)
        {
            return await openSearch.CreateIndexAsync<SearchModel>(index);
        }

        public async Task<bool> PostAsync(SearchModel model)
        {
            return await openSearch.PostAsync(model);
        }

        public async Task<bool> PostBulkAsync(List<SearchModel> models)
        {
            return await openSearch.PostBulkAsync(models);
        }

        public async Task<List<SearchModel>> QueryAsync(string query)
        {
            return await openSearch.QueryAsync<SearchModel>(query,
                [nameof(SearchModel.Id), nameof(SearchModel.Title), nameof(SearchModel.Description)]);
        }
    }
}
