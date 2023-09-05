using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Common;
using Shop.Application.Contracts.Services;
using Shop.Application.Dto;
using Shop.Domain.Exceptions;

namespace Shop.Api.Controllers;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Route("api/orders")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    /// <summary>
    /// Get all Orders.
    /// </summary>
    /// <returns>ActionResult with List of Orders.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IList<OrderDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync()
    {
        var orders = await _orderService.GetAllAsync();

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
    [ProducesResponseType(typeof(ErrorResponseBody), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid orderId)
    {
        var order = await _orderService.GetByIdAsync(orderId);

        return Ok(order);
    }

    /// <summary>
    /// Create Order.
    /// </summary>
    /// <param name="input">Order Input data.</param>
    /// <returns>ActionResult with created Order.</returns>
    /// <exception cref="NotFoundException">Customer with specified Id does not exist.</exception>
    [HttpPost]
    [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseBody), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateAsync([FromBody] OrderDtoInput input)
    {
        var orderId = await _orderService.CreateAsync(input);
        var order = await _orderService.GetByIdAsync(orderId);

        await _orderService.PublishOrderCreatedMessageAsync(orderId);

        return Ok(order);
    }

    /// <summary>
    /// Update Order Status.
    /// </summary>
    /// <param name="orderId">Id of Order to update.</param>
    /// <param name="input">Input object with data to set.</param>
    /// <returns>ActionResult with updated Order.</returns>
    /// <exception cref="NotFoundException">Order with specified Id does not exist.</exception>
    [HttpPatch("{orderId:guid}")]
    [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseBody), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PatchAsync([FromRoute] Guid orderId, [FromBody] OrderDtoInputPatch input)
    {
        await _orderService.UpdateOrderStatusAsync(orderId, input.Status);
        var order = await _orderService.GetByIdAsync(orderId);

        return Ok(order);
    }
}