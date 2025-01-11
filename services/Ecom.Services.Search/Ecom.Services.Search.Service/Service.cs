using Ecom.Services.Search.Database.Interface;
using Ecom.Services.Search.Service.Interface;
using Ecom.Services.Search.Service.Mapper;
using Ecom.Services.Search.Service.Model;

namespace Ecom.Services.Search.Service
{
    public class Service(IRepository repository) : IService
    {
        public async Task<bool> CreateIndexAsync(string index)
        {
            return await repository.CreateIndexAsync(index);
        }

        public async Task<bool> PostAsync(SearchServiceModel model)
        {
            return await repository.PostAsync(SearchModelMapper.ServiceToDatabaseModel(model));
        }

        public async Task<bool> PostBulkAsync(List<SearchServiceModel> models)
        {
            var dbModels = models.Select(m => SearchModelMapper.ServiceToDatabaseModel(m)).ToList();
            return await repository.PostBulkAsync(dbModels);
        }

        public async Task<List<SearchServiceModel>> QueryAsync(string query)
        {
            var searchDbModels = await repository.QueryAsync(query);
            return searchDbModels.Select(s => SearchModelMapper.DatabaseToServiceModel(s)).ToList();
        }
    }
}
