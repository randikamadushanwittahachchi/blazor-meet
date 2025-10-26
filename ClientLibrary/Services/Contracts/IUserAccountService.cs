using BasedLibrary.DTOs.Request.Authentication;
using BasedLibrary.DTOs.Response;
using BasedLibrary.DTOs.Response.Authentication;

namespace ClientLibrary.Services.Contracts;

public interface IUserAccountService
{
    Task<GeneralResponse> RegisterAsync(Register user);
    Task<LoginResponse> LoginAsync(Login user);
    Task<LoginResponse> RefreshTokenAsync(RefreshToken token);
}
