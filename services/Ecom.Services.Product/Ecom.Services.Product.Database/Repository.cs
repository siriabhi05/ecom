using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Ecom.Services.Product.Database.Interface;
using Ecom.Services.Product.Database.Model;
using Microsoft.Extensions.Options;
using System.Net;
using System.Text.Json;


namespace Ecom.Services.Product.Database
{
    public class Repository(IAmazonDynamoDB dynamoDB, IOptions<DynamoDbConfig> config) : IRepository
    {


        public async Task<List<ProductDbModel>> GetAllProductsAsync()
        {
            var scanRequest = new ScanRequest
            {
                TableName = config.Value.Table,
                Limit = 100
            };

            var response = await dynamoDB.ScanAsync(scanRequest);
            var products = new List<ProductDbModel>();

            foreach (var x in response.Items)
            {
                var json = Document.FromAttributeMap(x).ToJson();
                var product = JsonSerializer.Deserialize<ProductDbModel>(json);
                if (product != null)
                {
                    products.Add(product);
                }
            }

            return products;
        }

        public async Task<ProductDbModel?> GetProductByIdAsync(string id)
        {
            var getItemRequest = new GetItemRequest
            {
                TableName = config.Value.Table,
                Key = new Dictionary<string, AttributeValue>
            {
                { "id", new AttributeValue { S = id } },

            }
            };

            var response = await dynamoDB.GetItemAsync(getItemRequest);

            if (response.Item.Count == 0)
            {
                return null;
            }

            var itemAsDocument = Document.FromAttributeMap(response.Item);

            return itemAsDocument != null ? JsonSerializer.Deserialize<ProductDbModel>(itemAsDocument.ToJson()) : null;
        }

        //Inefficient, just for the testing
        public async Task<List<ProductDbModel>> GetProductsByCategoryAsync(string category)
        {
            var scanRequest = new ScanRequest
            {
                TableName = config.Value.Table,
                FilterExpression = "category = :category",
                Limit = 100,
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>
            {
                { ":category", new AttributeValue { S = category } }
            }
            };

            var response = await dynamoDB.ScanAsync(scanRequest);
            var products = new List<ProductDbModel>();

            foreach (var x in response.Items)
            {
                var json = Document.FromAttributeMap(x).ToJson();
                var product = JsonSerializer.Deserialize<ProductDbModel>(json);
                if (product != null)
                {
                    products.Add(product);
                }
            }

            return products;
        }

        public async Task<bool> CreateProductAsync(ProductDbModel product)
        {
            var productAsJson = JsonSerializer.Serialize(product);
            var productAsAttributes = Document.FromJson(productAsJson).ToAttributeMap();

            var putItemRequest = new PutItemRequest
            {
                TableName = config.Value.Table,
                Item = productAsAttributes
            };

            var response = await dynamoDB.PutItemAsync(putItemRequest);

            return response.HttpStatusCode == HttpStatusCode.OK;
        }

        public async Task<string[]> CreateProductsAsync(List<ProductDbModel> products)
        {
            var request = new List<WriteRequest>();
            foreach (var product in products)
            {
                var productAsJson = JsonSerializer.Serialize(product);
                var productAsAttributes = Document.FromJson(productAsJson).ToAttributeMap();

                var putRequest = new PutRequest
                {
                    Item = productAsAttributes
                };
                var writeRequest = new WriteRequest
                {
                    PutRequest = putRequest

                };
                request.Add(writeRequest);
            }
            var batchRequest = new Dictionary<string, List<WriteRequest>>
            {
                { config.Value.Table, request }
            };
            var response = await dynamoDB.BatchWriteItemAsync(batchRequest);
            return response.UnprocessedItems.Count == 0 ? [] : response.UnprocessedItems[config.Value.Table].Select(w => w.PutRequest.Item["id"].S).ToArray();
        }

        public async Task<bool> UpdateProductAsync(ProductDbModel product)
        {
            var productAsJson = JsonSerializer.Serialize(product);
            var productAsAttributes = Document.FromJson(productAsJson).ToAttributeMap();

            var updateItemRequest = new PutItemRequest
            {
                TableName = config.Value.Table,
                Item = productAsAttributes
            };

            var response = await dynamoDB.PutItemAsync(updateItemRequest);

            return response.HttpStatusCode == HttpStatusCode.OK;
        }

        public async Task<bool> DeleteProductsAsync()
        {
            var scanRequest = new ScanRequest
            {
                TableName = config.Value.Table,
                Limit = 100
            };

            var response = await dynamoDB.ScanAsync(scanRequest);
            var products = new List<ProductDbModel>();

            foreach (var item in response.Items)
            {

                var deleteRequest = new DeleteItemRequest
                {
                    TableName = config.Value.Table,
                    Key = new Dictionary<string, AttributeValue>() { { "id", item["id"] }, }
                };

                await dynamoDB.DeleteItemAsync(deleteRequest);
            }

            return true;
        }
    }

}
