using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Application.Contracts.Repositories;
using Shop.Domain.Entities;

namespace Shop.Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    public Task<IList<Customer>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Guid> CreateAsync(Customer input)
    {
        throw new NotImplementedException();
    }

    public Task SaveChangesAsync()
    {
        throw new NotImplementedException();
    }
}