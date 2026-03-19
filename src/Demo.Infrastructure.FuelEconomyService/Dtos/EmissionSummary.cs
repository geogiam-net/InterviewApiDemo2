namespace Demo.Infrastructure.FuelEconomyService.Dtos;

public class EmissionSummary
{
    // field id
    public int Id { get; init; } = default;

    // field make
    public string Brand { get; init; } = string.Empty;

    // field model
    public string Model { get; init; } = string.Empty;

    // field VClass
    public string Vclass { get; init; } = string.Empty;

    // field year
    public int YearOfManufacture { get; init; } = default;

    // field city08, mile per gallon
    public decimal FuelConsumptionCity { get; init; } = default;

    // field highway08, mile per gallon
    public decimal FuelConsumptionHighway { get; init; } = default;

    // field comb08, mile per gallon
    public decimal FuelConsumptionCombined { get; init; } = default;

    // field co2, gramm per mile
    public decimal Co2Emissions { get; init; } = default;
}
