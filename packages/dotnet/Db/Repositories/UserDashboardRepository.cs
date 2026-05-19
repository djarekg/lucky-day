namespace LuckyDay.Db.Repositories;

public class UserDashboardRepository(LuckyDayDbContext context)
  : Repository<UserDashboard>(context), IUserDashboardRepository
{
  public async Task<IEnumerable<UserDashboard>> GetByUserIdAsync(string userId)
  {
    return await _dbSet
      .Include(x => x.DashboardWidget)
      .Where(x => x.UserId == userId)
      .OrderBy(x => x.Position)
      .ToListAsync();
  }

  async Task<UserDashboard> IUserDashboardRepository.CreateAsync(UserDashboard entity)
  {
    return await AddAsync(entity);
  }

  async Task<UserDashboard> IUserDashboardRepository.UpdateAsync(UserDashboard entity)
  {
    return await base.UpdateAsync(entity);
  }

  async Task IUserDashboardRepository.DeleteAsync(string id)
  {
    await base.DeleteAsync(id);
  }
}
