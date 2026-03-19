using Demo.Domain.Interfaces;

namespace Demo.Api.Endpoints;

internal static class VehicleEmissionsEndpoint
{
    internal static void MapVehicleEmissionsEndpoint(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/api/vehicle/emissions/{vehicleId}",
            async Task<IResult> (string vehicleId) =>
          {
              //var user = await userRepository.GetUserAsync(username);
              //if (user is null)
              //{
              //    return TypedResults.NotFound();
              //}

              //return TypedResults.Ok(new UserDto(user));

              return TypedResults.Ok();
          });
    }
}