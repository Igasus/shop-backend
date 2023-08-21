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

public class OrderAggregate : IOrderAggregate
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderDataSource _orderDataSource;
    private readonly ICustomerDataSource _customerDataSource;
    private readonly IMapper _mapper;

    public OrderAggregate(
        IOrderRepository orderRepository,
        IOrderDataSource orderDataSource,
        ICustomerDataSource customerDataSource,
        IMapper mapper)
    {
        _orderRepository = orderRepository;
        _orderDataSource = orderDataSource;
        _customerDataSource = customerDataSource;
        _mapper = mapper;
    }

    public async Task<IList<OrderDto>> GetAllAsync()
    {
        var orders = await _orderDataSource.Orders
            .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return orders;
    }

    public async Task<OrderDto> GetByIdAsync(Guid id)
    {
        var order = await _orderDataSource.Orders
            .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (order is null)
        {
            throw new NotFoundException(ErrorMessages.NotFound((Order o) => o.Id, id));
        }

        return order;
    }

    public async Task<Guid> CreateAsync(OrderDtoInput input)
    {
        var customerExist = await _customerDataSource.Customers
            .AnyAsync(c => c.Id == input.CustomerId);
        
        if (!customerExist)
        {
            throw new NotFoundException(ErrorMessages.NotFound((Customer c) => c.Id, input.CustomerId));
        }
        
        var order = _mapper.Map<Order>(input);

        foreach (var orderProduct in order.Products)
        {
            orderProduct.Price.Total = orderProduct.Price.SubTotal * orderProduct.Unit.Quantity;
            order.Price.SubTotal += orderProduct.Price.Total;
        }

        order.Discount.Total = order.Price.SubTotal * (order.Discount.Percent / 100) + order.Discount.Value;
        order.Price.Total = Math.Max(0, order.Price.SubTotal - order.Discount.Total);

        await _orderRepository.Orders.AddAsync(order);
        await _orderRepository.Context.SaveChangesAsync();

        return order.Id;
    }
}