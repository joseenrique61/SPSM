using System.ComponentModel.DataAnnotations;

namespace InventoryService.Application.DTOs
{
    public class ProductDTO
    {
        [Required]
        [StringLength(100)]
        public required string Name { get; set; }

        [Required]
        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "The price needs to be greater than zero")]
        public double Price { get; set; }

        [Required(ErrorMessage = "The ID is mandatory")]
        public required int CategoryId { get; set; }
        public string? ImagePath { get; set; }
    }
}
