using Demo.Api.Dtos;
using Demo.Infrastructure.FuelEconomyService.Interfaces;

namespace Demo.Api.Endpoints;

internal static class VehicleEmissionsEndpoint
{
    internal static void MapVehicleEmissionsEndpoint(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/api/vehicle/{vehicleId}/emissions",
            async Task<IResult> (int vehicleId, IEmissionsService emissionsService) =>
          {

              var summary = await emissionsService.GetVehicleEmission(vehicleId);
              if(summary is null)
              {
                  return TypedResults.NotFound();
              }

              return TypedResults.Ok(new EmissionResponse(summary));
          })
            .RequireAuthorization(BasicAuthenticationHandler.SchemeName); ;
    }
}