using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Contracts.Aggregates;
using Shop.Application.Dto;
using Shop.Domain.Exceptions;

namespace Shop.Api.Controllers;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Route("api/orders")]
public class OrderController : ControllerBase
{
    private readonly IOrderAggregate _orderAggregate;

    public OrderController(IOrderAggregate orderAggregate)
    {
        _orderAggregate = orderAggregate;
    }

    /// <summary>
    /// Get all Orders.
    /// </summary>
    /// <returns>ActionResult with List of Orders.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IList<OrderDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync()
    {
        var orders = await _orderAggregate.GetAllAsync();

        return Ok(orders);
    }

    /// <summary>
    /// Get Order by Id.
    /// </summary>
    /// <param name="orderId">Id of Order.</param>
    /// <returns>ActionResult with Order.</returns>
    /// <exception cref="NotFoundException">Order with specified Id does not exist.</exception>
    [HttpGet("{orderId:guid}")]
    [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid orderId)
    {
        var order = await _orderAggregate.GetByIdAsync(orderId);

        return Ok(order);
    }

    /// <summary>
    /// Create Order.
    /// </summary>
    /// <param name="input">Order Input data.</param>
    /// <returns>ActionResult with created Order.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAsync([FromBody] OrderDtoInput input)
    {
        var orderId = await _orderAggregate.CreateAsync(input);
        var order = await _orderAggregate.GetByIdAsync(orderId);

        return Ok(order);
    }
}