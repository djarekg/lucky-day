using LuckyDay.Api.Models;
using LuckyDay.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace LuckyDay.Api.Controllers;

[ApiController]
[Route("dashboard")]
public class DashboardController(DashboardService dashboardService) : ControllerBase
{
  [HttpGet("top-user-sales")]
  [ProducesResponseType<IEnumerable<UserSalesTotalResponseModel>>(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<ActionResult<IEnumerable<UserSalesTotalResponseModel>>> GetTopUserSalesByYear([FromQuery] int year, [FromQuery] int take)
  {
    if (year < 1 || take < 1)
    {
      return BadRequest("year and take must be greater than zero.");
    }

    var results = await dashboardService.GetTopUserSalesByYearAsync(year, take);
    return Ok(results);
  }
}
