namespace LuckyDay.Api.Configuration;

public sealed class JwtConfigurationService(IConfiguration configuration) : IJwtConfigurationService
{
  private const int DefaultTokenExpirationMinutes = 60;

  public string Issuer => GetRequiredString("Jwt:Issuer");

  public string Audience => GetRequiredString("Jwt:Audience");

  public string Key => GetRequiredString("Jwt:Key");

  public int TokenExpirationMinutes => GetPositiveIntOrDefault(
    "Jwt:TokenExpirationMinutes",
    DefaultTokenExpirationMinutes);

  private string GetRequiredString(string configurationKey)
    => configuration[configurationKey]
      ?? throw new InvalidOperationException($"{configurationKey} is required.");

  private int GetPositiveIntOrDefault(string configurationKey, int defaultValue)
    => int.TryParse(configuration[configurationKey], out var value) && value > 0
      ? value
      : defaultValue;
}
