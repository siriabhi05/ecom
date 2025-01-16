using Amazon.SQS;
using Amazon.SQS.Model;
using Ecom.Services.Common.Sqs.Interface;
using Ecom.Services.Common.Sqs.Model;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Ecom.Services.Common.Sqs
{
    public class Sqs(IAmazonSQS client, IOptions<SqsConfig> sqsConfig) : ISqs
    {
        public async Task<List<T>> GetMessagesAsync<T>() where T : class
        {
            var getMessageResponse = new List<T>();
            try
            {
                var receiveMessageRequest = new ReceiveMessageRequest
                {
                    QueueUrl = sqsConfig.Value.QueueUrl,
                    MaxNumberOfMessages = 10,
                    WaitTimeSeconds = 20,
                    VisibilityTimeout = 30
                };
                var response = await client.ReceiveMessageAsync(receiveMessageRequest);
                if (response != null)
                {
                    foreach (var message in response.Messages)
                    {
                        if (message != null)
                        {
                            var item = JsonConvert.DeserializeObject<T>(message.Body);
                            if (item != null) getMessageResponse.Add(item);
                        };

                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }
            return getMessageResponse;

        }

        public async Task<SqsBulkResponse> SendBulkMessageAsync<T>(List<SqsBulkRequest<T>> sqsBulkRequests) where T : class
        {
            var sqsBulkResponse = new SqsBulkResponse();
            try
            {
                int i = 0;
                while (i < sqsBulkRequests.Count)
                {
                    var entries = new List<SendMessageBatchRequestEntry>();
                    for (int j = i; j < i + 10; j++)
                    {
                        entries.Add(new SendMessageBatchRequestEntry
                        {
                            Id = sqsBulkRequests[j].Id,
                            MessageBody = JsonConvert.SerializeObject(sqsBulkRequests[j].Messages),
                            MessageGroupId = "sync-product",
                            MessageDeduplicationId = Guid.NewGuid().ToString()
                        });
                    }
                    var sendMessageBatchRequest = new SendMessageBatchRequest
                    {
                        QueueUrl = sqsConfig.Value.QueueUrl,
                        Entries = entries.ToList(),

                    };
                    var response = await client.SendMessageBatchAsync(sendMessageBatchRequest);
                    sqsBulkResponse.Success.AddRange(response.Successful.Select(e => e.Id).ToList());
                    sqsBulkResponse.Fail.AddRange(response.Failed.Select(e => e.Id).ToList());
                    i += 10;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return sqsBulkResponse;

        }

        public async Task<bool> SendMessageAsync<T>(T message) where T : class
        {
            try
            {
                var request = new SendMessageRequest
                {
                    QueueUrl = sqsConfig.Value.QueueUrl,
                    MessageBody = JsonConvert.SerializeObject(message),
                    MessageGroupId = "sync-product",
                    MessageDeduplicationId = Guid.NewGuid().ToString()

                };
                var response = await client.SendMessageAsync(request);
                return response.HttpStatusCode == System.Net.HttpStatusCode.OK;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return false;
        }
    }
}


