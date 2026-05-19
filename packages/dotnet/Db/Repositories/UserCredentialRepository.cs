namespace LuckyDay.Db.Repositories;

public class UserCredentialRepository(LuckyDayDbContext context) : Repository<UserCredential>(context), IUserCredentialRepository
{
  public async Task<UserCredential?> GetByUserIdAsync(string userId)
  {
    return await _dbSet.FirstOrDefaultAsync(uc => uc.UserId == userId);
  }
}
