using Microsoft.Extensions.DependencyInjection;
using Shop.Application.Contracts.Repositories;
using Shop.Infrastructure.Repositories;

namespace Shop.Infrastructure;

public static class AssemblyConfigurator
{
    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services)
    {
        services.AddTransient<ICustomerRepository, CustomerRepository>();
        services.AddTransient<IOrderRepository, OrderRepository>();
        
        return services;
    }
}