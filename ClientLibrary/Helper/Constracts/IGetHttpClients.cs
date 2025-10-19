namespace ClientLibrary.Helper.Constracts;

public interface IGetHttpClients
{
    Task<HttpClient?> GetPrivateHttpClientAsync();
    HttpClient GetPublicHttpClient();
}
