using System.ComponentModel.DataAnnotations;

namespace Inventory_Management_API.Models
{
    public class StockOrder
    {
        [Key]
        public int StockOrderId { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        public ICollection<StockOrderItem> StockOrderItems { get; set; } = new List<StockOrderItem>();

        public Shipment Shipment { get; set; }
    }
}
