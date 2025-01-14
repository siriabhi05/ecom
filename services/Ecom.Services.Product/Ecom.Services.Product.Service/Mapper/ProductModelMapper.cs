using Ecom.Services.Product.Database.Model;
using Ecom.Services.Product.Service.Model;

namespace Ecom.Services.Product.Service.Mapper
{
    public static class ProductModelMapper
    {
        public static ProductDbModel ServiceToDatabaseModel(ProductServiceModel model, bool isUpdate= false)
        {
            return new ProductDbModel
            {
                Category = model.Category,
                Description = model.Description,
                Id = isUpdate? model.Id :Guid.NewGuid().ToString(),
                Price = model.Price,
                Rating = model.Rating != null ? new Database.Model.Rating { Count = model.Rating.Count, Rate = model.Rating.Rate } : null,
                Tags = model.Tags,
                Title = model.Title,
            };
        }

        public static ProductServiceModel DatabaseToServiceModel(ProductDbModel model)
        {
            return new ProductServiceModel
            {
                Category = model.Category,
                Description = model.Description,
                Id = model.Id,
                Price = model.Price,
                Rating = model.Rating != null ? new Model.Rating { Count = model.Rating.Count, Rate = model.Rating.Rate } : null,
                Tags = model.Tags,
                Title = model.Title,
            };
        }
    }
}
