using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProductUploader
{
    internal class Product
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public Decimal Price { get; set; }
        public Rating? Rating { get; set; }
        public string Category { get; set; } = string.Empty;

    }

    public class Rating
    {
        public float Rate { get; set; }
        public int Count { get; set; }
    }
}
