using Microsoft.EntityFrameworkCore;
using Shop.Application.Contracts.Repositories;
using Shop.Domain.Entities;

namespace Shop.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly ShopDbContext _dbContext;

    public OrderRepository(ShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public DbSet<Order> Orders => _dbContext.Orders;
    public DbContext Context => _dbContext;
}