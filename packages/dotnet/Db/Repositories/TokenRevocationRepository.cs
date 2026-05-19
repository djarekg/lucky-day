namespace LuckyDay.Db.Repositories;

/// <summary>
/// Repository for managing revoked JWT tokens.
/// </summary>
public class TokenRevocationRepository(LuckyDayDbContext context)
  : Repository<TokenRevocation>(context), ITokenRevocationRepository
{
  /// <summary>
  /// Revokes a JWT token by storing its hash with expiration time.
  /// </summary>
  /// <param name="tokenHash">SHA256 hash of the JWT token.</param>
  /// <param name="email">User's email address for audit.</param>
  /// <param name="expiresAtUtc">When the token naturally expires.</param>
  /// <returns>The created TokenRevocation entity.</returns>
  public async Task<TokenRevocation> RevokeTokenAsync(string tokenHash, string email, DateTime expiresAtUtc)
  {
    return await AddAsync(new TokenRevocation
    {
      Id = tokenHash,
      Email = email,
      ExpiresAtUtc = expiresAtUtc,
    });
  }

  /// <summary>
  /// Checks if a token has been revoked.
  /// </summary>
  /// <param name="tokenHash">SHA256 hash of the JWT token.</param>
  /// <returns>True if the token is revoked; false otherwise.</returns>
  public async Task<bool> IsTokenRevokedAsync(string tokenHash)
  {
    return await _dbSet.AnyAsync(tr => tr.Id == tokenHash);
  }

  /// <summary>
  /// Cleans up expired revocation entries from the database.
  /// Should be called periodically to maintain performance.
  /// </summary>
  /// <returns>The number of expired entries removed.</returns>
  public async Task<int> CleanupExpiredEntriesAsync()
  {
    var expiredCount = await _dbSet
        .Where(tr => tr.ExpiresAtUtc < DateTime.UtcNow)
        .ExecuteDeleteAsync();

    return expiredCount;
  }
}
