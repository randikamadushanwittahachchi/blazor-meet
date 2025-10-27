namespace BasedLibrary.Constants;

/// <summary>
/// Standard validation error message use acros the application. 
/// </summary>
public static class ValidationMessages
{
    // Name-related messages
    public const string NameRequired = "Name field  is required.";
    public const string NameMinLength = "Name should be at least 4 characters long.";
    public const string NameMaxLength = "Name should not exceed 20 characters.";
    public const string NameLengthExceeded = "The Name field must not exceed 100 characters.";

    // Email-related messages
    public const string EmailAddressRequired = "Email address field  is required.";
    public const string InvalidEmailAddress = "Please enter a valid email address.";

    // Password-related messages
    public const string PasswordRequired = "Password field  is required.";
    public const string InvalidPassword = "Password must be at least 8 characters long and include at least one number and one special character.";

    // ConfirmPassword-related messages
    public const string ConfirmPasswordRequired = "Confirm password field  is required.";
    public const string ConfirmPasswordMismatch = "Password does not match.";

    // UserRole related messages
    public const string AppUserIdRequired = "User id field  is required.";
    public const string InvalidAppUserId = "Please enter a valid user id.";
    public const string SystemRoleRequired = "System role field  is required.";
    public const string InvalidSystemRoleId = "Please enter a valid system role id.";

}
