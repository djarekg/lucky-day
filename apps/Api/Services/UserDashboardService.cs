using LuckyDay.Api.Models;
using LuckyDay.Db.Models;
using LuckyDay.Db.Repositories;

namespace LuckyDay.Api.Services;

public class UserDashboardService(IUnitOfWork uow)
{
  public async Task<UserDashboardResponseModel> CreateAsync(UserDashboardCreateModel model)
  {
    var dashboard = new UserDashboard
    {
      UserId = model.UserId,
      DashboardWidgetId = model.DashboardWidgetId,
      Position = model.Position
    };
    var createdDashboard = await uow.UserDashboards.CreateAsync(dashboard);
    return ToResponse(createdDashboard);
  }

  public async Task<IEnumerable<UserDashboardResponseModel>> GetByUserIdAsync(string userId)
  {
    var dashboards = await uow.UserDashboards.GetByUserIdAsync(userId);
    return dashboards.Select(ToResponse);
  }

  public async Task<UserDashboardResponseModel?> UpdateAsync(string id, UserDashboardUpdateModel model)
  {
    var dashboard = await uow.UserDashboards.GetByEmailAsync(id);
    if (dashboard is null)
    {
      return null;
    }
    dashboard.Position = model.Position;
    var updatedDashboard = await uow.UserDashboards.UpdateAsync(dashboard);
    return ToResponse(updatedDashboard);
  }

  public async Task DeleteAsync(string id)
  {
    await uow.UserDashboards.DeleteAsync(id);
  }

  private static UserDashboardResponseModel ToResponse(UserDashboard dashboard)
  {
    var widget = new DashboardWidgetResponseModel(
      dashboard.DashboardWidget.Id,
      dashboard.DashboardWidget.Name,
      dashboard.DashboardWidget.Category,
      dashboard.DashboardWidget.Type);

    return new UserDashboardResponseModel(
      dashboard.Id,
      dashboard.UserId,
      dashboard.DashboardWidgetId,
      dashboard.Position,
      widget);
  }
}
