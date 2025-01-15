using Ecom.Services.Common.OpenSearch.Interface;
using OpenSearch.Client;

namespace Ecom.Services.Common.OpenSearch
{

    public class OpenSearch(IOpenSearchClient client) : IOpenSearch
    {
        public async Task<bool> CreateIndexAsync<T>(string index) where T : class
        {
            var createIndexResponse = await client.Indices.CreateAsync(index, c => c
             .Map<T>(m => m
                 .AutoMap()
             ));
            return createIndexResponse.IsValid;
        }

        public async Task<bool> PostAsync<T>(T data) where T : class
        {
            var response = await client.IndexDocumentAsync(data);
            return response.IsValid;
        }

        public async Task<bool> PostBulkAsync<T>(List<T> datas) where T : class
        {
            var request = new BulkRequest
            {
                Operations = []
            };
            datas.ForEach(s => request.Operations.Add(new BulkIndexOperation<T>(s)));
            var response = await client.BulkAsync(request);
            return response.IsValid;
        }

        public async Task<List<T>> QueryAsync<T>(string query, string[] matchFields) where T : class
        {
            var response = await client.SearchAsync<T>(s => s
                 .Query(q => q
                 .MultiMatch(m => m
                 .Query(query)
                 .Fields(matchFields)
                 .Fuzziness(Fuzziness.Auto)
                 )));
            return response.IsValid ? response.Hits.Select(h => h.Source).ToList() : [];
        }
    }
}
