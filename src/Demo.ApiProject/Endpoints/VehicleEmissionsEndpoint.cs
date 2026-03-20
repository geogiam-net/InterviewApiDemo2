using Demo.Api.Dtos;
using Demo.Infrastructure.AuthorizationService.Interfaces;
using Demo.Infrastructure.FuelEconomyService.Interfaces;

namespace Demo.Api.Endpoints;

internal static class VehicleEmissionsEndpoint
{
    internal static void MapVehicleEmissionsEndpoint(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/api/vehicle/{vehicleId}/emissions",
            async Task<IResult> (int vehicleId, IEmissionsService emissionsService, IAuthService authService, HttpRequest request) =>
          {
              if (!request.Headers.TryGetValue("token", out var tokenValues))
              {
                  return TypedResults.Unauthorized();
              }

              var token = tokenValues.ToString();

              if (!authService.ValidateToken(token))
              {
                  return TypedResults.Unauthorized();
              }

              var summary = await emissionsService.GetVehicleEmission(vehicleId);
              if(summary is null)
              {
                  return TypedResults.NotFound();
              }

              return TypedResults.Ok(new EmissionResponse(summary));
          });
    }
}