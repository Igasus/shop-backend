using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Contracts.Aggregates;
using Shop.Application.Dto;

namespace Shop.Api.Controllers;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Route("api/customers")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerAggregate _customerAggregate;

    public CustomerController(ICustomerAggregate customerAggregate)
    {
        _customerAggregate = customerAggregate;
    }

    /// <summary>
    /// Get all Customers.
    /// </summary>
    /// <returns>ActionResult with List of Customers.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IList<CustomerDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync()
    {
        var customers = await _customerAggregate.GetAllAsync();

        return Ok(customers);
    }

    /// <summary>
    /// Create Customer.
    /// </summary>
    /// <param name="input">Customer Input data.</param>
    /// <returns>ActionResult with created Customer.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateAsync([FromBody] CustomerDtoInput input)
    {
        var customerId = await _customerAggregate.CreateAsync(input);
        var customer = await _customerAggregate.GetByIdAsync(customerId);

        return Ok(customer);
    }
}