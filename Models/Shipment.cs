using System.ComponentModel.DataAnnotations;

namespace Inventory_Management_API.Models
{
    public class Shipment
    {
        [Key]
        public int ShipmentId { get; set; }

        [Required]
        public DateTime LastUpdated { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public int StockOrderId { get; set; }

        public StockOrder StockOrder { get; set; }
    }
}
