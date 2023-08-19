using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Application.Contracts.Services;
using Shop.Application.Dto;

namespace Shop.Application.Services;

public class CustomerService : ICustomerService
{
    public Task<IList<CustomerDto>> GetAllAsync()
    {
        throw new System.NotImplementedException();
    }

    public Task<CustomerDto> CreateAsync(CustomerDtoInput input)
    {
        throw new System.NotImplementedException();
    }
}