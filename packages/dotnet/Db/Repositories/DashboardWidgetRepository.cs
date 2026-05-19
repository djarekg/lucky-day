namespace LuckyDay.Db.Repositories;

public class DashboardWidgetRepository(LuckyDayDbContext context)
  : Repository<DashboardWidget>(context), IDashboardWidgetRepository
{
  async Task<IEnumerable<DashboardWidget>> IDashboardWidgetRepository.GetAllAsync()
  {
    return await base.GetAllAsync();
  }
}
