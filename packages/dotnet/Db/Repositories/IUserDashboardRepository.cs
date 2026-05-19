namespace LuckyDay.Db.Repositories;

public interface IUserDashboardRepository
{
  Task<UserDashboard?> GetByEmailAsync(string id);
  Task<IEnumerable<UserDashboard>> GetByUserIdAsync(string id);
  Task<UserDashboard> CreateAsync(UserDashboard entity);
  Task<UserDashboard> UpdateAsync(UserDashboard entity);
  Task DeleteAsync(string id);
}
