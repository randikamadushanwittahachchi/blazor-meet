namespace BasedLibrary.DTOs.Response;

public record DataResponse<T>(bool flag, string message,T? data)
{
    public static DataResponse<T> Success(T data, string message = "Operation completed successfully.") => new(true, message, data);
    public static DataResponse<T> Failure(string message = "Operation failed to complete.") => new(false, message, default );
}
