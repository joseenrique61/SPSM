using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UI.Data.UnitOfWork;
using UI.Models;
using UI.Utilities;

namespace UI.Controllers
{
	public class SparePartController(
		IUnitOfWork unitOfWork,
		IAuthenticator authenticator,
		IWebHostEnvironment webHostEnvironment)
		: BaseController
	{
		public async Task<IActionResult> Index()
		{
			if (!authenticator.Authenticate(UserTypes.Admin))
			{
				return RedirectToAction("Login", "Client");
			}

			ViewBag.CategoryId = await unitOfWork.Category.GetAll();
			return View(await unitOfWork.SparePart.GetAll());
		}

		public async Task<IActionResult> Details(int id)
		{
			var sparePart = await unitOfWork.SparePart
				.GetById(id);
			if (sparePart == null)
			{
				return NotFound();
			}

			if (HttpContext.Session.GetString("Role") == UserTypes.Client)
			{
				PurchaseOrder purchaseOrder =
					await unitOfWork.PurchaseOrder.GetCurrentByClientId(
						(int)HttpContext.Session.GetInt32("ClientId")!);
				Order? order = purchaseOrder.Orders.FirstOrDefault(o => o.SparePartId == id);
				ViewBag.AmountOnCart = order?.Amount ?? 0;
			}

			return View(sparePart);
		}

		public async Task<IActionResult> DetailsList(string name)
		{
			ViewBag.CategoryId = await unitOfWork.Category.GetAll();
			IEnumerable<SparePart>? spareParts = (await unitOfWork.SparePart.GetAll())?
				.Where(sp => sp.Name.Contains(name, StringComparison.InvariantCultureIgnoreCase));
			return spareParts == null ? NotFound() : View(spareParts);
		}

		public async Task<IActionResult> AddToCart(int amount, SparePart sparePart)
		{
			if (!authenticator.Authenticate(UserTypes.Client))
			{
				return RedirectToAction("Login", "Client");
			}

			sparePart = (await unitOfWork.SparePart.GetById(sparePart.Id))!;

			if (amount > sparePart.Stock || amount <= 0)
			{
				TempData["Error"] = "Invalid amount.";
				return RedirectToAction(nameof(Details), new { id = sparePart.Id });
			}

			int? clientId = HttpContext.Session.GetInt32("ClientId");
			if (clientId == null)
			{
				return RedirectToAction("Login", "Client");
			}

			var result = await unitOfWork.PurchaseOrder.AddProduct((int)clientId, new Order()
			{
				SparePartId = sparePart.Id,
				Amount = amount
			});
			
			if (result) return RedirectToAction("CartInfo", "PurchaseOrder");
			
			TempData["Error"] = "Invalid amount.";
			return RedirectToAction(nameof(Details), new { id = sparePart.Id });
		}

		public async Task<IActionResult> Create()
		{
			if (!authenticator.Authenticate(UserTypes.Admin))
			{
				return RedirectToAction("Login", "Client");
			}

			ViewData["CategoryId"] = new SelectList(await unitOfWork.Category.GetAll(), "Id", "Name");
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(
			[Bind("Id,Name,Description,Stock,Price,Image,CategoryId")] SparePart sparePart)
		{
			if (!authenticator.Authenticate(UserTypes.Admin))
			{
				return RedirectToAction("Login", "Client");
			}

			IFormFile file = HttpContext.Request.Form.Files[0];

			string fileName = Guid.NewGuid().ToString();
			string imagePath = $@"\images\{fileName}{Path.GetExtension(file.FileName)}";
			string fullPath = webHostEnvironment.WebRootPath + imagePath;

			sparePart.Image = imagePath;

			ModelState.ClearValidationState("Image");
			if (!TryValidateModel(sparePart, "Image"))
			{
				ViewData["CategoryId"] = new SelectList(await unitOfWork.Category.GetAll(), "Id", "Name");
				return View(sparePart);
			}

			using (FileStream fileStream = new(fullPath, FileMode.Create))
			{
				file.CopyTo(fileStream);
			}

			await unitOfWork.SparePart.Create(sparePart);

			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Edit(int id)
		{
			if (!authenticator.Authenticate(UserTypes.Admin))
			{
				return RedirectToAction("Login", "Client");
			}

			ViewData["CategoryId"] = new SelectList(await unitOfWork.Category.GetAll(), "Id", "Name");
			var sparePart = await unitOfWork.SparePart.GetById(id);
			if (sparePart == null)
			{
				return NotFound();
			}

			return View(sparePart);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id,
			[Bind("Id,Name,Description,Stock,Price,CategoryId")] SparePart sparePart)
		{
			if (!authenticator.Authenticate(UserTypes.Admin))
			{
				return RedirectToAction("Login", "Client");
			}

			if (id != sparePart.Id)
			{
				return NotFound();
			}

			IFormFileCollection files = HttpContext.Request.Form.Files;
			if (files.Count > 0)
			{
				IFormFile file = files[0];

				string fileName = Guid.NewGuid().ToString();
				string imagePath = $@"\images\{fileName}{Path.GetExtension(file.FileName)}";
				string fullPath = webHostEnvironment.WebRootPath + imagePath;

				sparePart.Image = imagePath;

				ModelState.ClearValidationState("Image");
				if (!TryValidateModel(sparePart, "Image"))
				{
					ViewData["CategoryId"] = new SelectList(await unitOfWork.Category.GetAll(), "Id", "Name");
					return View(sparePart);
				}

				await using FileStream fileStream = new(fullPath, FileMode.Create);
				await file.CopyToAsync(fileStream);
			}
			else
			{
				sparePart.Image = (await unitOfWork.SparePart.GetById(id))!.Image;

				ModelState.ClearValidationState("Image");
				if (!TryValidateModel(sparePart, "Image"))
				{
					ViewData["CategoryId"] = new SelectList(await unitOfWork.Category.GetAll(), "Id", "Name");
					return View(sparePart);
				}
			}

			await unitOfWork.SparePart.Update(sparePart);
			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Delete(int id)
		{
			if (!authenticator.Authenticate(UserTypes.Admin))
			{
				return RedirectToAction("Login", "Client");
			}

			await unitOfWork.SparePart.Delete(id);
			return RedirectToAction(nameof(Index));
		}
	}
}