using Proj.API.Configurations;
using Proj.API.Data;
using Proj.API.Repositories;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

var sqliteConnection = configuration.GetSection(nameof(SqliteConnectionString)).Get<SqliteConnectionString>()!;
sqliteConnection.Validate();
builder.Services.AddSingleton<IDatabaseProvider>(sqliteConnection);
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddSingleton<ClientRepository>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


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
