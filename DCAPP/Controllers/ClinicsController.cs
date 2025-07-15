using DCAPP.Models;
using DCAPP.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DCAPP.Controllers;

public class ClinicsController(IClinicsService service) : Controller
{
    private readonly IClinicsService _service = service;

    public IActionResult Index([FromQuery] int currentPage = 1, [FromQuery] int pageSize = 5)
    {
        if (currentPage < 1) currentPage = 1;
        if (pageSize <= 0) pageSize = 5;

        var model = new PaginatedViewModel { CurrentPage = currentPage, PageSize = pageSize };
        return View(model);
    }

    public async Task<IActionResult> Details(int id)
    {
        var clinic = await _service.GetClinic(id);
        return View(clinic);
    }
}