using System.ComponentModel.DataAnnotations;

namespace Inventory_Management_API.Models
{
    public class StockOrderItem
    {
        [Key]
        public int StockOrderItemId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int StockOrderId { get; set; }

        public StockOrder StockOrder { get; set; }
        public Product Product { get; set; }
    }
}
