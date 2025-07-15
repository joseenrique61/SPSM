using UI.Models;

namespace UI.Data.Repositories.PurchaseOrderRepository
{
	public interface IPurchaseOrderRepository
	{
		public Task<List<PurchaseOrder>?> GetAll();

		public Task<PurchaseOrder> GetCurrentByClientId(int id);

		public Task<List<PurchaseOrder>?> GetByClientId(int id);

		public Task<bool> AddProduct(int userId, Order order);
		
		public Task<bool> RemoveProduct(int userId, Order order);
		
		public Task<bool> DeleteProduct(int userId, int productId);
	}
}