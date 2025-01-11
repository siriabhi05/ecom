using System.Text.Json.Serialization;

namespace Ecom.Services.Product.Database.Model
{
    public class ProductDbModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        [JsonPropertyName("rating")]
        public Rating? Rating { get; set; }
        [JsonPropertyName("category")]
        public string Category { get; set; }
        [JsonPropertyName("tags")]
        public List<string> Tags { get; set; }
        public ProductDbModel()
        {
            Id = Guid.NewGuid().ToString();
            Title = string.Empty;
            Category = string.Empty;    
            Tags = [];
        }

    }

    public class Rating
    {
        public float Rate { get; set; }
        public int Count { get; set; }
    }
}
