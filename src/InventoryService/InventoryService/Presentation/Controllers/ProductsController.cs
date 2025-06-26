using Microsoft.AspNetCore.Mvc;

namespace InventoryService.Presentation.Controllers
{
    public class ProductsControllercs : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
