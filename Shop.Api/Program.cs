using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shop.Api.Middlewares;
using Shop.Application;
using Shop.Infrastructure;
using Shop.Infrastructure.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<AzureOptions>(builder.Configuration.GetSection(AzureOptions.Section));

builder.Services
    .ConfigureApplicationServices()
    .ConfigureInfrastructureServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

await app.Services.ActualizeMigrationsAsync();

app.Run();