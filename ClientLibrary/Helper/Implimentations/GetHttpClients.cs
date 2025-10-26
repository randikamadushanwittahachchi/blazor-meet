using BasedLibrary.DTOs.UserSession;
using ClientLibrary.Constant.ClientConstant;
using ClientLibrary.Helper.Constracts;
using System.Net.Http.Headers;

namespace ClientLibrary.Helper.Implimentations;

public class GetHttpClients : IGetHttpClients
{
    private readonly ILocalStorage _localStorage;
    private readonly ISerialization<UserSession> _userSessionSerialization;
    private readonly IHttpClientFactory _httpClientFactory;
    public GetHttpClients(ILocalStorage localStorage, ISerialization<UserSession> userSessionSerialization, IHttpClientFactory httpClientFactory)
    {
        _localStorage = localStorage;
        _userSessionSerialization = userSessionSerialization;
        _httpClientFactory = httpClientFactory;

    }
    public HttpClient GetPublicHttpClient()
    {
        HttpClient client = _httpClientFactory.CreateClient(ClientConstant.SystemApiClientName);
        client.DefaultRequestHeaders.Remove(ClientConstant.AuthorizationHeader);
        return client;
    }
    public async Task<HttpClient?> GetPrivateHttpClientAsync()
    {
        HttpClient client = _httpClientFactory.CreateClient(ClientConstant.SystemApiClientName);

        var stringToken = await _localStorage.GetTokenAsync();
        if (string.IsNullOrEmpty(stringToken)) return client;

        var deserializationToken = _userSessionSerialization.DeserializationJsonString(stringToken);
        if (deserializationToken is null) return client;

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(ClientConstant.Bearer, deserializationToken.Token);
        return client;
    }

}
