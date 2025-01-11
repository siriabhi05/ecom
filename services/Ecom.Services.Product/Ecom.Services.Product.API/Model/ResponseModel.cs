using Ecom.Services.Product.Service.Model;
using System.Net;

namespace Ecom.Services.Product.Api.Model
{

    public class ResponseModel
    {
        public HttpStatusCode StatusCode { get; set; } = System.Net.HttpStatusCode.OK;
        public string Message { get; set; } = string.Empty;
    }

    public class GetProductsResponseModel : ResponseModel
    {
        public List<ProductServiceModel> Products { get; set; } = [];
    }

    public class GetProductResponseModel : ResponseModel
    {
        public ProductServiceModel? Product { get; set; }
    }
}
