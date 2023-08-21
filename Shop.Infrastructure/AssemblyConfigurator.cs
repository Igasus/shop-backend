using System;
using System.Linq;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Application.Contracts.DataSources;
using Shop.Application.Contracts.Messaging;
using Shop.Application.Contracts.Repositories;
using Shop.Infrastructure.DataSources;
using Shop.Infrastructure.Messaging;
using Shop.Infrastructure.Options;
using Shop.Infrastructure.Repositories;

namespace Shop.Infrastructure;

public static class AssemblyConfigurator
{
    public static IServiceCollection ConfigureInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ShopDbContext>(optionsBuilder =>
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Sql"));
        });

        var azureOptionsSection = configuration.GetSection(AzureOptions.Section);
        services.Configure<AzureOptions>(options => azureOptionsSection.Bind(options));

        services.AddSingleton(_ =>
            new ServiceBusClient(azureOptionsSection.Get<AzureOptions>().ServiceBus.ConnectionString));
        
        services.AddTransient<ICustomerRepository, CustomerRepository>();
        services.AddTransient<ICustomerDataSource, CustomerDataSource>();

        services.AddTransient<IOrderRepository, OrderRepository>();
        services.AddTransient<IOrderDataSource, OrderDataSource>();

        services.AddTransient<IMessagePublisher, AzureServiceBusMessagePublisher>();
        
        return services;
    }

    public static async Task ActualizeMigrationsAsync(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        await using var dbContext = scope.ServiceProvider.GetRequiredService<ShopDbContext>();

        if ((await dbContext.Database.GetPendingMigrationsAsync()).Any())
        {
            await dbContext.Database.MigrateAsync();
        }
    }
}