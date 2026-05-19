namespace LuckyDay.Db.Repositories;

public interface IRepository<T> where T : class
{
  Task<T?> GetByEmailAsync(string email);
  Task<IEnumerable<T>> GetAllAsync();
  Task<T> AddAsync(T entity);
  Task<T> UpdateAsync(T entity);
  Task DeleteAsync(string id);
  Task<bool> ExistsAsync(string id);
}
