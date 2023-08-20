using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Application.Contracts.DataSources;
using Shop.Application.Contracts.Repositories;
using Shop.Application.Contracts.Services;
using Shop.Application.Dto;

namespace Shop.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderDataSource _orderDataSource;

    public OrderService(IOrderRepository orderRepository, IOrderDataSource orderDataSource)
    {
        _orderRepository = orderRepository;
        _orderDataSource = orderDataSource;
    }

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