using UI.Data.Adapters;
using UI.Data.ApiClient;
using UI.Data.Repositories.SparePartRepository;
using UI.Models;
using UI.Models.DTOs;

namespace UI.Data.Repositories.PurchaseOrderRepository
{
	public class PurchaseOrderRepository(IApiClient client, ISparePartRepository sparePartRepository) : IPurchaseOrderRepository
	{
		private async Task<PurchaseOrder> ComposePurchaseOrder(PurchaseOrderDTO dto)
		{
			var purchaseOrder = dto.ToPurchaseOrder();

			foreach (var order in purchaseOrder.Orders)
			{
				order.SparePart = await sparePartRepository.GetById(order.SparePartId);
			}
			
			return purchaseOrder;
		}
		
		public async Task<List<PurchaseOrder>?> GetAll()
		{
			HttpResponseMessage response = await client.Get("shopping_cart/all");
			
			if (!response.IsSuccessStatusCode) return null;
			
			var purchaseOrders = new List<PurchaseOrder>();
			foreach (var purchaseOrder in (await response.Content.ReadFromJsonAsync<List<PurchaseOrderDTO>>())!)
			{
				purchaseOrders.Add(await ComposePurchaseOrder(purchaseOrder));
			}

			return purchaseOrders;
		}

		public async Task<List<PurchaseOrder>?> GetByClientId(int id)
		{
			throw new NotImplementedException();
			// HttpResponseMessage response = await client.Get($"shopping_cart/userId/{id}");
			// if (response.IsSuccessStatusCode)
			// {
			// 	return await response.Content.ReadFromJsonAsync<List<PurchaseOrder>>();
			// }
			// return null;
		}

		public async Task<PurchaseOrder?> GetById(int id)
		{
			throw new NotImplementedException();
			// HttpResponseMessage response = await client.Get($"shopping_cart/id/{id}");
			// if (response.IsSuccessStatusCode)
			// {
			// 	return await response.Content.ReadFromJsonAsync<PurchaseOrder>();
			// }
			// return null;
		}

		public async Task<PurchaseOrder> GetCurrentByClientId(int id)
		{
			HttpResponseMessage response = await client.Get($"shopping_cart/userId/{id}");
			return await ComposePurchaseOrder((await response.Content.ReadFromJsonAsync<PurchaseOrderDTO>())!);
		}

		public async Task<bool> Create(PurchaseOrder purchaseOrder)
		{
			throw new NotImplementedException();
			// HttpResponseMessage response = await client.Post("create", purchaseOrder);
			// return response.IsSuccessStatusCode;
		}

		public async Task<bool> Update(PurchaseOrder purchaseOrder)
		{
			throw new NotImplementedException();
			// HttpResponseMessage response = await client.Put("update", purchaseOrder);
			// return response.IsSuccessStatusCode;
		}
	}
}