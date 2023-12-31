using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Application.Dto;

namespace Shop.Application.Contracts.Services;

public interface ICustomerService
{
    Task<IList<CustomerDto>> GetAllAsync();
    Task<CustomerDto> GetByIdAsync(Guid id);
    Task<Guid> CreateAsync(CustomerDtoInput input);
}