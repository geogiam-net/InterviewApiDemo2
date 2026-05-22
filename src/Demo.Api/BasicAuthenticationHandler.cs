using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using Demo.Infrastructure.AuthorizationService.Interfaces;

namespace Demo.Api;

// https://www.roundthecode.com/dotnet-tutorials/how-to-secure-asp-net-core-apis-basic-authentication
public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public const string SchemeName = "BasicAuthentication";
    private readonly IAuthService AuthService;

    public BasicAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        IAuthService authService,
        UrlEncoder encoder) : base(options, logger, encoder) {

        AuthService = authService;
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue("Authorization", out var authHeaderValue))
        {
            return Task.FromResult(AuthenticateResult.Fail(
                "'Authorization' is missing from the request header")
            );
        }

        var token = authHeaderValue.ToString();

        if (!AuthService.ValidateToken(token))
        {
            return Task.FromResult(AuthenticateResult.Fail(
                "Unauthorized")
            );
        }

        var identity = new ClaimsIdentity(
            [
                new Claim(ClaimTypes.Email, "")
            ],
            SchemeName
            );

        return Task.FromResult(AuthenticateResult.Success(
            new AuthenticationTicket(
                new ClaimsPrincipal(identity),
                SchemeName
            )));
    }
}
