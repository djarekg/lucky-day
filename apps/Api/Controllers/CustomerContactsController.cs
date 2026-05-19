using LuckyDay.Api.Models;
using LuckyDay.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace LuckyDay.Api.Controllers;

[ApiController]
[Route("customer-contacts")]
public class CustomerContactsController(CustomerContactService customerContactService) : ControllerBase
{
  [HttpGet]
  [ProducesResponseType<IEnumerable<CustomerContactResponseModel>>(StatusCodes.Status200OK)]
  public async Task<ActionResult<IEnumerable<CustomerContactResponseModel>>> GetAll()
  {
    var contacts = await customerContactService.GetAllAsync();
    return Ok(contacts);
  }

  [HttpGet("{id}")]
  [ProducesResponseType<CustomerContactResponseModel>(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<ActionResult<CustomerContactResponseModel>> GetById(string id)
  {
    var contact = await customerContactService.GetByIdAsync(id);
    if (contact is null)
    {
      return NotFound();
    }

    return Ok(contact);
  }

  [HttpPost]
  [ProducesResponseType<CustomerContactResponseModel>(StatusCodes.Status201Created)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<ActionResult<CustomerContactResponseModel>> Create([FromBody] CustomerContactCreateModel request)
  {
    if (string.IsNullOrWhiteSpace(request.CustomerId) || string.IsNullOrWhiteSpace(request.Email))
    {
      return BadRequest("CustomerId and email are required.");
    }

    var createdContact = await customerContactService.CreateAsync(request);
    return CreatedAtAction(nameof(GetById), new { id = createdContact.Id }, createdContact);
  }

  [HttpPut("{id}")]
  [ProducesResponseType<CustomerContactResponseModel>(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<ActionResult<CustomerContactResponseModel>> Update(string id, [FromBody] CustomerContactUpdateModel request)
  {
    var updatedContact = await customerContactService.UpdateAsync(id, request);
    if (updatedContact is null)
    {
      return NotFound();
    }

    return Ok(updatedContact);
  }

  [HttpDelete("{id}")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<IActionResult> Delete(string id)
  {
    var deleted = await customerContactService.DeleteAsync(id);
    if (!deleted)
    {
      return NotFound();
    }

    return NoContent();
  }
}
