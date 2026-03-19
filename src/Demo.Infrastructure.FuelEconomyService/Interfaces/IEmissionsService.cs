using Demo.Infrastructure.FuelEconomyService.Dtos;

namespace Demo.Infrastructure.FuelEconomyService.Interfaces;

public interface IEmissionsService
{
    Task<EmissionSummary?> GetVehicleEmission(int vehicleId);
}