using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Api.Configuration;

public static class AuthenticationConfigurationExtensions
{
  public static IServiceCollection AddJwtAuthenticationConfiguration(
      this IServiceCollection services,
      IConfiguration configuration)
  {
    var jwtIssuer = configuration["Jwt:Issuer"]
        ?? throw new InvalidOperationException("Jwt:Issuer is required.");
    var jwtAudience = configuration["Jwt:Audience"]
        ?? throw new InvalidOperationException("Jwt:Audience is required.");
    var jwtKey = configuration["Jwt:Key"]
        ?? throw new InvalidOperationException("Jwt:Key is required.");

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
