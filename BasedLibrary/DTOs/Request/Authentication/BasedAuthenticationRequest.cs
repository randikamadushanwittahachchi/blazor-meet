using System.ComponentModel.DataAnnotations;

namespace BasedLibrary.DTOs.Request.Authentication;

public class BasedAuthenticationRequest
{
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage ="Please enter a valid email address.")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = string.Empty;


    [Required(ErrorMessage ="Password is required.")]
    [RegularExpression(@"^(?=.*[0-9])(?=.*[!@#$%^&*]).{8,}$",
        ErrorMessage = "Password must be at least 8 characters long and include at least one number and one special character.")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
}
