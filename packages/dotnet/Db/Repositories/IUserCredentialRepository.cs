namespace LuckyDay.Db.Repositories;

public interface IUserCredentialRepository : IRepository<UserCredential>
{
  Task<UserCredential?> GetByUserIdAsync(string userId);
}
