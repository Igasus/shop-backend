using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Contracts.Services;
using Shop.Application.Dto;

namespace Shop.Api.Controllers;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Route("api/customers")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    /// <summary>
    /// Get all Customers.
    /// </summary>
    /// <returns>ActionResult with List of Customers.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IList<CustomerDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync()
    {
        var customers = await _customerService.GetAllAsync();

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
        var customerId = await _customerService.CreateAsync(input);
        var customer = await _customerService.GetByIdAsync(customerId);

        return Ok(customer);
    }
}