using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UI.Models
{
	public class Order
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[ForeignKey(nameof(PurchaseOrder))]
		public int PurchaseOrderId {  get; set; }

		public PurchaseOrder? PurchaseOrder { get; set; }

		[Required]
		[ForeignKey(nameof(SparePart))]
		public int SparePartId { get; set; }
		
		public SparePart? SparePart {  get; set; }

		[Required]
		public int Amount { get; set; }
	}
}