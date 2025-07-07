using System.ComponentModel.DataAnnotations;

namespace InventoryService.Application.DTOs
{
    public class CategoryDTO
    {
        [Required]
        [StringLength(100)]
        public required string Name { get; set; }
    }
}
