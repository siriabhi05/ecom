using Ecom.Services.Product.Service.Model;

namespace Ecom.Services.Product.Service.Interface
{
    public interface IService
    {
        Task<ProductServiceModel?> GetProductByIdAsync(string id);
        Task<List<ProductServiceModel>> GetAllProductsAsync();
        Task<List<ProductServiceModel>> GetProductsByCategoryAsync(string category);
        Task<bool> CreateProductsAsync(List<ProductServiceModel> products);
        Task<bool> DeleteProductsAsync();
        Task<bool> CreateProductAsync(ProductServiceModel product);
        Task<bool> UpdateProductAsync(ProductServiceModel product);
    }
}
