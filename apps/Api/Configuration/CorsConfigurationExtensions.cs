namespace LuckyDay.Api.Configuration;

public static class CorsConfigurationExtensions
{
  public static IServiceCollection AddCorsConfiguration(
      this IServiceCollection services,
      IConfiguration configuration)
  {
    var allowedOrigins = configuration.GetSection("Cors:AllowedOrigins").Get<string[]>()
        ?? ["http://localhost:3000"];

    services.AddCors(options =>
    {
      options.AddPolicy("WebClient", policy =>
      {
        policy.WithOrigins(allowedOrigins)
          .AllowAnyHeader()
          .AllowAnyMethod()
          .AllowCredentials();
      });
    });

    return services;
  }
}
