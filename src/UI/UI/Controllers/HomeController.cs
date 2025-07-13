using Microsoft.AspNetCore.Mvc;
using UI.Data.ApiClient;
using UI.Data.UnitOfWork;
using UI.Models;
using System.Diagnostics;

namespace UI.Controllers
{
	public class HomeController(IUnitOfWork unitOfWork, IApiClient client)
		: BaseController
	{
		public async Task<IActionResult> Index(int? categoryId)
        {
            var spareParts = await unitOfWork.SparePart.GetAll();

            if (categoryId.HasValue)
            {
                spareParts = spareParts.Where(s => s.CategoryId == categoryId.Value).ToList();
            }

            ViewBag.CategoryId = await unitOfWork.Category.GetAll(); 
            return View(spareParts);
        }

        public IActionResult Privacy()
		{
			return View();
		}
        public IActionResult Contact()
        {
            return View();
        }
     
        public IActionResult Logout()
		{
			client.SetToken("");
			HttpContext.Session.SetString("Role", "");
			HttpContext.Session.SetInt32("ClientId", -1);
			HttpContext.Session.SetString("Email", "");

			return RedirectToAction(nameof(Index));
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}