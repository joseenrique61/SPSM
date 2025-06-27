using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UserService.Domain.Models;

[Index(nameof(Email), IsUnique = true)]
public class User
{
	[Key]
	public int Id { get; set; }

	[Required]
	[DataType(DataType.EmailAddress)]
	[MaxLength(100)]
	public required string Email { get; set; }

	[Required]
	[DataType(DataType.Password)]
	[NotMapped]
	public required string? Password { get; set; }

	public string? PasswordHash { get; set; }
}