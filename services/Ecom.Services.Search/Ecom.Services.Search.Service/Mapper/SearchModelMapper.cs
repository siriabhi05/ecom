using Ecom.Services.Search.Database.Model;
using Ecom.Services.Search.Service.Model;

namespace Ecom.Services.Search.Service.Mapper
{
    public static class SearchModelMapper
    {
        public static SearchDbModel ServiceToDatabaseModel(SearchServiceModel model, bool isUpdate = false)
        {
            return new SearchDbModel
            {
                Category = model.Category,
                Description = model.Description,
                Id = isUpdate ? model.Id : Guid.NewGuid().ToString(),
                Price = model.Price,
                Rating = model.Rating != null ? new Database.Model.Rating { Count = model.Rating.Count, Rate = model.Rating.Rate } : null,
                Tags = string.Join(',', model.Tags),
                Title = model.Title,
            };
        }

        public static SearchServiceModel DatabaseToServiceModel(SearchDbModel model)
        {
            return new SearchServiceModel
            {
                Category = model.Category,
                Description = model.Description,
                Id = model.Id,
                Price = model.Price,
                Rating = model.Rating != null ? new Model.Rating { Count = model.Rating.Count, Rate = model.Rating.Rate } : null,
                Tags = [.. model.Tags.Split(',')],
                Title = model.Title,
            };
        }
    }
}
