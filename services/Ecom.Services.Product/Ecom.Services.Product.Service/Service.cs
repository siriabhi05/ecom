using Ecom.Services.Common.Sqs.Interface;
using Ecom.Services.Product.Database.Interface;
using Ecom.Services.Product.Database.Model;
using Ecom.Services.Product.Service.Interface;
using Ecom.Services.Product.Service.Mapper;
using Ecom.Services.Product.Service.Model;

namespace Ecom.Services.Product.Service
{
    public class Service(IRepository repository, ISqs QueueService) : IService
    {
        public async Task<bool> CreateProductAsync(ProductServiceModel product)
        {
            var productDbModel = ProductModelMapper.ServiceToDatabaseModel(product);
            var isCreated = await repository.CreateProductAsync(productDbModel);
            if (isCreated) await QueueService.SendMessageAsync(productDbModel);
            return isCreated;
        }

        public async Task<bool> CreateProductsAsync(List<ProductServiceModel> products)
        {
            var productDbModels = products.Select(p => ProductModelMapper.ServiceToDatabaseModel(p)).ToList();
            var failedProducts = await repository.CreateProductsAsync(productDbModels);
            if (failedProducts != null)
            {
                if (failedProducts.Count() > 0) productDbModels = productDbModels.Where(p => !failedProducts.Contains(p.Id)).ToList();
                await QueueService.SendBulkMessageAsync(productDbModels
               .Select((p, i) => new Common.Sqs.Model.SqsBulkRequest<ProductDbModel> { Id = i.ToString(), Messages = p }).ToList());
            }

            return failedProducts?.Length == 0;
        }

        public async Task<List<ProductServiceModel>> GetAllProductsAsync()
        {
            var products = await repository.GetAllProductsAsync();
            if (products.Count == 0) { return []; }
            return products.Select(p => ProductModelMapper.DatabaseToServiceModel(p)).ToList();
        }

        public async Task<ProductServiceModel?> GetProductByIdAsync(string id)
        {
            var product = await repository.GetProductByIdAsync(id);
            return product != null ? ProductModelMapper.DatabaseToServiceModel(product) : default;
        }

        public async Task<List<ProductServiceModel>> GetProductsByCategoryAsync(string category)
        {
            var products = await repository.GetProductsByCategoryAsync(category);
            if (products.Count == 0) { return []; }
            return products.Select(p => ProductModelMapper.DatabaseToServiceModel(p)).ToList();
        }

        public async Task<bool> UpdateProductAsync(ProductServiceModel product)
        {
            return await repository.UpdateProductAsync(ProductModelMapper.ServiceToDatabaseModel(product, true));
        }

        public async Task<bool> DeleteProductsAsync()
        {
            return await repository.DeleteProductsAsync();
        }
    }
}
