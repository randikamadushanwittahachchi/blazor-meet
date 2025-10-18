namespace BasedLibrary.DTOs.Response.Authentication;

public record LoginResponse(bool flag, string message, string token, string refreshToken) 
{
    public static LoginResponse Sucess(string message, string token, string refreshToken) => new(true, message, token, refreshToken);
    public static LoginResponse Failure(string message, string token, string refreshToken) => new(false, message, token, refreshToken);
}
