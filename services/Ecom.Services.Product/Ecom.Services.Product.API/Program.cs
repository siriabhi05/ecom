using Amazon.DynamoDBv2;
using Ecom.Services.Product.Database;
using Ecom.Services.Product.Database.Interface;
using Ecom.Services.Product.Service;
using Ecom.Services.Product.Service.Interface;
using Ecom.Services.Common.Sqs.Interface;
using Ecom.Services.Common.Sqs;
using Amazon.SQS;
using Ecom.Services.Common.Sqs.Model;
using Amazon;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using Ecom.Services.Product.Database.Model;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.Configure<SqsConfig>(builder.Configuration.GetSection("SqsConfig"));
        builder.Services.Configure<DynamoDbConfig>(builder.Configuration.GetSection("DynamoDbConfig"));
        string aKey = "XXX"; 
        string sKey = "XXX"; 


        builder.Services.AddControllers();
        builder.Services.AddSingleton<IAmazonDynamoDB, AmazonDynamoDBClient>(sp =>
        {
            var dynamoDbConfig = sp.GetRequiredService<IOptions<DynamoDbConfig>>().Value;
            RegionEndpoint region = RegionEndpoint.GetBySystemName(dynamoDbConfig.Region);
            return new AmazonDynamoDBClient(aKey, sKey, region: RegionEndpoint.GetBySystemName(dynamoDbConfig.Region));
        });
        builder.Services.AddSingleton<IAmazonSQS, AmazonSQSClient>(sp =>
        {
            var sqsConfig = sp.GetRequiredService<IOptions<SqsConfig>>().Value;
            RegionEndpoint region = RegionEndpoint.GetBySystemName(sqsConfig.Region);
            return new AmazonSQSClient(aKey, sKey, region);
        });
        builder.Services.AddSingleton<ISqs, Sqs>();
        builder.Services.AddSingleton<IService, Service>();
        builder.Services.AddSingleton<IRepository, Repository>();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}