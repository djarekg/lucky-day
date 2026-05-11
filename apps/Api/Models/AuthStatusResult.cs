namespace Api.Models;

public sealed record AuthStatusResult(bool IsAuthenticated, string? Email, string? Role);
