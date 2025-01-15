using System.Text.Json.Serialization;

namespace Ecom.Services.Search.Service.Model
{

    public class SearchModel
    {
        [JsonPropertyName("id")]
        public required string Id { get; set; }
        [JsonPropertyName("title")]
        public required string Title { get; set; }
        [JsonPropertyName("description")]
        public string? Description { get; set; }
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        [JsonPropertyName("rating")]
        public Rating? Rating { get; set; }
        [JsonPropertyName("category")]
        public string? Category { get; set; }
        [JsonPropertyName("tags")]
        public string? Tags { get; set; }

    }

    public class Rating
    {
        public float Rate { get; set; }
        public int Count { get; set; }
    }
}
