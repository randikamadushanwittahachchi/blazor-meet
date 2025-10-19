using Microsoft.AspNetCore.Components.Authorization;

namespace ClientLibrary.Authentication.CustomAuthenticationStateProvider;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        throw new NotImplementedException();
    }
}
