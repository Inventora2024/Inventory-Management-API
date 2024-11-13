using System.ComponentModel.DataAnnotations;

namespace Inventory_Management_API.DTOs
{
    public class StockOrderItemDTO
    {
        [Key]
        public int StockOrderItemId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public int StockOrderId { get; set; }
    }
}
