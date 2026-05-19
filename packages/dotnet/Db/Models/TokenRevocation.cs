namespace LuckyDay.Db.Models;

/// <summary>
/// Represents a revoked JWT token that should no longer be accepted by the API.
/// Expired entries are cleaned up automatically based on the token's expiration time.
/// </summary>
public class TokenRevocation
{
  /// <summary>
  /// The SHA256 hash of the revoked JWT token.
  /// </summary>
  public string Id { get; set; } = null!;

  /// <summary>
  /// The user's email address for audit purposes.
  /// </summary>
  public string Email { get; set; } = null!;

  /// <summary>
  /// The UTC date/time when the token expires and this revocation entry should be cleaned up.
  /// </summary>
  public DateTime ExpiresAtUtc { get; set; }

  /// <summary>
  /// The UTC date/time when the token was revoked.
  /// </summary>
  public DateTime RevokedAtUtc { get; set; } = DateTime.UtcNow;
}
