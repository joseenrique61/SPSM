using UI.Data.ApiClient;
using UI.Models;

namespace UI.Data.Repositories.PurchaseOrderRepository
{
	public class PurchaseOrderRepository(IApiClient client) : IPurchaseOrderRepository
	{
		public async Task<List<PurchaseOrder>?> GetAll()
		{
			HttpResponseMessage response = await client.Get("shopping_cart/all");
			if (response.IsSuccessStatusCode)
			{
				return await response.Content.ReadFromJsonAsync<List<PurchaseOrder>>();
			}
			return null;
		}

		public async Task<List<PurchaseOrder>?> GetByClientId(int id)
		{
			HttpResponseMessage response = await client.Get($"shopping_cart/userId/{id}");
			if (response.IsSuccessStatusCode)
			{
				return await response.Content.ReadFromJsonAsync<List<PurchaseOrder>>();
			}
			return null;
		}

		public async Task<PurchaseOrder?> GetById(int id)
		{
			HttpResponseMessage response = await client.Get($"shopping_cart/id/{id}");
			if (response.IsSuccessStatusCode)
			{
				return await response.Content.ReadFromJsonAsync<PurchaseOrder>();
			}
			return null;
		}

		public async Task<PurchaseOrder> GetCurrentByClientId(int id)
		{
			HttpResponseMessage response = await client.Get($"current/{id}");
			return (await response.Content.ReadFromJsonAsync<PurchaseOrder>())!;
		}

		public async Task<bool> Create(PurchaseOrder purchaseOrder)
		{
			HttpResponseMessage response = await client.Post("create", purchaseOrder);
			return response.IsSuccessStatusCode;
		}

		public async Task<bool> Update(PurchaseOrder purchaseOrder)
		{
			HttpResponseMessage response = await client.Put("update", purchaseOrder);
			return response.IsSuccessStatusCode;
		}
	}
}