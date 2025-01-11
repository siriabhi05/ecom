using Ecom.Services.Search.Database.Interface;
using Ecom.Services.Search.Database.Model;
using OpenSearch.Client;

namespace Ecom.Services.Search.Database
{
    public class Repository(IOpenSearchClient client) : IRepository
    {
        public async Task<List<SearchDbModel>> QueryAsync(string query)
        {
            var response = await client.SearchAsync<SearchDbModel>(s => s
            .Query(q => q
            .MultiMatch(m => m
            .Fields(f => f
            .Field(p => p.Title)
            .Field(p => p.Description)
            .Field(p => p.Category)
            )
            .Query(query)
            .Fuzziness(Fuzziness.Auto)
            )));
            return response.IsValid ? response.Hits.Select(h => h.Source).ToList() : [];
        }

        public async Task<bool> CreateIndexAsync(string index)
        {
            var response = await client.Indices.CreateAsync(index, c => c
            .Map<SearchDbModel>(m => m
            .AutoMap()
            .Properties(p => p
            .Text(t => t.Name(n => n.Id))
            .Text(t => t.Name(n => n.Title))
            .Text(t => t.Name(n => n.Description))
            .Number(n => n.Name(n => n.Price))
            .Keyword(k => k.Name(n => n.Category))
            .Text(t => t.Name(n => n.Tags))
            .Object<Rating>(o => o.Name(n => n.Rating))
            )));


            return response.IsValid;
        }

        public async Task<bool> PostAsync(SearchDbModel searchDbModel)
        {
            var response = await client.IndexDocumentAsync(searchDbModel);
            return response.IsValid;
        }

        public async Task<bool> PostBulkAsync(List<SearchDbModel> searchDbModels)
        {
            var request = new BulkRequest
            {
                Operations = []
            };
            searchDbModels.ForEach(s => request.Operations.Add(new BulkIndexOperation<SearchDbModel>(s)));
            var response = await client.BulkAsync(request);
            return response.IsValid;
        }
    }
}
