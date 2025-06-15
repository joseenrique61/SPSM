using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DataSeeder.Models.Common;

[Index(nameof(Name), IsUnique = true)]
public class Category
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(40)]
    public required string Name { get; set; }

    public ICollection<Product> Products { get; set; } = [];
}