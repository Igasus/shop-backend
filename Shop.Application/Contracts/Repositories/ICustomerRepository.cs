using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Domain.Entities;

namespace Shop.Application.Contracts.Repositories;

public interface ICustomerRepository
{
    Task<IList<Customer>> GetAllAsync();
    Task<Guid> CreateAsync(Customer input);
    Task SaveChangesAsync();
}