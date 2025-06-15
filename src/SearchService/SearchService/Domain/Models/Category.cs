using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace SearchService.Domain.Models;

[Index(nameof(Name), IsUnique = true)]
public class Category
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(15)]
    public required string Name { get; set; }

    [JsonIgnore]
    public ICollection<Product> Products { get; set; } = [];
}