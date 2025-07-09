using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using InventoryService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Domain.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Category : ISoftDelete
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(40)]
        public required string Name { get; set; }

        [JsonIgnore]
        public ICollection<Product> Products { get; set; } = [];
        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
    }
}
