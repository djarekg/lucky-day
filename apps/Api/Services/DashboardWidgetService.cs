using LuckyDay.Api.Models;
using LuckyDay.Db.Models;
using LuckyDay.Db.Repositories;

namespace LuckyDay.Api.Services;

public class DashboardWidgetService(IUnitOfWork uow)
{
  public async Task<IEnumerable<DashboardWidgetResponseModel>> GetAllAsync()
  {
    var widgets = await uow.DashboardWidgets.GetAllAsync();
    return widgets.Select(ToResponse);
  }

  private DashboardWidgetResponseModel ToResponse(DashboardWidget widget)
  {
    return new DashboardWidgetResponseModel(
      widget.Id,
      widget.Name,
      widget.Category,
      widget.Type);
  }
}
