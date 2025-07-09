using SparePartsStoreWeb.Data.Repositories.CategoryRepository;
using UI.Data.Repositories.ClientRepository;
using UI.Data.Repositories.PurchaseOrderRepository;
using UI.Data.Repositories.SparePartRepository;

namespace UI.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
	{
		public ISparePartRepository SparePart { get; }

		public ICategoryRepository Category { get; }
		
		public IPurchaseOrderRepository PurchaseOrder { get; }

		public IClientRepository Client { get; }

		public UnitOfWork(ISparePartRepository sparePart, ICategoryRepository category, IPurchaseOrderRepository purchaseOrder, IClientRepository client)
		{
			SparePart = sparePart;
			Category = category;
			PurchaseOrder = purchaseOrder;
			Client = client;
		}
	}
}