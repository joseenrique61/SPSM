using System.ComponentModel.DataAnnotations;

namespace UI.Models
{
	public class Category
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(50)]
		public string Name { get; set; }
	}
}
