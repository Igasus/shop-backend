using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Application.Contracts.Aggregates;
using Shop.Application.Contracts.DataSources;
using Shop.Application.Contracts.Repositories;
using Shop.Application.Dto;

namespace Shop.Application.Aggregates;

public class CustomerAggregate : ICustomerAggregate
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ICustomerDataSource _customerDataSource;

    public CustomerAggregate(ICustomerRepository customerRepository, ICustomerDataSource customerDataSource)
    {
        _customerRepository = customerRepository;
        _customerDataSource = customerDataSource;
    }

    public Task<IList<CustomerDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<CustomerDto> CreateAsync(CustomerDtoInput input)
    {
        throw new System.NotImplementedException();
    }
}