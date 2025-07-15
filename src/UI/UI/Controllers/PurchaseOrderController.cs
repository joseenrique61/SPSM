using Microsoft.AspNetCore.Mvc;
using UI.Data.UnitOfWork;
using UI.Models;
using UI.Utilities;

namespace UI.Controllers
{
	public class PurchaseOrderController(IUnitOfWork unitOfWork, IAuthenticator authenticator) : BaseController
	{
		public async Task<IActionResult> CartInfo()
		{
			if (!authenticator.Authenticate(UserTypes.Client))
			{
				return RedirectToAction("Login", "Client");
			}

			var purchaseOrder = await unitOfWork.PurchaseOrder.GetCurrentByClientId((int)HttpContext.Session.GetInt32("ClientId")!);

			List<string> warnings = purchaseOrder.Orders
				.Where(o => o.Amount > o.SparePart!.Stock)
				.Select(o => o.SparePart!.Name)
				.ToList();
			ViewBag.Warnings = warnings;

			return View(purchaseOrder);
		}

		public async Task<IActionResult> RemoveFromCart(int sparePartId)
		{
			if (!authenticator.Authenticate(UserTypes.Client))
			{
				return RedirectToAction("Login", "Client");
			}
			
			int? clientId = HttpContext.Session.GetInt32("ClientId");
			if (clientId == null)
			{
				return RedirectToAction("Login", "Client");
			}
			await unitOfWork.PurchaseOrder.DeleteProduct(clientId.Value, sparePartId);	
			
			return RedirectToAction("CartInfo", "PurchaseOrder");
		}

		public async Task<IActionResult> PurchaseOrderListAdmin()
		{
			if (!authenticator.Authenticate(UserTypes.Admin))
			{
				return RedirectToAction("Login", "Client");
			}

			return View((await unitOfWork.PurchaseOrder
				.GetAll())!
				.OrderByDescending(p => p.Id));
		}

		public async Task<IActionResult> PurchaseOrderListClient()
		{
			if (!authenticator.Authenticate(UserTypes.Client))
			{
				return RedirectToAction("Login", "Client");
			}

			return View((await unitOfWork.PurchaseOrder
				.GetByClientId((int)HttpContext.Session.GetInt32("ClientId")!))!
				.OrderByDescending(p => p.Id));
		}

		public async Task<IActionResult> Buy()
		{
			if (!authenticator.Authenticate(UserTypes.Client))
			{
				return RedirectToAction("Login", "Client");
			}

			int? clientId = HttpContext.Session.GetInt32("ClientId");
			if (clientId == null)
			{
				return RedirectToAction("Login", "Client");
			}
			
			await unitOfWork.PurchaseOrder.Buy((int)clientId);
			// PurchaseOrder purchaseOrder = await unitOfWork.PurchaseOrder.GetCurrentByClientId((int)clientId);
			// purchaseOrder.PurchaseCompleted = true;
			// purchaseOrder.Client = null;
			// await unitOfWork.PurchaseOrder.Update(purchaseOrder);

			// List<SparePart> spareParts = (await unitOfWork.SparePart.GetAll());
			// foreach (SparePart sparePart in spareParts)
			// {
			// 	Order? order = purchaseOrder.Orders.FirstOrDefault(o => o.SparePartId == sparePart.Id);
			// 	if (order == null)
			// 	{
			// 		continue;
			// 	}
			//
			// 	sparePart.Stock -= order.Amount;
			// 	await unitOfWork.SparePart.Update(sparePart);
			// }

			return RedirectToAction("Index", "Home");
		}
	}
}