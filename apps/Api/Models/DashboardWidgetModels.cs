using LuckyDay.Db.Enums;

namespace LuckyDay.Api.Models;

public record DashboardWidgetResponseModel(
  string Id,
  string Name,
  DashboardWidgetCategory Category,
  DashboardWidgetType Type);
