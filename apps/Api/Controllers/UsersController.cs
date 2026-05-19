using LuckyDay.Api.Models;
using LuckyDay.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace LuckyDay.Api.Controllers;

[ApiController]
[Route("users")]
public class UsersController(UserService userService) : ControllerBase
{
  [HttpGet]
  [ProducesResponseType<IEnumerable<UserResponseModel>>(StatusCodes.Status200OK)]
  public async Task<ActionResult<IEnumerable<UserResponseModel>>> GetAll()
  {
    var users = await userService.GetAllAsync();
    return Ok(users);
  }

  [HttpGet("{email}")]
  [ProducesResponseType<UserResponseModel>(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<ActionResult<UserResponseModel>> GetByEmail(string email)
  {
    var user = await userService.GetByEmailAsync(email);
    if (user is null)
    {
      return NotFound();
    }

    return Ok(user);
  }

  [HttpPost]
  [ProducesResponseType<UserResponseModel>(StatusCodes.Status201Created)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<ActionResult<UserResponseModel>> Create([FromBody] UserCreateModel request)
  {
    if (string.IsNullOrWhiteSpace(request.Email))
    {
      return BadRequest("Email is required.");
    }

    var createdUser = await userService.CreateAsync(request);
    return CreatedAtAction(nameof(GetByEmail), new { id = createdUser.Id }, createdUser);
  }

  [HttpPut("{id}")]
  [ProducesResponseType<UserResponseModel>(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<ActionResult<UserResponseModel>> Update(string id, [FromBody] UserUpdateModel request)
  {
    var updatedUser = await userService.UpdateAsync(id, request);
    if (updatedUser is null)
    {
      return NotFound();
    }

    return Ok(updatedUser);
  }

  [HttpDelete("{id}")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<IActionResult> Delete(string id)
  {
    var deleted = await userService.DeleteAsync(id);
    if (!deleted)
    {
      return NotFound();
    }

    return NoContent();
  }
}
