using BasedLibrary.Constants;
using System.ComponentModel.DataAnnotations;

namespace BasedLibrary.DTOs.Request.Authentication;

public class BasedAuthenticationRequest
{
    [Required(ErrorMessage = ValidationMessages.EmailAddressRequired)]
    [EmailAddress(ErrorMessage =ValidationMessages.InvalidEmailAddress)]
    [DataType(DataType.EmailAddress)]
    public String Email { get; set; } = String.Empty;


    [Required(ErrorMessage =ValidationMessages.PasswordRequired)]
    [RegularExpression(@"^(?=.*[0-9])(?=.*[!@#$%^&*]).{8,}$", ErrorMessage = ValidationMessages.InvalidPassword)]
    [DataType(DataType.Password)]
    public String Password { get; set; } = String.Empty;
}
