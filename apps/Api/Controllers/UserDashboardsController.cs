using LuckyDay.Api.Models;
using LuckyDay.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace LuckyDay.Api.Controllers;

[ApiController]
[Route("user-dashboards")]
public class UserDashboardsController(UserDashboardService userDashboardService) : ControllerBase
{
  [HttpGet("{userId}")]
  [ProducesResponseType<IEnumerable<UserDashboardResponseModel>>(StatusCodes.Status200OK)]
  public async Task<ActionResult<IEnumerable<UserDashboardResponseModel>>> GetByUserId(string userId)
  {
    var dashboards = await userDashboardService.GetByUserIdAsync(userId);
    return Ok(dashboards);
  }

  [HttpPost]
  [ProducesResponseType<UserDashboardResponseModel>(StatusCodes.Status201Created)]
  public async Task<ActionResult<UserDashboardResponseModel>> Create(UserDashboardCreateModel model)
  {
    var createdDashboard = await userDashboardService.CreateAsync(model);
    return CreatedAtAction(nameof(GetByUserId), new { userId = createdDashboard.UserId }, createdDashboard);
  }

  [HttpPut("{id}")]
  [ProducesResponseType<UserDashboardResponseModel>(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<ActionResult<UserDashboardResponseModel>> Update(string id, UserDashboardUpdateModel model)
  {
    var updatedDashboard = await userDashboardService.UpdateAsync(id, model);
    if (updatedDashboard is null)
    {
      return NotFound();
    }

    return Ok(updatedDashboard);
  }

  [HttpDelete("{id}")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  public async Task<IActionResult> Delete(string id)
  {
    await userDashboardService.DeleteAsync(id);
    return NoContent();
  }
}
