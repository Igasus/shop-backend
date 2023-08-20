using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Application.Dto;

namespace Shop.Application.Contracts.Aggregates;

public interface ICustomerAggregate
{
    Task<IList<CustomerDto>> GetAllAsync();
    Task<CustomerDto> GetByIdAsync(Guid id);
    Task<Guid> CreateAsync(CustomerDtoInput input);
}