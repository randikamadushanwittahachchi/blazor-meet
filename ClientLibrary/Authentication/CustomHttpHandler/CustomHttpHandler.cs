using BasedLibrary.DTOs.Request.Authentication;
using BasedLibrary.DTOs.UserSession;
using ClientLibrary.Constant.BaseUrlConstant;
using ClientLibrary.Constant.HttpClientConstant;
using ClientLibrary.Helper.Constracts;
using ClientLibrary.Helper.Implimentations;
using ClientLibrary.Services.Contracts;
using System.Net;

namespace ClientLibrary.Authentication.CustomHttpHandler;

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

    protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        
        bool loginUrl = request.RequestUri!.AbsoluteUri.Contains(BaseUrlConstant.Login);
        bool registerUrl = request.RequestUri.AbsoluteUri.Contains(BaseUrlConstant.Register);
        bool refreshTokenUrl = request.RequestUri.AbsoluteUri.Contains(BaseUrlConstant.RefreshToken);

        var response = await base.SendAsync(request, cancellationToken);

        // Check login/ register/ refresh-token
        if (loginUrl || registerUrl || refreshTokenUrl) return response;

        // Check Unauthorized client
        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            //Get Token from loal storage
            var stringToken = await _localStorage.GetTokenAsync();
            if (string.IsNullOrWhiteSpace(stringToken)) return response;

            // De-serialized Token
            var deserializedToken = _userSessionSerialization.DeserializationJsonString(stringToken);
            if (deserializedToken is null || string.IsNullOrWhiteSpace(deserializedToken.Token)) return response;

            var token = request.Headers.Authorization!.Parameter;

            // Header Autherization
            if (string.IsNullOrWhiteSpace(token))
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(HttpClientConstant.Bearer, deserializedToken.Token);
                return await base.SendAsync(request, cancellationToken);
            }

            //Get Refresh Token
            var jwtToken = await GetRefreshToken(deserializedToken.RefershToken);
            if (string.IsNullOrWhiteSpace(jwtToken)) return response;

            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(HttpClientConstant.Bearer, jwtToken);
            return await base.SendAsync(request, cancellationToken);

        }


        return await base.SendAsync(request, cancellationToken);
    }

    // Refresh Token 
    private async Task<string> GetRefreshToken(string refreshToken)
    {
        var response = await _userAccountService.RefreshTokenAsync(new RefershToken { Token= refreshToken});
        if (!response.flag) return string.Empty;

        var stringToken = _userSessionSerialization.SerializationModelObject(new UserSession { Token = response.token, RefershToken = response.refreshToken });
        if (string.IsNullOrWhiteSpace(stringToken)) return string.Empty;

        await _localStorage.SetTokenAsync(stringToken);
        return response.token;
    }

}
