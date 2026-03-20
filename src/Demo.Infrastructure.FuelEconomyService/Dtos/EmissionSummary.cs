using System.Xml.Serialization;

namespace Demo.Infrastructure.FuelEconomyService.Dtos;

[XmlRoot(ElementName = "vehicle")]
public class EmissionSummary
{
    [XmlElement(ElementName = "id")]
    public int Id { get; init; } = default;

    [XmlElement(ElementName = "make")]
    public string Brand { get; init; } = string.Empty;

    [XmlElement(ElementName = "model")]
    public string Model { get; init; } = string.Empty;

    [XmlElement(ElementName = "VClass")]
    public string Vclass { get; init; } = string.Empty;

    [XmlElement(ElementName = "year")]
    public int YearOfManufacture { get; init; } = default;

    // mile per gallon
    [XmlElement(ElementName = "city08")]
    public decimal FuelConsumptionCity { get; init; } = default;

    // mile per gallon
    [XmlElement(ElementName = "highway08")]
    public decimal FuelConsumptionHighway { get; init; } = default;

    // mile per gallon
    [XmlElement(ElementName = "comb08")]
    public decimal FuelConsumptionCombined { get; init; } = default;

    // gramm per mile
    [XmlElement(ElementName = "co2")]
    public decimal Co2Emissions { get; init; } = default;
}
