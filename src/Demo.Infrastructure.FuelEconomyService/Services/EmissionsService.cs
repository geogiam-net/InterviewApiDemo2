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
        // For this demo, using the HttpClient in this way is enough, but in production it must not be like this.

        // I have years of experience of using the HttpClient class for webrobots, and I am aware of the potential socket exhaustion, because one can dispose it in .Net but the internal Windows wsocket might be disposed much later, that is out of our control, that is up to Windows.

        // For production the recommendation from Microsoft is to consider using IHttpClientFactory to manage HttpClient instances, the idea is to create a pool of HttpClients that can be reused, that means the sockets are permanently reserved, if much traffic comes to the Api App, instead of exausting all sockets and provoke exceptions, the application has to place threads hold until a HttpClient from the queue is free or timeout.

        string url = $"{SourceUrl}{vehicleId}";
        using HttpClientHandler handler = new HttpClientHandler { UseCookies = false };
        using HttpClient client = new HttpClient(handler) { BaseAddress = new Uri(url) };

        var response = await client.GetAsync(url);

        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }

        if (!response.IsSuccessStatusCode)
        {
            throw new DataAccessException("Failed to retrieve vehicle data.");
        }

        string xmlContent = await response.Content.ReadAsStringAsync();
        var xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xmlContent);

        XmlNode? brandNode = xmlDoc.SelectSingleNode("//make");
        string brand = brandNode?.InnerText ?? string.Empty;

        XmlNode? modelNode = xmlDoc.SelectSingleNode("//model");
        string model = modelNode?.InnerText ?? string.Empty;

        XmlNode? vClassNode = xmlDoc.SelectSingleNode("//VClass");
        string vClass = vClassNode?.InnerText ?? string.Empty;

        XmlNode? yearNode = xmlDoc.SelectSingleNode("//year");
        int.TryParse(yearNode?.InnerText, out int year);

        XmlNode? cityNode = xmlDoc.SelectSingleNode("//city08");
        decimal.TryParse(cityNode?.InnerText, out decimal city);

        XmlNode? highwayNode = xmlDoc.SelectSingleNode("//highway08");
        decimal.TryParse(highwayNode?.InnerText, out decimal highway);

        XmlNode? combinedNode = xmlDoc.SelectSingleNode("//comb08");
        decimal.TryParse(combinedNode?.InnerText, out decimal combined);

        XmlNode? co2Node = xmlDoc.SelectSingleNode("//co2");
        decimal.TryParse(co2Node?.InnerText, out decimal co2);

        var summary = new EmissionSummary()
        {
            Id = vehicleId,
            Brand = brand,
            Model = model,
            Vclass = vClass,
            YearOfManufacture = year,
            FuelConsumptionCity = city,
            FuelConsumptionHighway = highway,
            FuelConsumptionCombined = combined,
            Co2Emissions = co2,
        };

        return summary;
    }
}