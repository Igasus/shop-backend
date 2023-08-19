using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Application.Contracts.Repositories;
using Shop.Domain.Entities;

namespace Shop.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    public Task<IList<Order>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Order> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Guid> CreateAsync(Order input)
    {
        throw new NotImplementedException();
    }

    public Task SaveChangesAsync()
    {
        throw new NotImplementedException();
    }
}