using Amazon.Runtime;
using Ecom.Services.Search.API.Model;
using Ecom.Services.Search.Database;
using Ecom.Services.Search.Database.Interface;
using Ecom.Services.Search.Service;
using Ecom.Services.Search.Service.Interface;
using Microsoft.Extensions.Options;
using OpenSearch.Client;
using OpenSearch.Net;
using OpenSearch.Net.Auth.AwsSigV4;


internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.Configure<AWSConfig>(builder.Configuration.GetSection("AWS"));

        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddSingleton<IOpenSearchClient>(sp =>
        {
            var awsConfig = sp.GetRequiredService<IOptions<AWSConfig>>().Value;
            var pool = new SingleNodeConnectionPool(new Uri(awsConfig.Uri));
            var httpConnection = new AwsSigV4HttpConnection(new BasicAWSCredentials("XXXXX", "XXXX"),
                Amazon.RegionEndpoint.GetBySystemName(awsConfig.Region));

            var connectionSettings = new ConnectionSettings(pool, httpConnection)
                .EnableDebugMode()
                .DefaultIndex(awsConfig.Index);

            var opensearchClient = new OpenSearchClient(connectionSettings);
            return opensearchClient;
        });
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

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}