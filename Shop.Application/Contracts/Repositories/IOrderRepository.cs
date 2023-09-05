using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;

namespace Shop.Application.Contracts.Repositories;

public interface IOrderRepository
{
    DbSet<Order> Orders { get; }
    DbContext Context { get; }
}