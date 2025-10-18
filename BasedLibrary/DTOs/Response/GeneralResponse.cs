
namespace BasedLibrary.DTOs.Response;

public record GeneralResponse(bool flag,string message)
{
    public static GeneralResponse Success(string message = "Process is success.") => new(true, message);
    public static GeneralResponse Failure(string message = "Process is un success.") => new(false, message);
}
