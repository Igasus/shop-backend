using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Application.Contracts.Services;
using Shop.Application.Dto;

namespace Shop.Application.Services;

public class OrderService : IOrderService
{
    public Task<IList<OrderDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<OrderDto> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<OrderDto> CreateAsync(OrderDtoInput input)
    {
        throw new NotImplementedException();
    }
}