using DCAPP.Models;
using DCAPPLIB.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DCAPP.ViewComponents;

public class GetClinicsWithPaginationViewComponent(IHttpClientFactory httpClientFactory) : ViewComponent
{
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    public async Task<IViewComponentResult> InvokeAsync(PaginatedViewModel paginatedViewModel)
    {
        var client = _httpClientFactory.CreateClient("DentalClinicAPI");
        var totalCount = await client.GetFromJsonAsync<int>("/api/clinicsCount");
        var clinics = await client.GetFromJsonAsync<List<Clinical>>($"/api/clinics?currentPage={paginatedViewModel.CurrentPage}&pageSize={paginatedViewModel.PageSize}");
        var model = new PaginationDataModel<Clinical>()
        {
            PaginatedList = clinics,
            PaginationModel = new()
            {
                TotalCount = totalCount,
                CurrentPage = paginatedViewModel.CurrentPage,
                PageSize = paginatedViewModel.PageSize
            }
        };
        return View(model);
    }
}