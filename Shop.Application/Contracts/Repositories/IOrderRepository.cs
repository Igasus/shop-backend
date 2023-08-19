using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Domain.Entities;

namespace Shop.Application.Contracts.Repositories;

public interface IOrderRepository
{
    Task<IList<Order>> GetAllAsync();
    Task<Order> GetByIdAsync(Guid id);
    Task<Guid> CreateAsync(Order input);
    Task SaveChangesAsync();
}