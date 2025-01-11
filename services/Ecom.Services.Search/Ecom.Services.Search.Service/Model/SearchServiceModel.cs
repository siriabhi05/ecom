namespace Ecom.Services.Search.Service.Model
{

    public class SearchServiceModel
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public Rating? Rating { get; set; }
        public string Category { get; set; } = string.Empty;
        public List<string> Tags { get; set; } = [];

    }

    public class Rating
    {
        public float Rate { get; set; }
        public int Count { get; set; }
    }
}
