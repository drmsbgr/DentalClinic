using DCAPP.Services.Contracts;
using DCAPPLIB.Entities;

namespace DCAPP.Services;

public class ClinicsService(IHttpClientFactory httpClientFactory) : IClinicsService
{
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

    public async Task<Clinical> GetClinic(int id)
    {
        var client = _httpClientFactory.CreateClient("DentalClinicAPI");
        var clinic = await client.GetFromJsonAsync<Clinical>($"/api/getClinicById/{id}");
        return clinic!;
    }

    public List<Clinical> GetClinicals()
    {
        return [];
    }
}