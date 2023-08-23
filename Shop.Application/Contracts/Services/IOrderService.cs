using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Application.Dto;
using Shop.Domain.Enums;

namespace Shop.Application.Contracts.Services;

public interface IOrderService
{
    Task<IList<OrderDto>> GetAllAsync();
    Task<OrderDto> GetByIdAsync(Guid id);
    Task<Guid> CreateAsync(OrderDtoInput input);
    Task PublishOrderCreatedMessageAsync(Guid orderId);
    Task UpdateOrderStatusAsync(Guid orderId, OrderStatus status);
}