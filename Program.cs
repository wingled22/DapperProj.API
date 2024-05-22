using Amazon.SecretsManager.Model;
using Amazon.SecretsManager;
using Dapper;
using Proj.API.Configurations;
using Proj.API.Data;
using Proj.API.Repositories;
using Proj.API.TypeHandlers;
using Amazon;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;



var secretConnection = configuration.GetSection(nameof(SqliteConnectionString)).Get<SqliteConnectionString>()!;
secretConnection.Validate();

// var secretConnection = await GetSecret<SqliteConnectionString>();
// secretConnection.Validate();

builder.Services.AddSingleton<IDatabaseProvider>(secretConnection);

builder.Services.AddSingleton<IDapperContext, DapperContext>();
builder.Services.AddSingleton<ClientRepository>();
builder.Services.AddSingleton<ContributionRepository>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

SqlMapper.AddTypeHandler(new GuidTypeHandler());



var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

// //TODO : replicate cati format of configs
// static async Task<T> GetSecret<T>()
// {
//     string secretName = "test-secret";
//     string region = "us-west-2";

//     IAmazonSecretsManager client = new AmazonSecretsManagerClient(RegionEndpoint.GetBySystemName(region));

//     GetSecretValueRequest request = new GetSecretValueRequest
//     {
//         SecretId = secretName,
//         VersionStage = "AWSCURRENT", 
//     };

//     GetSecretValueResponse response;

//     try
//     {
//         response = await client.GetSecretValueAsync(request);
//     }
//     catch (Exception e)
//     {
//         // For a list of the exceptions thrown, see
//         // https://docs.aws.amazon.com/secretsmanager/latest/apireference/API_GetSecretValue.html
//         throw e;
//     }

//     string secret = response.SecretString;

//     return JsonSerializer.Deserialize<T>(secret)!;

// }