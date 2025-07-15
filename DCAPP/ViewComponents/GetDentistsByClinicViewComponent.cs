using DCAPPLIB.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DCAPP.ViewComponents;

public class GetDentistsByClinicViewComponent(IHttpClientFactory httpClientFactory) : ViewComponent
{
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    public async Task<IViewComponentResult> InvokeAsync(int id)
    {
        var client = _httpClientFactory.CreateClient("DentalClinicAPI");
        var dentists = await client.GetFromJsonAsync<List<Dentist>>($"/api/getDentistsByClinic/{id}");
        return View(dentists ?? []);
    }
}