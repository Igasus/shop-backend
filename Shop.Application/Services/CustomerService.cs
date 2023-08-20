using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Contracts.DataSources;
using Shop.Application.Contracts.Repositories;
using Shop.Application.Contracts.Services;
using Shop.Application.Dto;

namespace Shop.Application.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ICustomerDataSource _customerDataSource;

    public CustomerService(ICustomerRepository customerRepository, ICustomerDataSource customerDataSource)
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