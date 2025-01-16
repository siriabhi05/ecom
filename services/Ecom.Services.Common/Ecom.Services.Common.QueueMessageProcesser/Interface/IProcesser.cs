
namespace Ecom.Services.Common.QueuePoller.Interface
{
    public interface IProcesser<T> where T : class
    {
        Task ProcessAsync();
        Task<bool> ProcessMessagesAsync(List<T> messageses);
    }
}