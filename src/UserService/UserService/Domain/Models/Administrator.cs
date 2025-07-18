using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserService.Domain.Models;

public class Administrator
{
	[Key]
	public int Id { get; set; }

	[ForeignKey(nameof(User))]
	public int UserId { get; set; }

	public User? User { get; set; }
}