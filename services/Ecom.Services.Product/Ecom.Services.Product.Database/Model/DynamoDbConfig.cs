namespace Ecom.Services.Product.Database.Model
{
    public class DynamoDbConfig
    {
        public string Table {  get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
    }
}
