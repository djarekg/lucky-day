using LuckyDay.Api.Auth;
using LuckyDay.Api.Models;

namespace LuckyDay.Api.Services;

public class AuthService(IConfiguration configuration)
{
  private const int DefaultTokenExpirationMinutes = 1440;

  /// <summary>
  /// Validates the provided credentials and returns a signed JWT access token when valid.
  /// </summary>
  /// <param name="email">The user email used as the token subject.</param>
  /// <param name="password">The user password to validate.</param>
  /// <returns>
  /// A token result containing the access token and its expiration timestamp, or <see langword="null"/> when credentials are invalid.
  /// </returns>
  public AuthTokenResult? Signin(string email, string password)
  {
    if (!AuthCredentialRules.IsValidCredential(email, password))
    {
      return null;
    }

    var role = AuthRoleResolver.ResolveRole(email);
    var expiresAtUtc = DateTime.UtcNow.AddMinutes(
      JwtTokenFactory.GetTokenExpirationMinutes(configuration, DefaultTokenExpirationMinutes));
    var token = JwtTokenFactory.GenerateJwtToken(configuration, email, role, expiresAtUtc);

    return new AuthTokenResult(token, expiresAtUtc);
  }
}

