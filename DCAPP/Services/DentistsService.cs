using DCAPP.Services.Contracts;
using DCAPPLIB.Entities.Dtos.Dentist;

namespace DCAPP.Services;

public class DentistsService(IHttpClientFactory httpClientFactory) : IDentistsService
{
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

    public async Task<DentistWithClinicDto> GetDentist(int id)
    {
        var client = _httpClientFactory.CreateClient("DentalClinicAPI");
        var dentist = await client.GetFromJsonAsync<DentistWithClinicDto>($"/api/dentists/{id}");
        return dentist!;
    }

    public List<DentistWithClinicDto> GetDentists()
    {
        return [];
    }
}