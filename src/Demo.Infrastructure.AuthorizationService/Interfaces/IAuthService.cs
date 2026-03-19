namespace Demo.Infrastructure.AuthorizationService.Interfaces;

public interface IAuthService
{
    public Models.Authorization Authorize(string email);

    public bool ValidateToken(string token);
}