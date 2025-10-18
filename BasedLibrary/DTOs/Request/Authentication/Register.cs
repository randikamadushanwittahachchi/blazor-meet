using System.ComponentModel.DataAnnotations;

namespace BasedLibrary.DTOs.Request.Authentication;

public class Register:BasedAuthenticationRequest
{
    [Required(ErrorMessage ="Name is required.")]
    [MinLength(4,ErrorMessage = "Name shoud be minimum 4 characters.")]
    [MaxLength(20,ErrorMessage ="Name shoud be maximum 20 characters.")]
    [DataType(DataType.Text)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage ="Confirm password is required.")]
    [Compare(nameof(Password),ErrorMessage ="Password does not match.")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; } = string.Empty;
}
