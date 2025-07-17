using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DCAPP.Areas.Admin.Controllers;

public class DashboardController : Controller
{
    [Authorize(Roles = "Admin")]
    public IActionResult Index() => View();
}