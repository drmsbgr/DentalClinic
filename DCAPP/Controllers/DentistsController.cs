using DCAPP.Models;
using DCAPP.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DCAPP.Controllers;

public class DentistsController(IDentistsService _service) : Controller
{
    public IActionResult Index([FromQuery] int currentPage = 1, [FromQuery] int pageSize = 5)
    {
        if (currentPage < 1) currentPage = 1;
        if (pageSize <= 0) pageSize = 5;

        var model = new PaginatedViewModel { CurrentPage = currentPage, PageSize = pageSize };
        return View(model);
    }

    public async Task<IActionResult> Details(int id)
    {
        var dentist = await _service.GetDentist(id);
        return View(dentist);
    }
}