using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Application.Dto;

namespace Shop.Application.Contracts.Aggregates;

public interface IOrderAggregate
{
    Task<IList<OrderDto>> GetAllAsync();
    Task<OrderDto> GetByIdAsync(Guid id);
    Task<OrderDto> CreateAsync(OrderDtoInput input);
}