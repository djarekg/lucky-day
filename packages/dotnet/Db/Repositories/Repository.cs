namespace Db.Repositories;

public class Repository<T>(LuckyDayDbContext context) : IRepository<T> where T : class
{
  protected readonly LuckyDayDbContext _context = context;
  protected readonly DbSet<T> _dbSet = context.Set<T>();

  public async Task<T?> GetByIdAsync(string id)
  {
    return await _dbSet.FindAsync(id);
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
    var entity = await GetByIdAsync(id);
    if (entity != null)
    {
      _dbSet.Remove(entity);
      await _context.SaveChangesAsync();
    }
  }

  public async Task<bool> ExistsAsync(string id)
  {
    return await _dbSet.FindAsync(id) != null;
  }
}
