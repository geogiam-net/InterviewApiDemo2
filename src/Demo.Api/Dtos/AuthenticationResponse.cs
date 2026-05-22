using Demo.Infrastructure.AuthorizationService.Models;
using Demo.Infrastructure.FuelEconomyService.Dtos;
using System.Reflection;

namespace Demo.Api.Dtos;

public class AuthenticationResponse
{
    public string Token { get; set; } = string.Empty;

    public DateTime Expiration { get; set; } = default;

    public AuthenticationResponse(Authentication auth)
    {
        Token = auth.Token;
        Expiration = auth.Expiration;
    }
}
