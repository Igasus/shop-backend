using Microsoft.EntityFrameworkCore;
using Shop.Application.Contracts.Repositories;
using Shop.Domain.Entities;

namespace Shop.Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly ShopDbContext _dbContext;

    public CustomerRepository(ShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public DbSet<Customer> Customers => _dbContext.Customers;
    public DbContext Context => _dbContext;
}