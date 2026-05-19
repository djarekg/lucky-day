namespace LuckyDay.Db.Repositories;

public interface ITokenRevocationRepository : IRepository<TokenRevocation>
{
  Task<TokenRevocation> RevokeTokenAsync(string tokenHash, string email, DateTime expiresAtUtc);
  Task<bool> IsTokenRevokedAsync(string tokenHash);
  Task<int> CleanupExpiredEntriesAsync();
}
