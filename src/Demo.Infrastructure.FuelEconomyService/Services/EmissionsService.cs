using System.Xml.Serialization;
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
        // I am aware of the potential socket exhaustion, because one can dispose it in .Net but the internal Windows socket might be disposed much later, that is out of our control, that is up to Windows.
        // For production the recommendation from Microsoft is to consider using IHttpClientFactory to manage HttpClient instances, the idea is to create a pool of HttpClients that can be reused.d until a HttpClient from the queue is free or timeout.

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

        var serializer = new XmlSerializer(typeof(EmissionSummary));
        EmissionSummary? result = null;

        using (TextReader reader = new StringReader(xmlContent))
        {
            result = (EmissionSummary?)serializer.Deserialize(reader);
        }

        if (result is null) {             
            throw new Exception("Failed to deserialize vehicle emission data.");
        }

        return result;
    }
}