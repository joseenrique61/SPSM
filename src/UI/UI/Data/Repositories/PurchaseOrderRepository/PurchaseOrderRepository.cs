using UI.Data.Adapters;
using UI.Data.ApiClient;
using UI.Data.Repositories.ClientRepository;
using UI.Data.Repositories.SparePartRepository;
using UI.Models;
using UI.Models.DTOs;

namespace UI.Data.Repositories.PurchaseOrderRepository
{
	public class PurchaseOrderRepository(IApiClient client, ISparePartRepository sparePartRepository, IClientRepository clientRepository) : IPurchaseOrderRepository
	{
		private async Task<PurchaseOrder> ComposePurchaseOrder(PurchaseOrderDTO dto)
		{
			var purchaseOrder = dto.ToPurchaseOrder();

			purchaseOrder.Client = await clientRepository.GetById(purchaseOrder.ClientId);

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

		public async Task<PurchaseOrder> GetCurrentByClientId(int id)
		{
			HttpResponseMessage response = await client.Get($"shopping_cart/userId/{id}");
			return await ComposePurchaseOrder((await response.Content.ReadFromJsonAsync<PurchaseOrderDTO>())!);
		}

		public async Task<bool> AddProduct(int userId, Order order)
		{
			return (await client.Post($"shopping_cart/add_item/{userId}", new OrderDTO()
			{
				Id = order.SparePartId,
				Amount = order.Amount
			})).IsSuccessStatusCode;
		}

		public Task<bool> RemoveProduct(int userId, Order order)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteProduct(int userId, int productId)
		{
			throw new NotImplementedException();
		}
	}
}