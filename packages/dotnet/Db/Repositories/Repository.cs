namespace LuckyDay.Db.Repositories;

public class Repository<T>(LuckyDayDbContext context) : IRepository<T> where T : class
{
  protected readonly LuckyDayDbContext _context = context;
  protected readonly DbSet<T> _dbSet = context.Set<T>();

  public async Task<T?> GetByEmailAsync(string email)
  {
    return await _dbSet.FindAsync(email);
  }

  public async Task<IEnumerable<T>> GetAllAsync()
  {
    return await _dbSet.ToListAsync();
  }

  public async Task<T> AddAsync(T entity)
  {
    _dbSet.Add(entity);
    await _context.SaveChangesAsync();
    return entity;
  }

  public async Task<T> UpdateAsync(T entity)
  {
    _dbSet.Update(entity);
    await _context.SaveChangesAsync();
    return entity;
  }

  public async Task DeleteAsync(string id)
  {
    var entity = await GetByEmailAsync(id);
    if (entity is not null)
    {
      _dbSet.Remove(entity);
      await _context.SaveChangesAsync();
    }
  }

  public async Task<bool> ExistsAsync(string id)
  {
    return await _dbSet.FindAsync(id) is not null;
  }
}
