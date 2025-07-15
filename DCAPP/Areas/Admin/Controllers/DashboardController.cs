using Microsoft.AspNetCore.Mvc;

namespace DCAPP.Areas.Admin.Controllers;

public class DashboardController : Controller
{
    public IActionResult Index() => View();
}