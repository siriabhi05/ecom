using Ecom.Services.Search.API.Model;
using Ecom.Services.Search.Service.Interface;
using Ecom.Services.Search.Service.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Ecom.Services.Search.API.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchController(IService service, IOptions<AWSConfig> aws) : ControllerBase
    {
        [HttpGet]
        [Route("query")]
        public async Task<SearchQueryResponseModel> QueryAsync(string query)
        {
            var response = new SearchQueryResponseModel();
            try
            {
                var queryResult = await service.QueryAsync(query);
                if (queryResult != null)
                {
                    response.searchProducts = queryResult;
                }

            }
            catch (Exception ex)
            {

                ExceptionHandler.Handle(ex, response);
            }
            return response;
        }

        [HttpPost]
        [Route("create/bulk")]
        public async Task<ResponseModel> BulkPost([FromBody] List<SearchServiceModel> models)
        {
            var response = new ResponseModel();
            try
            {
                var isPosted = await service.PostBulkAsync(models);
                if (!isPosted)
                {
                    throw new Exception("unable to post products for search at the moment");
                }

            }
            catch (Exception ex)
            {

                ExceptionHandler.Handle(ex, response);
            }
            return response;
        }

        [HttpPost]
        [Route("create")]
        public async Task<ResponseModel> Post([FromBody] SearchServiceModel model)
        {
            var response = new ResponseModel();
            try
            {
                var isPosted = await service.PostAsync(model);
                if (!isPosted)
                {
                    throw new Exception("unable to post this product for search at the moment");
                }

            }
            catch (Exception ex)
            {

                ExceptionHandler.Handle(ex, response);
            }
            return response;
        }

        [HttpPost]
        [Route("index")]
        public async Task<ResponseModel> CreateIndexAsync()
        {
            var response = new ResponseModel();
            try
            {
                var created = await service.CreateIndexAsync(aws.Value.Index);
                if (!created)
                {
                    throw (new Exception("Unable to create index at this moment."));
                }
            }
            catch (Exception ex)
            {

                ExceptionHandler.Handle(ex, response);
            }
            return response;
        }

    }
}
