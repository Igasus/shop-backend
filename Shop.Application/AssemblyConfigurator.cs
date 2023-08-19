using Microsoft.Extensions.DependencyInjection;
using Shop.Application.Contracts.Services;
using Shop.Application.Services;

namespace Shop.Application;

public static class AssemblyConfigurator
{
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<ICustomerService, CustomerService>();
        services.AddTransient<IOrderService, OrderService>();
        
        return services;
    }
}