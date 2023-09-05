using System.Linq;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Contracts.DataSources;
using Shop.Domain.Entities;

namespace Shop.Infrastructure.DataSources;

public class OrderDataSource : IOrderDataSource
{
    private readonly ShopDbContext _dbContext;

    public OrderDataSource(ShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<Order> Orders => _dbContext.Orders.AsNoTracking();
}