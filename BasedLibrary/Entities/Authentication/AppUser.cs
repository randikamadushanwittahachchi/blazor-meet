using BasedLibrary.Constants;
using System.ComponentModel.DataAnnotations;

namespace BasedLibrary.Entities.Authentication;

/// <summary>
/// Represent as application user entity that holds authentication-related details 
/// </summary>
public class AppUser
{
    /// <summary>
    /// Unique identifier for the user
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// user's name
    /// </summary>
    [Required(ErrorMessage = ValidationMessages.NameRequired)]
    [MinLength(4, ErrorMessage = ValidationMessages.NameMinLength)]
    [MaxLength(20, ErrorMessage = ValidationMessages.NameMaxLength)]
    [DataType(DataType.Text)]
    public String Name { get; set; } = String.Empty;

    /// <summary>
    /// user's email address used for login and communication
    /// </summary>
    [Required(ErrorMessage = ValidationMessages.EmailAddressRequired)]
    [EmailAddress(ErrorMessage = ValidationMessages.InvalidEmailAddress)]
    [DataType(DataType.EmailAddress)]
    public String Email { get; set; } = String.Empty;

    /// <summary>
    /// user's password or row password input (depending on storage logic).
    /// </summary>
    [Required(ErrorMessage = ValidationMessages.PasswordRequired)]
    [RegularExpression(@"^(?=.*[0-9])(?=.*[!@#$%^&*]).{8,}$", ErrorMessage = ValidationMessages.InvalidPassword)]
    [DataType(DataType.Password)]
    public String Password { get; set; } = String.Empty;

}
