namespace Demo.Infrastructure.AuthorizationService.Models;

public class Authentication
{
    public string Email { get; set; } = string.Empty;

    public string Token { get; set; } = string.Empty;

    // Utf time
    public DateTime Expiration { get; set; } = default;

    public static DateTime GenerateExpirationDate(DateTime nowUtf)
    {
        return nowUtf.AddDays(30);
    }
}
