using DCAPP.Models;
using DCAPPLIB.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DCAPP.ViewComponents;

public class GetDentistsWithPaginationViewComponent(IHttpClientFactory httpClientFactory) : ViewComponent
{
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    public async Task<IViewComponentResult> InvokeAsync(PaginatedViewModel paginatedViewModel)
    {
        var client = _httpClientFactory.CreateClient("DentalClinicAPI");
        var totalCount = await client.GetFromJsonAsync<int>("/api/dentistsCount");
        var dentists = await client.GetFromJsonAsync<List<Dentist>>($"/api/dentists?currentPage={paginatedViewModel.CurrentPage}&pageSize={paginatedViewModel.PageSize}");
        var model = new PaginationDataModel<Dentist>()
        {
            PaginatedList = dentists,
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