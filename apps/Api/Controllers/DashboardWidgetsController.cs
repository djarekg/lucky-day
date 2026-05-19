using LuckyDay.Api.Models;
using LuckyDay.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace LuckyDay.Api.Controllers;

[ApiController]
[Route("dashboard-widgets")]
public class DashboardWidgetsController(DashboardWidgetService dashboardWidgetService) : ControllerBase
{
  [HttpGet]
  [ProducesResponseType<IEnumerable<DashboardWidgetResponseModel>>(StatusCodes.Status200OK)]
  public async Task<ActionResult<IEnumerable<DashboardWidgetResponseModel>>> GetAll()
  {
    var widgets = await dashboardWidgetService.GetAllAsync();
    return Ok(widgets);
  }
}
