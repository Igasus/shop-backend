using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;

namespace Shop.Application.Contracts.Repositories;

public interface ICustomerRepository
{
    DbSet<Customer> Customers { get; }
    DbContext Context { get; }
}