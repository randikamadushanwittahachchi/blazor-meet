using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Constants;

/// <summary>
/// Defines reusable response message constants for consistent system output.
/// </summary>
public static class ResponseMessages
{
    // General operation results
    public const string Success = "Operation completed successfully.";
    public const string Failuer = "Operation failed to complete.";
    public const string NotFound = "Requested record not found.";
    public const string AlreadyExists = "Record already exists.";
    public const string InvalidRequest = "Invalid request data.";
    public const string Unauthorized = "Unauthorized access.";
    public const string Forbidden = "Access denied";
    public const string SereverError = "An unexpected server error occurred";

    // Authentication & Authorization
    public const string LoginSuccess = "Login Successful.";
    public const string LoginFailed = "Invalid email or password.";
    public const string RegistrationSuccess = "User registered successfully";
    public const string RegistrationFailed = "User registeration failed.";
    public const string TokenGenerated = "Token generated successfully.";
    public const string TokenRefreshSuccess = "Token refreshed successfully.";
    public const string TokenInvalid = "Invalid or expired token";

    // Role & Permission
    public const string RoleNotFound = "User role not found.";
    public const string RoleInvalid = "Invalid user role.";

    // Data validation
    public const string ValidationFailed = "One or more validation error occured.";

}
