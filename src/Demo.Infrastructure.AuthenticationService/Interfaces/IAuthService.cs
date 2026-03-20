namespace Demo.Infrastructure.AuthorizationService.Interfaces;

public interface IAuthService
{
    public Models.Authentication Authenticate(string email);

    public bool ValidateToken(string token);
}