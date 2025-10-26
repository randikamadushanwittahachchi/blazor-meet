using BasedLibrary.DTOs.CustomUserClaims;
using BasedLibrary.DTOs.UserSession;
using ClientLibrary.Constant.ClientConstant;
using ClientLibrary.Helper.Constracts;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ClientLibrary.Authentication.CustomAuthenticationStateProvider;

/// <summary>
/// Provides the authentication state for current use base on a locally stored jwt token.
/// Handles user login, logout, token deserialization
/// </summary>

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorage _localStorage;
    private readonly ISerialization<UserSession> _userSessionSerialization;

    // Represents  an unauthentication user (no claims)
    private readonly ClaimsPrincipal _anonymous = new(new ClaimsIdentity());

    public CustomAuthenticationStateProvider(ILocalStorage localStorage, ISerialization<UserSession> userSessionSerialization)
    {
        _localStorage = localStorage;
        _userSessionSerialization = userSessionSerialization;
    }

    /// <summary>
    /// Retrieves the current authentication state.
    /// Read the stored jwt token, validates it, and build the a claimsPrincipal.
    /// </summary>
    /// <returns>The user's <see cref="AuthenticationState"/>.</returns>
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        // Try to get the saved token from  local storage
        var stringToken = await _localStorage.GetTokenAsync();

        // No token -> anonymous use
        if (string.IsNullOrWhiteSpace(stringToken)) return new AuthenticationState(_anonymous);

        // Deserilization token object from storage
        var userSession = _userSessionSerialization.DeserializationJsonString(stringToken);
        if (userSession is null || string.IsNullOrWhiteSpace(userSession.Token)) return new AuthenticationState(_anonymous);

        //Decode jwt token
        var userClaim = DecryptToken(userSession.Token);
        if (userClaim is null) return new AuthenticationState(_anonymous);

        //Build the claimsPrincipal
        var claimsPrincipal = SetClaimsPrincipal(userClaim);
        if (claimsPrincipal is null || claimsPrincipal.Identity!.IsAuthenticated) return new AuthenticationState(_anonymous);

        return new AuthenticationState(claimsPrincipal);

    }

    /// <summary>
    /// Update current authentication state after login or logout.
    /// persists token to local storage and notifies blazer of authentication change.
    /// </summary>
    public async Task UpdateAuthenticationState(UserSession userSession)
    {
        ClaimsPrincipal claimsPrincipal;

        if (!string.IsNullOrWhiteSpace(userSession.Token))
        {
            // save the new token to local storage
            var stringToken = _userSessionSerialization.SerializationModelObject(userSession);
            if (string.IsNullOrWhiteSpace(stringToken)) claimsPrincipal = _anonymous;
            await _localStorage.SetTokenAsync(stringToken!);

            // buils the claims from token
            var userClaims = DecryptToken(userSession.Token);
            if (userClaims is null) claimsPrincipal = _anonymous;
            claimsPrincipal = SetClaimsPrincipal(userClaims!);
        }
        else
        {
            // claer token on logout
            await _localStorage.RemoveTokenAsync();

            // set anonymous claims
            claimsPrincipal = _anonymous;

        }
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
    }

    /// <summary>
    /// Builds a <see cref="ClaimsPrincipal"/> based on user claims.
    /// </summary>
    private static ClaimsPrincipal SetClaimsPrincipal(CustomUserClaims claims)
    {
        var identity = new ClaimsIdentity(new List<Claim> 
        {
            new(ClaimTypes.NameIdentifier, claims.Id),
            new(ClaimTypes.Name, claims.Name),
            new(ClaimTypes.Email, claims.Email),
            new(ClaimTypes.Role, claims.Role)
        }, authenticationType: ClientConstant.JwtAuth);

        return new ClaimsPrincipal(identity);
    }

    /// <summary>
    /// Extracts custom claims from a jwt token string.
    /// </summary>
    private static CustomUserClaims DecryptToken(string jwtToken)
    {
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(jwtToken);

        var userId = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        var userName = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value ?? string.Empty;
        var userEmail = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value ?? string.Empty;
        var userRole = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value ?? string.Empty;

        return new CustomUserClaims(userId, userName, userEmail, userRole);

    }
}
