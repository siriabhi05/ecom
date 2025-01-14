namespace Ecom.Services.Common.Sqs.Model
{
    public class SqsBulkResponse
    {
        public List<string> Success { get; internal set; } = [];
        public List<string> Fail { get; internal set; } = [];
    }
}
