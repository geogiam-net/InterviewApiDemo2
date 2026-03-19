using System.Xml;
using Demo.Domain.Exceptions;
using Demo.Infrastructure.FuelEconomyService.Dtos;
using Demo.Infrastructure.FuelEconomyService.Interfaces;

namespace Demo.Infrastructure.FuelEconomyService.Services;

// Reference at https://www.fueleconomy.gov/feg/ws/
public class EmissionsService : IEmissionsService
{
    private const string SourceUrl = "https://www.fueleconomy.gov/ws/rest/vehicle/";

    public async Task<EmissionSummary?> GetVehicleEmission(int vehicleId)
    {
        string url = $"{SourceUrl}{vehicleId}";
        using HttpClientHandler handler = new HttpClientHandler { UseCookies = false };
        using HttpClient client = new HttpClient(handler) { BaseAddress = new Uri(url) };

        var response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();

        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }

        if (!response.IsSuccessStatusCode)
        {
            throw new DomainException($"Failed to retrieve vehicle data. Status code: {response.StatusCode}");
        }

        string xmlContent = await response.Content.ReadAsStringAsync();
        var xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xmlContent);

        // TODO read the xml and map to EmissionSummary
        var summary = new EmissionSummary()
        {
            Id = 0,
            Brand = string.Empty,
            Model = string.Empty,
            Vclass = string.Empty,
            YearOfManufacture = 0,
            FuelConsumptionCity = 0M,
            FuelConsumptionHighway = 0M,
            FuelConsumptionCombined = 0M,
            Co2Emissions = 0M,
        };

        return summary;
    }
}