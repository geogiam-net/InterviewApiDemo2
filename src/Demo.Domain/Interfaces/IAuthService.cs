namespace Demo.Domain.Interfaces;

public interface IAuthService
{
    public Models.Authorization? Authorize(string email);

    public bool ValidateAuthorization(string token);
}