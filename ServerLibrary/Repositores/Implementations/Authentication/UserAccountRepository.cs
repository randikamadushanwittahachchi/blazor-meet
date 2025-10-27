using BasedLibrary.DTOs.Request.Authentication;
using BasedLibrary.DTOs.Response;
using BasedLibrary.DTOs.Response.Authentication;
using ServerLibrary.Data;
using ServerLibrary.Repositores.Contracts.Authentication;

namespace ServerLibrary.Repositores.Implementations.Authentication;

public class UserAccountRepository : IUserAccountRepository
{

    public Task<GeneralResponse> CreateAsync(Register user)
    {
        throw new NotImplementedException();
    }

    public Task<LoginResponse> RefreshTokenAsync(RefreshToken refershToken)
    {
        throw new NotImplementedException();
    }

    public Task<LoginResponse> SigninAsync(Login user)
    {
        throw new NotImplementedException();
    }
}
