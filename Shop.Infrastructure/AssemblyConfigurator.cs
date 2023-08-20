using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Application.Contracts.DataSources;
using Shop.Application.Contracts.Repositories;
using Shop.Infrastructure.DataSources;
using Shop.Infrastructure.Repositories;

namespace Shop.Infrastructure;

public static class AssemblyConfigurator
{
    public static IServiceCollection ConfigureInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddTransient<ICustomerRepository, CustomerRepository>();
        services.AddTransient<ICustomerDataSource, CustomerDataSource>();

        services.AddTransient<IOrderRepository, OrderRepository>();
        services.AddTransient<IOrderDataSource, OrderDataSource>();

        services.AddDbContext<ShopDbContext>(optionsBuilder =>
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Sql"));
        });
        
        return services;
    }
}