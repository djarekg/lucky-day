namespace LuckyDay.Db.Repositories;

public interface IDashboardWidgetRepository
{
  Task<IEnumerable<DashboardWidget>> GetAllAsync();
}
