using System.Linq;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Contracts.DataSources;
using Shop.Domain.Entities;

namespace Shop.Infrastructure.DataSources;

public class CustomerDataSource : ICustomerDataSource
{
    private readonly ShopDbContext _dbContext;

    public CustomerDataSource(ShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<Customer> Customers => _dbContext.Customers.AsNoTracking();
}