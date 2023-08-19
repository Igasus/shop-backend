using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Dto;

namespace Shop.Api.Controllers;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Route("api/orders")]
public class OrderController : ControllerBase
{
    /// <summary>
    /// Get all Orders.
    /// </summary>
    /// <returns>ActionResult with List of Orders.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IList<OrderDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Get Order by Id.
    /// </summary>
    /// <param name="orderId">Id of Order.</param>
    /// <returns>ActionResult with Order.</returns>
    [HttpGet("{orderId:guid}")]
    [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
    public Task<IActionResult> GetByIdAsync([FromRoute] Guid orderId)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Create Order.
    /// </summary>
    /// <param name="input">Order Input data.</param>
    /// <returns>ActionResult with created Order.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<IActionResult> CreateAsync([FromBody] OrderDtoInput input)
    {
        throw new NotImplementedException();
    }
}