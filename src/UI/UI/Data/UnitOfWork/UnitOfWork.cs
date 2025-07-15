using UI.Data.Repositories.CategoryRepository;
using UI.Data.Repositories.ClientRepository;
using UI.Data.Repositories.PurchaseOrderRepository;
using UI.Data.Repositories.SparePartRepository;

namespace UI.Data.UnitOfWork
{
    public class UnitOfWork(
	    ISparePartRepository sparePart,
	    ICategoryRepository category,
	    IPurchaseOrderRepository purchaseOrder,
	    IClientRepository client)
	    : IUnitOfWork
    {
		public ISparePartRepository SparePart { get; } = sparePart;

		public ICategoryRepository Category { get; } = category;

		public IPurchaseOrderRepository PurchaseOrder { get; } = purchaseOrder;

		public IClientRepository Client { get; } = client;
	}
}