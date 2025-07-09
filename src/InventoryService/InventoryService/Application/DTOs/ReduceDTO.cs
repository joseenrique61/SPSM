using System.ComponentModel.DataAnnotations;

namespace InventoryService.Application.DTOs
{
    public class ReduceDTO
    {
        [Required]
        public required int Id { get; set; }
        [Required]
        public int Amount { get; set; }
    }
}
