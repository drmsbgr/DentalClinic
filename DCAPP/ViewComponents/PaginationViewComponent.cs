using DCAPP.Models;
using Microsoft.AspNetCore.Mvc;

namespace DCAPP.ViewComponents;

public class PaginationViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(PaginationModel model)
    {
        return View(model);
    }
}