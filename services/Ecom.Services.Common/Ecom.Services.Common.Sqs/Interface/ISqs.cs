using Ecom.Services.Common.Sqs.Model;

namespace Ecom.Services.Common.Sqs.Interface
{
    public interface ISqs
    {
        Task<bool> SendMessageAsync<T>(T message) where T : class;
        Task<SqsBulkResponse> SendBulkMessageAsync<T>(List<SqsBulkRequest<T>> sqsBulkRequests) where T : class;
        Task<List<T>> GetMessagesAsync<T>() where T : class;
    }
}
