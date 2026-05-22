using Demo.Infrastructure.FuelEconomyService.Dtos;

namespace Demo.Api.Dtos;

public class EmissionResponse
{
    public int Id { get; init; } = default;

    public string Brand { get; init; } = string.Empty;

    public string Model { get; init; } = string.Empty;

    public string Vclass { get; init; } = string.Empty;

    public int YearOfManufacture { get; init; } = default;

    public decimal FuelConsumptionCity { get; init; } = default;

    public decimal FuelConsumptionHighway { get; init; } = default;

    public decimal FuelConsumptionCombined { get; init; } = default;

    public decimal Co2Emissions { get; init; } = default;

    public EmissionResponse(EmissionSummary emissionSummary) { 
        Id = emissionSummary.Id;
        Brand = emissionSummary.Brand;
        Model = emissionSummary.Model;
        Vclass = emissionSummary.Vclass;
        YearOfManufacture = emissionSummary.YearOfManufacture;
        FuelConsumptionCity = emissionSummary.FuelConsumptionCity;
        FuelConsumptionHighway = emissionSummary.FuelConsumptionHighway;
        FuelConsumptionCombined = emissionSummary.FuelConsumptionCombined;
        Co2Emissions = emissionSummary.Co2Emissions;
    }
}
