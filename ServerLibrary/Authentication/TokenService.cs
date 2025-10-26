using BasedLibrary.DTOs.JWTSection;
using BasedLibrary.Entities.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ServerLibrary.Authentication;

/// <summary>
/// Provides the functionality for generating json web tokens (JWTs)
/// use for authenticating and authorizion application user
/// </summary>
public class TokenService
{
    private readonly IOptions<JWTSection> _config;

    /// <summary>
    /// Initializes a new instance of the <see cref="TokenService"/> class
    /// with application JWT configuration settings.
    /// </summary>
    /// <param name="config">Injected configuration settings for JWT (key, issuer, audience, etc.).</param>
    public TokenService(IOptions<JWTSection> config)
    {
        _config = config;
    }

    /// <summary>
    /// Generates a signed the JSON web token(JWT) for the specified user and role
    /// </summary>
    /// <param name="appUser">the application use for whom the token will be generated</param>
    /// <param name="role">the user's assigned role use for authorization claims </param>
    /// <returns>
    /// a signed JWT token string containing user's id,name,email,role valid for 45 minutes from the time of creation.
    /// </returns>
    public string GetToken(AppUser appUser, string role)
    {
        // create the security key from the configured secret key
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Value.Key));

        // Define the singing credentials using HMAC-SHA256 algorithm.
        var credential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        // Define user-specific claims embedded in the token.
        var userClaims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, appUser.Id.ToString()),
            new Claim(ClaimTypes.Name, appUser.Name),
            new Claim(ClaimTypes.Email, appUser.Email),
            new Claim(ClaimTypes.Role, role)
        };

        // create the JWT token with issure, audience, claims, expiration and credentals.
        var token = new JwtSecurityToken(
            issuer:_config.Value.Issuer,
            audience:_config.Value.Audience,
            claims:userClaims,
            expires:DateTime.Now.AddMinutes(_config.Value.ExpirationMinutes),
            signingCredentials:credential
            );

        // serialize and return the token as string.
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}
