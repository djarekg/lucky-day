namespace LuckyDay.Api.Configuration;

public interface IJwtConfigurationService
{
  string Issuer { get; }

  string Audience { get; }

  string Key { get; }

  int TokenExpirationMinutes { get; }
}
