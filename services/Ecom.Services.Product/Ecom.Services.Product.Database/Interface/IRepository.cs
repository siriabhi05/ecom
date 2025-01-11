using Ecom.Services.Product.Database.Model;

namespace Ecom.Services.Product.Database.Interface
{
    public interface IRepository
    {
        Task<ProductDbModel?> GetProductByIdAsync(string id);
        Task<List<ProductDbModel>> GetAllProductsAsync();
        Task<List<ProductDbModel>> GetProductsByCategoryAsync(string category);
        Task<string[]> CreateProductsAsync(List<ProductDbModel> products);
        Task<bool> CreateProductAsync(ProductDbModel product);
        Task<bool> UpdateProductAsync(ProductDbModel product);
        Task<bool> DeleteProductsAsync();
    }
}
