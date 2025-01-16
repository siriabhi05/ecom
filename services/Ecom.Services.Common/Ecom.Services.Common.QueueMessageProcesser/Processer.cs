using Ecom.Services.Common.QueuePoller.Interface;
using Ecom.Services.Common.Sqs.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Services.Common.QueuePoller
{
    public abstract class Processer<T>(ISqs sqs) : IProcesser<T> where T : class
    {
        public async Task ProcessAsync()
        {
            var messages = await GetMessagesFromQueueAsync();
            var isProcessed = await ProcessMessagesAsync(messages);
            Console.WriteLine(isProcessed);
        }
        private async Task<List<T>> GetMessagesFromQueueAsync()
        {
            return await sqs.GetMessagesAsync<T>();
        }

        public abstract Task<bool> ProcessMessagesAsync(List<T> messageses);
    }
}
