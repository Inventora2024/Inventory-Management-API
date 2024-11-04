using System.ComponentModel.DataAnnotations;

namespace Inventory_Management_API.DTOs
{
    public class ShipmentDTO
    {
        [Key]
        public int ShipmentId { get; set; }
        [Required]
        public DateTime LastUpdated { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public int StockOrderId { get; set; }
    }
}
