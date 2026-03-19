using Demo.Api.Dtos;
using Demo.Infrastructure.AuthorizationService.Interfaces;

namespace Demo.Api.Endpoints;

internal static class AuthorizationEndpoint
{
    internal static void MapAuthorizationEndpoint(this IEndpointRouteBuilder builder)
    {
        // TODO comment               

        // https://www.browserstack.com/guide/authorization-header

        builder.MapPost("/api/authorize", 
            IResult (AuthorizeRequest authRequest, IAuthService authService) =>
          {
              var authorization = authService.Authorize(authRequest.Email);
              if (authorization is null) {
                  return TypedResults.Unauthorized();
              }

              return TypedResults.Ok(authorization);
          });
    }
}