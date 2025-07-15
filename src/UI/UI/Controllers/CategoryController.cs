using Microsoft.AspNetCore.Mvc;
using UI.Data.UnitOfWork;
using UI.Models;
using UI.Utilities;

namespace UI.Controllers
{
	public class CategoryController(IUnitOfWork unitOfWork, IAuthenticator authenticator) : BaseController
	{
		public async Task<IActionResult> Index()
		{
			if (!authenticator.Authenticate(UserTypes.Admin))
			{
				return RedirectToAction("Login", "Client");
			}

			return View(await unitOfWork.Category.GetAll());
		}

		public IActionResult Create()
		{
			if (!authenticator.Authenticate(UserTypes.Admin))
			{
				return RedirectToAction("Login", "Client");
			}

			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Name")] Category category)
		{
			if (!authenticator.Authenticate(UserTypes.Admin))
			{
				return RedirectToAction("Login", "Client");
			}

			if (ModelState.IsValid)
			{
				await unitOfWork.Category.Create(category);
				return RedirectToAction(nameof(Index));
			}

			return View(category);
		}

		public async Task<IActionResult> Edit(int id)
		{
			if (!authenticator.Authenticate(UserTypes.Admin))
			{
				return RedirectToAction("Login", "Client");
			}

			var category = await unitOfWork.Category.GetById(id);
			if (category == null)
			{
				return NotFound();
			}

			return View(category);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Category category)
		{
			if (!authenticator.Authenticate(UserTypes.Admin))
			{
				return RedirectToAction("Login", "Client");
			}

			if (id != category.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				await unitOfWork.Category.Update(category);
				return RedirectToAction(nameof(Index));
			}

			return View(category);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(int id)
		{
			if (!authenticator.Authenticate(UserTypes.Admin))
			{
				return RedirectToAction("Login", "Client");
			}

			var category = await unitOfWork.Category.GetById(id);
			if (category != null)
			{
				await unitOfWork.Category.Delete(id);
			}

			return RedirectToAction(nameof(Index));
		}
	}
}