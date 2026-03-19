

namespace Demo.Api.Endpoints;

internal static class AuthorizationEndpoint
{
    internal static void MapAuthorizationEndpoint(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("/api/authorize", 
            async Task<IResult> (string email) =>
          {
              //await userRepository.AddUserAsync(user.Username, user.Name, user.DateOfBirth);

              //// return 201 with link to created resource, because there is nothing new to return
              //return TypedResults.Created(
              //  uri: $"/api/users/{user.Username}",
              //  value: user);

              return TypedResults.Ok();
          });
    }
}