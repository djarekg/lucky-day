namespace LuckyDay.Api.Models;

public sealed record AuthTokenResult(string AccessToken, DateTime ExpiresAtUtc, string TokenType = "Bearer");
