using Microsoft.AspNetCore.Mvc;

namespace CafeShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MaterialController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
