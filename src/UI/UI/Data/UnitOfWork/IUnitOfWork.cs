using UI.Data.Repositories.CategoryRepository;
using UI.Data.Repositories.ClientRepository;
using UI.Data.Repositories.PurchaseOrderRepository;
using UI.Data.Repositories.SparePartRepository;

namespace UI.Data.UnitOfWork
{
    public interface IUnitOfWork
	{
		public ISparePartRepository SparePart { get; }

		public ICategoryRepository Category { get; }

		public IPurchaseOrderRepository PurchaseOrder { get; }

		public IClientRepository Client { get; }
	}
}