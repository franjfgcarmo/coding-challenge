using Customer.Api;
using Customer.Api.Infrastructure.Data.SeedWord;
using Customer.Api.Infrastructure.Filters;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var services = builder.Services;
var configuration = builder.Configuration;

services.AddEndpointsApiExplorer()
    .AddSwaggerGen(c => c.EnableAnnotations());
services.AddSwaggerGen();
services.AddControllers(options =>
    options.Filters.Add<ApiExceptionFilterAttribute>());
services.Configure<ApiBehaviorOptions>(options =>
    options.SuppressModelStateInvalidFilter = true);
services.AddInfrastructure(configuration);
services.AddHostedService<DatabaseSeeder>();


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


//app.UseHttpsRedirection();


app.MapControllers();

app.Run();

namespace Customer.Host
{
    public partial class Program
    {
    }
}