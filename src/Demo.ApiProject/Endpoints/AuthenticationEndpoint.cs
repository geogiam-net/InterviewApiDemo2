using Demo.Api.Dtos;
using Demo.Infrastructure.AuthorizationService.Interfaces;

namespace Demo.Api.Endpoints;

internal static class AuthenticationEndpoint
{
    internal static void MapAutheticateEndpoint(this IEndpointRouteBuilder builder)
    {
        // For this demo we are using a simple email-based authorization, which is not secure and should not be used in production. In a real application, you would typically use a more secure method of authentication, such as JWT tokens or OAuth, which also support more data about the user, like roles and permissions.
        // For more info:
        // https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis/security?view=aspnetcore-10.0
        // https://www.browserstack.com/guide/authorization-header

        builder.MapPost("/api/auth", 
            IResult (AuthenticationRequest authRequest, IAuthService authService) =>
          {
              var authorization = authService.Authenticate(authRequest.Email);
              if (authorization is null) {
                  return TypedResults.Unauthorized();
              }

              return TypedResults.Ok(authorization);
          });
    }
}