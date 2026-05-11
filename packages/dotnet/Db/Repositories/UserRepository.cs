namespace Db.Repositories;

public interface IUserRepository : IRepository<User>
{
  Task<User?> GetByEmailAsync(string email);
  Task<User?> GetUserWithCredentialAsync(string id);
}

public class UserRepository(LuckyDayDbContext context) : Repository<User>(context), IUserRepository
{
  public async Task<User?> GetByEmailAsync(string email)
  {
    return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
  }

  public async Task<User?> GetUserWithCredentialAsync(string id)
  {
    return await _dbSet.Include(u => u.UserCredential).FirstOrDefaultAsync(u => u.Id == id);
  }
}
