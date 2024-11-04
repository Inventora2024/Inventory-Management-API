using System.ComponentModel.DataAnnotations;

namespace Inventory_Management_API.DTOs
{
    public class ProductCategoryDTO
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Nature { get; set; }
    }
}
