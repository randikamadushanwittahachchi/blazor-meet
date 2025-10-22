using BasedLibrary.DTOs.Request.Authentication;
using BasedLibrary.DTOs.Response;
using BasedLibrary.DTOs.Response.Authentication;
using BasedLibrary.DTOs.UserSession;
using ClientLibrary.Constant.BaseUrlConstant;
using ClientLibrary.Helper.Constracts;
using ClientLibrary.Services.Contracts;
using System.Net.Http.Json;

namespace ClientLibrary.Services.Implimentations;

public class UserAccountService : IUserAccountService
{
    private readonly IGetHttpClients _getHttpClients;

    public UserAccountService(IGetHttpClients getHttpClients)
    {
        _getHttpClients = getHttpClients;
    }

    // Register User
    public async Task<GeneralResponse> RegisterAsync(Register user)
    {
        var httpClient = _getHttpClients.GetPublicHttpClient();
        var response = await httpClient.PostAsJsonAsync(BaseUrlConstant.Register, user);
        var result = await response.Content.ReadFromJsonAsync<GeneralResponse>();
        if (!response.IsSuccessStatusCode) return GeneralResponse.Failure("Registration failed. Please try again later.");
        return result!;

    }
    public async Task<LoginResponse> LoginAsync(Login user)
    {
        var httpClient = _getHttpClients.GetPublicHttpClient();
        var response = await httpClient.PostAsJsonAsync(BaseUrlConstant.Login, user);
        var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
        if (!response.IsSuccessStatusCode) return LoginResponse.Failure("Login failed. Please try again later.");
        return result!;
    }

    public async Task<LoginResponse> RefreshTokenAsync(RefershToken token)
    {
        var httpClient = _getHttpClients.GetPublicHttpClient();
        var response = await httpClient.PostAsJsonAsync(BaseUrlConstant.RefreshToken, token);
        var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
        if (!response.IsSuccessStatusCode) return LoginResponse.Failure("Refresh login failed. Please try again later");
        return result!;
    }

}
