using System.Text.RegularExpressions;

namespace Demo.Infrastructure.AuthorizationService.Validators;

// We could return the error messages in the user language with an injected service. For simplicity, the error messages are in English.
public static class AuthenticationValidator
{
    // this can be fetched from https://github.com/disposable/disposable-email-domains, stored in a file and we read it once and cache it in memory. For simplicity, I am hardcoding a few disposable email domains here. It would be injected as a service into the Domain.
    public static readonly List<string> DisposableEmailDomainList = new List<string> { "@mailinator.com" };

    public static string ValidateEmail(string email)
    {
        email = email.ToLower().Trim();

        if (String.IsNullOrEmpty(email))
        {
            return "Email is empty"; ;
        }

        var isValid = Regex.IsMatch(email, @"^[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+(?:[a-zA-Z]{2,6})$", RegexOptions.IgnorePatternWhitespace);

        if (!isValid)
        {
            return "Email has wrong format"; ;
        }

        if(DisposableEmailDomainList.Any(d => email.EndsWith(d)))
        {
            return "Email domain is not allowed";
        }

        return string.Empty;
    }

    public static string ValidateExpiration(DateTime expirationUtf, DateTime nowUtf)
    {
        if (nowUtf > expirationUtf)
        {
            return "Authorization token has expired";
        }

        return string.Empty;
    }
}