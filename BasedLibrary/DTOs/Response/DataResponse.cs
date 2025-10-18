namespace BasedLibrary.DTOs.Response;

public record DataResponse<T>(bool flag, string message,T? data)
{
    public static DataResponse<T> Success(T data, string message = "Process is success") => new(true, message, data);
    public static DataResponse<T> Failure(string message = "Process is un success") => new(false, message, default );
}
