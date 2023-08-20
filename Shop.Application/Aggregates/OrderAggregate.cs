using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.Application.Contracts.Aggregates;
using Shop.Application.Contracts.DataSources;
using Shop.Application.Contracts.Repositories;
using Shop.Application.Dto;

namespace Shop.Application.Aggregates;

public class OrderAggregate : IOrderAggregate
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderDataSource _orderDataSource;

    public OrderAggregate(IOrderRepository orderRepository, IOrderDataSource orderDataSource)
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