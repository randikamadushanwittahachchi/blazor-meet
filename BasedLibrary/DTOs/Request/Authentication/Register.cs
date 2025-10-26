using BasedLibrary.Constants;
using System.ComponentModel.DataAnnotations;

namespace BasedLibrary.DTOs.Request.Authentication;

public class Register:BasedAuthenticationRequest
{
    [Required(ErrorMessage =ValidationMessages.NameRequired)]
    [MinLength(4,ErrorMessage = ValidationMessages.NameMinLength)]
    [MaxLength(20,ErrorMessage =ValidationMessages.NameMaxLength)]
    [DataType(DataType.Text)]
    public String Name { get; set; } = String.Empty;

    [Required(ErrorMessage =ValidationMessages.ConfirmPasswordRequired)]
    [Compare(nameof(Password),ErrorMessage =ValidationMessages.ConfirmPasswordMismatch)]
    [DataType(DataType.Password)]
    public String ConfirmPassword { get; set; } = String.Empty;
}
