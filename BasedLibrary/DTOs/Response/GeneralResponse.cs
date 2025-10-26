
namespace BasedLibrary.DTOs.Response;

public record GeneralResponse(bool flag,string message)
{
    public static GeneralResponse Success(string message = "Operation completed successfully.") => new(true, message);
    public static GeneralResponse Failure(string message = "Operation failed to complete.") => new(false, message);
}
