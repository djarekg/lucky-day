using LuckyDay.Api.Models;
using LuckyDay.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace LuckyDay.Api.Controllers;

[ApiController]
[Route("states")]
public class StatesController(StateService stateService) : ControllerBase
{
  [HttpGet]
  [ProducesResponseType<IEnumerable<StateResponseModel>>(StatusCodes.Status200OK)]
  public async Task<ActionResult<IEnumerable<StateResponseModel>>> GetAll()
  {
    var states = await stateService.GetAllAsync();
    return Ok(states);
  }

  [HttpGet("{id}")]
  [ProducesResponseType<StateResponseModel>(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<ActionResult<StateResponseModel>> GetById(string id)
  {
    var state = await stateService.GetByIdAsync(id);
    if (state is null)
    {
      return NotFound();
    }

    return Ok(state);
  }

  [HttpPost]
  [ProducesResponseType<StateResponseModel>(StatusCodes.Status201Created)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<ActionResult<StateResponseModel>> Create([FromBody] StateCreateModel request)
  {
    if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Code))
    {
      return BadRequest("Name and code are required.");
    }

    var createdState = await stateService.CreateAsync(request);
    return CreatedAtAction(nameof(GetById), new { id = createdState.Id }, createdState);
  }

  [HttpPut("{id}")]
  [ProducesResponseType<StateResponseModel>(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<ActionResult<StateResponseModel>> Update(string id, [FromBody] StateUpdateModel request)
  {
    var updatedState = await stateService.UpdateAsync(id, request);
    if (updatedState is null)
    {
      return NotFound();
    }

    return Ok(updatedState);
  }

  [HttpDelete("{id}")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<IActionResult> Delete(string id)
  {
    var deleted = await stateService.DeleteAsync(id);
    if (!deleted)
    {
      return NotFound();
    }

    return NoContent();
  }
}
