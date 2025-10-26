namespace BasedLibrary.DTOs.JWTSection;

public class JWTSection
{
    public String Key { get; set; } = String.Empty;
    public String Issuer { get; set; } = String.Empty;
    public String Audience { get; set; } = String.Empty;
    public int ExpirationMinutes { get; set; } = 45;
}
