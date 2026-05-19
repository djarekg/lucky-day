using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LuckyDay.Api.Configuration;

public static class AuthenticationConfigurationExtensions
{
  public static IServiceCollection AddJwtAuthenticationConfiguration(
      this IServiceCollection services,
      IJwtConfigurationService jwtConfiguration)
  {
    var jwtIssuer = jwtConfiguration.Issuer;
    var jwtAudience = jwtConfiguration.Audience;
    var jwtKey = jwtConfiguration.Key;

    var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

    services
        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
          options.TokenValidationParameters = new TokenValidationParameters
          {
            ValidateIssuer = true,
            ValidIssuer = jwtIssuer,
            ValidateAudience = true,
            ValidAudience = jwtAudience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = signingKey,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromMinutes(1)
          };
        });

    services.AddAuthorization();

    return services;
  }
}
