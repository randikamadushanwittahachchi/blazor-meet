using System.Security.Cryptography;

namespace ServerLibrary.Authentication;

/// <summary>
/// Provides functionality to generate secure refresh token for authentication purposes.
/// </summary>
public class RefreshTokenService
{
    /// <summary>
    /// Generates a cryptographically secure random refresh token.
    /// </summary>
    /// <returns>A 32-byte base64-encoded string suitable for use as a refresh token.</returns>
    public static string GetToken() => Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
}
