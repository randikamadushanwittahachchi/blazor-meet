using BasedLibrary.DTOs.Request.Authentication;
using BasedLibrary.DTOs.Response;
using BasedLibrary.DTOs.Response.Authentication;

namespace ServerLibrary.Repositores.Contracts.Authentication;

/// <summary>
/// Defines operations for managing user accounts, including registration, login, and token refresh.
/// </summary>
public interface IUserAccountRepository
{
    /// <summary>
    /// Register a new account.
    /// </summary>
    /// <param name="user"> the user registration data.</param>
    /// <returns>A General response including success or failure.</returns>
    Task<GeneralResponse> CreateAsync(Register user);

    /// <summary>
    /// Authenticates a user and generates authentication tokens.
    /// </summary>
    /// <param name="user">the login request data.</param>
    /// <returns>A login response containing tokens and status information.</returns>
    Task<LoginResponse> SigninAsync(Login user);

    /// <summary>
    /// Refreshs a user's authentication token
    /// </summary>
    /// <param name="refershToken">The refresh token request data.</param>
    /// <returns>A login response containing new token and status information.</returns>
    Task<LoginResponse> RefreshTokenAsync(RefreshToken refershToken);

}
