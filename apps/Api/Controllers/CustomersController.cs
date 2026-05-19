using LuckyDay.Api.Models;
using LuckyDay.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace LuckyDay.Api.Controllers;

[ApiController]
[Route("customers")]
public class CustomersController(CustomerService customerService) : ControllerBase
{
  [HttpGet]
  [ProducesResponseType<IEnumerable<CustomerResponseModel>>(StatusCodes.Status200OK)]
  public async Task<ActionResult<IEnumerable<CustomerResponseModel>>> GetAll()
  {
    var customers = await customerService.GetAllAsync();
    return Ok(customers);
  }

  [HttpGet("{id}")]
  [ProducesResponseType<CustomerResponseModel>(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<ActionResult<CustomerResponseModel>> GetById(string id)
  {
    var customer = await customerService.GetByIdAsync(id);
    if (customer is null)
    {
      return NotFound();
    }

    return Ok(customer);
  }

  [HttpPost]
  [ProducesResponseType<CustomerResponseModel>(StatusCodes.Status201Created)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<ActionResult<CustomerResponseModel>> Create([FromBody] CustomerCreateModel request)
  {
    if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.StreetAddress) ||
        string.IsNullOrWhiteSpace(request.City) || string.IsNullOrWhiteSpace(request.StateId) ||
        string.IsNullOrWhiteSpace(request.Zip) || string.IsNullOrWhiteSpace(request.Phone))
    {
      return BadRequest("Name, address, city, state, zip, and phone are required.");
    }

    var createdCustomer = await customerService.CreateAsync(request);
    return CreatedAtAction(nameof(GetById), new { id = createdCustomer.Id }, createdCustomer);
  }

  [HttpPut("{id}")]
  [ProducesResponseType<CustomerResponseModel>(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<ActionResult<CustomerResponseModel>> Update(string id, [FromBody] CustomerUpdateModel request)
  {
    var updatedCustomer = await customerService.UpdateAsync(id, request);
    if (updatedCustomer is null)
    {
      return NotFound();
    }

    return Ok(updatedCustomer);
  }

  [HttpDelete("{id}")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<IActionResult> Delete(string id)
  {
    var deleted = await customerService.DeleteAsync(id);
    if (!deleted)
    {
      return NotFound();
    }

    return NoContent();
  }
}
