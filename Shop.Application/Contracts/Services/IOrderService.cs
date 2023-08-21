using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Application.Dto;

namespace Shop.Application.Contracts.Services;

public interface IOrderService
{
    Task<IList<OrderDto>> GetAllAsync();
    Task<OrderDto> GetByIdAsync(Guid id);
    Task<Guid> CreateAsync(OrderDtoInput input);
}