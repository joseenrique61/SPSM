using System.ComponentModel;
using InventoryService.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace InventoryService.Domain.Models
{
    public class Product : ISoftDelete
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public required string Name { get; set; }

        [Required, MaxLength(500)]
        public string? Description { get; set; }

        [Required, ForeignKey(nameof(Category))]
        public required int CategoryId { get; set; }
        [Required]
        public required double Price { get; set; }
        public required int Stock { get; set; }
        public Category? Category { get; set; }
        public string? ImagePath { get; set; }
        
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
    }
}
