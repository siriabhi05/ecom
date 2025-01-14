namespace Ecom.Services.Common.Sqs.Model
{
    public class SqsBulkRequest<T> where T : class
    {
        public required string Id { get; set; } 
        public required T Messages { get; set; } 
    }
}
