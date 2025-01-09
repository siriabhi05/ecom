using Ecom.Services.Product.Api.Model;
using Ecom.Services.Product.Service.Interface;
using Ecom.Services.Product.Service.Model;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Ecom.Services.Product.Api.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController(IService service) : ControllerBase
    {

        [HttpGet]
        [Route("list")]
        public async Task<GetProductsResponseModel> GetProductsAsync()
        {
            var response = new GetProductsResponseModel();
            try
            {
                var products = await service.GetAllProductsAsync();
                response.Products = products;

            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex, response);
            }
            return response;

        }

        [HttpGet]
        public async Task<GetProductResponseModel> GetProductAsync([Required] string id)
        {
            var response = new GetProductResponseModel();
            try
            {
                var product = await service.GetProductByIdAsync(id);
                if (product == null)
                {
                    response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    response.Message = " This product is unavailable";
                }
                else
                {
                    response.Product = product;
                }
            }
            catch (Exception ex)
            {

                ExceptionHandler.Handle(ex, response);
            }
            return response;

        }


        [HttpGet]
        [Route("byCategory")]
        public async Task<GetProductsResponseModel> GetProductsByCategoryAsync([Required] string category)
        {
            var response = new GetProductsResponseModel();
            try
            {
                var products = await service.GetProductsByCategoryAsync(category);
                response.Products = products;
            }
            catch (Exception ex)
            {

                ExceptionHandler.Handle(ex, response);
            }
            return response;

        }

        [HttpPost]
        public async Task<ResponseModel> CreateProductAsync([FromBody] ProductServiceModel productServiceModel)
        {
            var response = new ResponseModel();
            try
            {
                var created = await service.CreateProductAsync(productServiceModel);
                if (!created)
                {
                    throw new Exception("unable to create this product at the moment");
                }
            }
            catch (Exception ex)
            {

                ExceptionHandler.Handle(ex, response);
            }
            return response;
        }


        [HttpPost]
        [Route("batch")]
        public async Task<ResponseModel> CreateProductsAsync([FromBody] List<ProductServiceModel> productServiceModels)
        {
            var response = new ResponseModel();
            try
            {
                var created = await service.CreateProductsAsync(productServiceModels);
                if (!created)
                {
                    throw new Exception("Some product failed to create");
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex, response);
            }
            return response;
        }

        [HttpPatch]
        public async Task<ResponseModel> UpdateProductAsync([FromBody] ProductServiceModel productServiceModel)
        {
            var response = new ResponseModel();
            if (string.IsNullOrEmpty(productServiceModel.Id))
            {
                throw new Exception("Invalid product");
            }
            try
            {
                var created = await service.UpdateProductAsync(productServiceModel);
                if (!created)
                {
                    throw new Exception("unable to update this product at the moment");
                }
            }
            catch (Exception ex)
            {

                ExceptionHandler.Handle(ex, response);
            }
            return response;
        }

        [HttpDelete]
        public async Task<ResponseModel> DeleteProductsAsync()
        {
            var response = new ResponseModel();
            try
            {
                var deleted = await service.DeleteProductsAsync();
                if (!deleted)
                {
                    throw (new Exception("Unable to delete the products at this momemnt"));
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
