using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SearchService.Domain.Models;

public class Product
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public required string Name { get; set; }

    [Required, MaxLength(500)]
    public required string Description { get; set; }

    [Required, ForeignKey(nameof(Category))]
    public required int CategoryId { get; set; }

    public Category? Category { get; set; }

    public required double Price { get; set; }
    
    public required int Stock { get; set; }
    
    public bool IsDeleted { get; set; }

    public string? ImagePath { get; set; }
}