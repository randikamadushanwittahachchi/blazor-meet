using BasedLibrary.DTOs.UserSession;
using ClientLibrary.Helper.Constracts;
using ClientLibrary.Services.Contracts;

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
        return await base.SendAsync(request, cancellationToken);
    }


}
