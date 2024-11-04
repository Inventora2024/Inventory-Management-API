using System.ComponentModel.DataAnnotations;

namespace Inventory_Management_API.DTOs
{
    public class ProductDTO
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public int StockQuantity { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
