using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Contracts.DataSources;
using Shop.Application.Contracts.Messaging;
using Shop.Application.Contracts.Repositories;
using Shop.Application.Contracts.Services;
using Shop.Application.Dto;
using Shop.Application.Dto.Messaging;
using Shop.Domain.Entities;
using Shop.Domain.Exceptions;

namespace Shop.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderDataSource _orderDataSource;
    private readonly ICustomerDataSource _customerDataSource;
    private readonly IMapper _mapper;
    private readonly IMessagePublisher _messagePublisher;

    public OrderService(
        IOrderRepository orderRepository,
        IOrderDataSource orderDataSource,
        ICustomerDataSource customerDataSource,
        IMapper mapper,
        IMessagePublisher messagePublisher)
    {
        _orderRepository = orderRepository;
        _orderDataSource = orderDataSource;
        _customerDataSource = customerDataSource;
        _mapper = mapper;
        _messagePublisher = messagePublisher;
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

        order.ResultDiscount.Value = Math.Min(order.Price.SubTotal,
            order.Price.SubTotal * (order.RequestedDiscount.Percent / 100) + order.RequestedDiscount.Value);
        order.ResultDiscount.Percent = 100 * order.ResultDiscount.Value / order.Price.SubTotal;
        order.Price.Total = Math.Max(0, order.Price.SubTotal - order.ResultDiscount.Value);

        await _orderRepository.Orders.AddAsync(order);
        await _orderRepository.Context.SaveChangesAsync();

        return order.Id;
    }

    public async Task PublishOrderCreatedMessage(Guid orderId)
    {
        var order = await _orderDataSource.Orders
            .ProjectTo<OrderDtoMessage>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(o => o.Id == orderId);

        if (order is null)
        {
            throw new NotFoundException(ErrorMessages.NotFound((Order o) => o.Id, orderId));
        }

        await _messagePublisher.PublishAsync(order);
    }
}