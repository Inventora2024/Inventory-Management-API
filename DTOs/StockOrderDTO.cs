using System.ComponentModel.DataAnnotations;

namespace Inventory_Management_API.DTOs
{
    public class StockOrderDTO
    {
        [Key]
        public int StockOrderId { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
    }
}
