using Microsoft.Extensions.DependencyInjection;
using Shop.Application.Aggregates;
using Shop.Application.Contracts.Aggregates;

namespace Shop.Application;

public static class AssemblyConfigurator
{
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<ICustomerAggregate, CustomerAggregate>();
        services.AddTransient<IOrderAggregate, OrderAggregate>();
        
        return services;
    }
}