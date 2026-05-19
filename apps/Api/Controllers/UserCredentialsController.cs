using LuckyDay.Api.Models;
using LuckyDay.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace LuckyDay.Api.Controllers;

[ApiController]
[Route("user-credentials")]
public class UserCredentialsController(UserCredentialService userCredentialService) : ControllerBase
{
  [HttpGet]
  [ProducesResponseType<IEnumerable<UserCredentialResponseModel>>(StatusCodes.Status200OK)]
  public async Task<ActionResult<IEnumerable<UserCredentialResponseModel>>> GetAll()
  {
    var credentials = await userCredentialService.GetAllAsync();
    return Ok(credentials);
  }

  [HttpGet("{id}")]
  [ProducesResponseType<UserCredentialResponseModel>(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<ActionResult<UserCredentialResponseModel>> GetById(string id)
  {
    var credential = await userCredentialService.GetByIdAsync(id);
    if (credential is null)
    {
      return NotFound();
    }

    return Ok(credential);
  }

  [HttpPost]
  [ProducesResponseType<UserCredentialResponseModel>(StatusCodes.Status201Created)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<ActionResult<UserCredentialResponseModel>> Create([FromBody] UserCredentialCreateModel request)
  {
    if (string.IsNullOrWhiteSpace(request.UserId) || string.IsNullOrWhiteSpace(request.Password))
    {
      return BadRequest("UserId and password are required.");
    }

    var createdCredential = await userCredentialService.CreateAsync(request);
    return CreatedAtAction(nameof(GetById), new { id = createdCredential.Id }, createdCredential);
  }

  [HttpPut("{id}")]
  [ProducesResponseType<UserCredentialResponseModel>(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<ActionResult<UserCredentialResponseModel>> Update(string id, [FromBody] UserCredentialUpdateModel request)
  {
    var updatedCredential = await userCredentialService.UpdateAsync(id, request);
    if (updatedCredential is null)
    {
      return NotFound();
    }

    return Ok(updatedCredential);
  }

  [HttpDelete("{id}")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<IActionResult> Delete(string id)
  {
    var deleted = await userCredentialService.DeleteAsync(id);
    if (!deleted)
    {
      return NotFound();
    }

    return NoContent();
  }
}
