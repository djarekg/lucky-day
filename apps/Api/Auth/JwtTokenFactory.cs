using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace LuckyDay.Api.Auth;

internal static class JwtTokenFactory
{
  /// <summary>
  /// Generates a signed JWT access token for the specified user identity and role.
  /// </summary>
  /// <param name="configuration">The application configuration containing JWT settings.</param>
  /// <param name="email">The user email to include in token claims.</param>
  /// <param name="role">The role claim value to include in the token.</param>
  /// <param name="expiresAtUtc">The UTC expiration timestamp for the token.</param>
  /// <returns>The serialized JWT bearer token string.</returns>
  public static string GenerateJwtToken(IConfiguration configuration, string email, string role, DateTime expiresAtUtc)
  {
    var jwtIssuer = configuration["Jwt:Issuer"]
      ?? throw new InvalidOperationException("Jwt:Issuer is required.");
    var jwtAudience = configuration["Jwt:Audience"]
      ?? throw new InvalidOperationException("Jwt:Audience is required.");
    var jwtKey = configuration["Jwt:Key"]
      ?? throw new InvalidOperationException("Jwt:Key is required.");

    var claims = new List<Claim>
    {
      new(JwtRegisteredClaimNames.Sub, email),
      new(JwtRegisteredClaimNames.Email, email),
      new(ClaimTypes.Role, role)
    };

    var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
    var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
    var tokenDescriptor = new JwtSecurityToken(
      issuer: jwtIssuer,
      audience: jwtAudience,
      claims: claims,
      expires: expiresAtUtc,
      signingCredentials: credentials);

    return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
  }

  /// <summary>
  /// Reads token expiration minutes from configuration and falls back to a default when unset or invalid.
  /// </summary>
  /// <param name="configuration">The application configuration containing JWT settings.</param>
  /// <param name="defaultMinutes">The fallback expiration in minutes.</param>
  /// <returns>A positive expiration value in minutes.</returns>
  public static int GetTokenExpirationMinutes(IConfiguration configuration, int defaultMinutes)
  {
    if (int.TryParse(configuration["Jwt:TokenExpirationMinutes"], out var minutes) && minutes > 0)
    {
      return minutes;
    }

    return defaultMinutes;
  }
}
