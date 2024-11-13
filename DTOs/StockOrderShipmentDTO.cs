namespace Inventory_Management_API.DTOs
{
    public class StockOrderShipmentDTO
    {
        public int ShipmentId { get; set; }
        public int StockOrderId { get; set; }
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
