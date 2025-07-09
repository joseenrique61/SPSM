using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UI.Models
{
	public class User
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[NotMapped]
		public string? Password { get; set; }

		public string? PasswordHash { get; set; }
	}
}