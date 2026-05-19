namespace LuckyDay.Api.Auth;

internal static class AuthRoleResolver
{
  /// <summary>
  /// Resolves the application role for the supplied email address.
  /// </summary>
  /// <param name="email">The email address to evaluate.</param>
  /// <returns>The resolved role name.</returns>
  public static string ResolveRole(string email)
  {
    return email.EndsWith("@admin.local", StringComparison.OrdinalIgnoreCase)
      ? "Admin"
      : "User";
  }
}
