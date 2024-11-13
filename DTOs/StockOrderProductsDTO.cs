namespace Inventory_Management_API.DTOs
{
    public class StockOrderProductsDTO
    {
        public int StockOrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public List<StockOrderProductItemDTO> StockOrderItems { get; set; } = new List<StockOrderProductItemDTO>();
    }

    public class StockOrderProductItemDTO
    {
        public int StockOrderItemId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; }
    }
}
