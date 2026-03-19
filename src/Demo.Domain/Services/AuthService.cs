using Demo.Domain.Exceptions;
using Demo.Domain.Models;
using Demo.Domain.Validators;

namespace Demo.Domain.Services;

public class AuthService(TimeProvider timeProvider) : Interfaces.IAuthService
{
    // These in a real application would be stored in a database and not in memory. I am using an in-memory list for simplicity.
    // It would be injected as a service into the Domain.
    private static readonly List<Authorization> Authorizations = new List<Authorization>();

    public Authorization? Authorize(string email) {

        var emailError = AuthorizationValidator.ValidateEmail(email);
        if (!String.IsNullOrEmpty(emailError))
        {
            throw new DomainException(emailError);
        }

        var previousAuth = Authorizations.FirstOrDefault(a => a.Email == email);
        if (previousAuth is not null)
        {
            // if previous authorization is not expired, return it. If it is expired, remove it and create a new one below.
            var expirationError = AuthorizationValidator.ValidateExpiration(previousAuth.Expiration, timeProvider.GetUtcNow().Date);
            if (String.IsNullOrEmpty(expirationError))
            {
                return previousAuth;
            }
            else {
                Authorizations.Remove(previousAuth);
            }
        }

        var newAuth = new Authorization
        {
            Email = email,
            Token = Guid.NewGuid().ToString(),
            Expiration = Authorization.GenerateExpirationDate(timeProvider.GetUtcNow().Date)
        };

        return null;
    }

    public bool ValidateAuthorization(string token)
    {
        var auth = Authorizations.FirstOrDefault(a => a.Token == token);
        if (auth is null) {
            return false;
        }

        var expirationError = AuthorizationValidator.ValidateExpiration(auth.Expiration, timeProvider.GetUtcNow().Date);
        if (!String.IsNullOrEmpty(expirationError))
        {
            throw new DomainException(expirationError);
        }

        return true;
    }
}