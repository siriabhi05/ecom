using Ecom.Services.Search.Service.Model;
using System.Net;

namespace Ecom.Services.Search.API.Model
{
    public class ResponseModel
    {
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        public string Message { get; set; } = string.Empty;
    }

    public class SearchQueryResponseModel : ResponseModel
    {
        public List<SearchModel> searchProducts { get; set; } = [];
    }
}
