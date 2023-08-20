using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Contracts.Aggregates;
using Shop.Application.Contracts.DataSources;
using Shop.Application.Contracts.Repositories;
using Shop.Application.Dto;
using Shop.Domain.Entities;
using Shop.Domain.Exceptions;

namespace Shop.Application.Aggregates;

public class CustomerAggregate : ICustomerAggregate
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ICustomerDataSource _customerDataSource;
    private readonly IMapper _mapper;

    public CustomerAggregate(
        ICustomerRepository customerRepository,
        ICustomerDataSource customerDataSource,
        IMapper mapper)
    {
        _customerRepository = customerRepository;
        _customerDataSource = customerDataSource;
        _mapper = mapper;
    }

    public async Task<IList<CustomerDto>> GetAllAsync()
    {
        var customers = await _customerDataSource.Customers
            .ProjectTo<CustomerDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return customers;
    }

    public async Task<CustomerDto> GetByIdAsync(Guid id)
    {
        var customer = await _customerDataSource.Customers
            .ProjectTo<CustomerDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (customer is null)
        {
            throw new NotFoundException(ErrorMessages.NotFound((Customer c) => c.Id, id));
        }

        return customer;
    }

    public async Task<Guid> CreateAsync(CustomerDtoInput input)
    {
        var customer = _mapper.Map<Customer>(input);

        await _customerRepository.Customers.AddAsync(customer);
        await _customerRepository.Context.SaveChangesAsync();

        return customer.Id;
    }
}