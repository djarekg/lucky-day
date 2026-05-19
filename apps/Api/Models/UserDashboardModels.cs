namespace LuckyDay.Api.Models;

public record UserDashboardResponseModel(
  string Id,
  string UserId,
  string DashboardWidgetId,
  int Position,
  DashboardWidgetResponseModel Widget);

public record UserDashboardCreateModel(
  string UserId,
  string DashboardWidgetId,
  int Position);

public record UserDashboardUpdateModel(
  string Id,
  int Position);
