using BasedLibrary.DTOs.Request.Authentication;
using BasedLibrary.DTOs.UserSession;
using ClientLibrary.Constant.BaseUrlConstant;
using ClientLibrary.Constant.ClientConstant;
using ClientLibrary.Helper.Constracts;
using ClientLibrary.Helper.Implimentations;
using ClientLibrary.Services.Contracts;
using System.Net;
using System.Net.Http.Headers;

namespace ClientLibrary.Authentication.CustomHttpHandler;

/// <summary>
/// A custom DelegatingHandler that automatically attaches JWT tokens to outgoing HTTP requests,
/// and handles token refresh when a request is unauthorized (401).
/// </summary>
public class CustomHttpHandler : DelegatingHandler
{
    private readonly ILocalStorage _localStorage;
    private readonly ISerialization<UserSession> _userSessionSerialization;
    private readonly IUserAccountService _userAccountService;

    public CustomHttpHandler(ILocalStorage localStorage, ISerialization<UserSession> userSessionSerialization, IUserAccountService userAccountService)
    {
        _localStorage = localStorage;
        _userSessionSerialization = userSessionSerialization;
        _userAccountService = userAccountService;
    }

    /// <summary>
    /// Intercepts HTTP requests to attach authentication tokens and handle automatic token refresh.
    /// </summary>

    
    protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {

        // Bypass authentication for login, registration, and refresh token endpoints
        bool loginUrl = request.RequestUri!.AbsoluteUri.Contains(BaseUrlConstant.Login);
        bool registerUrl = request.RequestUri.AbsoluteUri.Contains(BaseUrlConstant.Register);
        bool refreshTokenUrl = request.RequestUri.AbsoluteUri.Contains(BaseUrlConstant.RefreshToken);


        if (loginUrl || registerUrl || refreshTokenUrl) return await base.SendAsync(request, cancellationToken);

        // Send the initial request
        var response = await base.SendAsync(request, cancellationToken);

        // If the response indicates an unauthorized request (401)
        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            // Retrieve the serialized user session from local storage
            var stringToken = await _localStorage.GetTokenAsync();
            if (string.IsNullOrWhiteSpace(stringToken)) return response;

            // Deserialize the stored session into a UserSession object
            var deserializedToken = _userSessionSerialization.DeserializationJsonString(stringToken);
            if (deserializedToken is null || string.IsNullOrWhiteSpace(deserializedToken.Token)) return response;

            // If the original request has no Authorization header, add the stored JWT
            if (string.IsNullOrWhiteSpace(request.Headers.Authorization!.Parameter))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue(ClientConstant.Bearer, deserializedToken.Token);
                return await base.SendAsync(request, cancellationToken);
            }

            // Attempt to refresh the token using the refresh token
            var jwtToken = await GetRefreshToken(deserializedToken.RefershToken);
            if (string.IsNullOrWhiteSpace(jwtToken)) return response;

            // Attach the new token and resend the request
            request.Headers.Authorization = new AuthenticationHeaderValue(ClientConstant.Bearer, jwtToken);
            return await base.SendAsync(request, cancellationToken);
        }
        // Return the original response if no re-authentication was needed
        return await base.SendAsync(request, cancellationToken);
    }

    /// <summary>
    /// Attempts to refresh the JWT using the provided refresh token.
    /// Updates the local storage if the refresh succeeds.
    /// </summary>
    private async Task<string> GetRefreshToken(string refreshToken)
    {
        // Request a new token from the user account service
        var response = await _userAccountService.RefreshTokenAsync(new RefershToken { Token= refreshToken});
        if (!response.flag) return string.Empty;

        // Serialize the new session data
        var stringToken = _userSessionSerialization.SerializationModelObject(new UserSession { Token = response.token, RefershToken = response.refreshToken });
        if (string.IsNullOrWhiteSpace(stringToken)) return string.Empty;

        // Save the new token data to local storage
        await _localStorage.SetTokenAsync(stringToken);

        return response.token;
    }

}
