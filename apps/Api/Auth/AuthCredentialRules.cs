namespace Api.Auth;

internal static class AuthCredentialRules
{
  /// <summary>
  /// Validates whether the provided sign-in inputs meet minimum credential requirements.
  /// </summary>
  /// <param name="email">The email value supplied by the caller.</param>
  /// <param name="password">The password value supplied by the caller.</param>
  /// <returns><see langword="true"/> when both values are present and the password meets minimum length; otherwise <see langword="false"/>.</returns>
  public static bool IsValidCredential(string email, string password)
  {
    return !string.IsNullOrWhiteSpace(email)
      && !string.IsNullOrWhiteSpace(password)
      && password.Length >= 6;
  }
}
