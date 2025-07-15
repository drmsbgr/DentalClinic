using Microsoft.AspNetCore.Mvc;

namespace DCAPP.ViewComponents;

public class CountOfAnyViewComponent(IHttpClientFactory httpClientFactory) : ViewComponent
{
    public async Task<string> InvokeAsync(string target)
    {
        var client = httpClientFactory.CreateClient("DentalClinicAPI");
        var totalCount = await client.GetFromJsonAsync<int>($"/api/{target}Count");
        return totalCount.ToString();
    }
}