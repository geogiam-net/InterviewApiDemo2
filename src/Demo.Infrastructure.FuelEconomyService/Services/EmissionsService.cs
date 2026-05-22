using Demo.Business.Exceptions;
using Demo.Infrastructure.FuelEconomyService.Dtos;
using Demo.Infrastructure.FuelEconomyService.Interfaces;
using Microsoft.Extensions.Logging;
using System.Xml.Serialization;

namespace Demo.Infrastructure.FuelEconomyService.Services;

// Reference at https://www.fueleconomy.gov/feg/ws/
public class EmissionsService(
    IHttpClientFactory httpClientFactory,
    ILogger<EmissionsService> logger) : IEmissionsService
{
    private const string SourceUrl = "https://www.fueleconomy.gov/ws/rest/vehicle/";

    // querying external sources could be cached in production in memory or Reddis server
    public async Task<EmissionSummary?> GetVehicleEmission(int vehicleId)
    {
        HttpClient client = httpClientFactory.CreateClient();
        EmissionSummary? result = null;

        try
        {
            string url = $"{SourceUrl}{vehicleId}";

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


            using (TextReader reader = new StringReader(xmlContent))
            {
                result = (EmissionSummary?)serializer.Deserialize(reader);
            }
        }
        catch (Exception ex)
        {
            logger.LogError("Error when getting a Vehicle Emission: {Error}", ex);
        }

        if (result is null) {             
            throw new Exception("Failed to deserialize vehicle emission data.");
        }

        return result;
    }
}