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
[Route("api/customers")]
public class CustomerController : ControllerBase
{
    /// <summary>
    /// Get all Customers.
    /// </summary>
    /// <returns>ActionResult with List of Customers.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IList<CustomerDto>), StatusCodes.Status200OK)]
    public Task<IActionResult> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Create Customer.
    /// </summary>
    /// <param name="input">Customer Input data.</param>
    /// <returns>ActionResult with created Customer.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public Task<IActionResult> CreateAsync([FromBody] CustomerDtoInput input)
    {
        throw new NotImplementedException();
    }
}