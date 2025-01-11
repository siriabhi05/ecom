using System.Text.Json.Serialization;

namespace Ecom.Services.Search.Database.Model
{
    public class SearchDbModel
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
        public string Tags { get; set; }
        public SearchDbModel()
        {
            Id = Guid.NewGuid().ToString();
            Title = string.Empty;
            Category = string.Empty;
            Tags = string.Empty;
        }
    }


    public class Rating
    {
        public float Rate { get; set; }
        public int Count { get; set; }
    }

}
