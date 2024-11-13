namespace Inventory_Management_API.DTOs
{
    public class CustomerOrderProductsDTO
    {
        public int CustomerOrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public List<CustomerOrderProductItemDTO> CustomerOrderItems { get; set; } = new List<CustomerOrderProductItemDTO>();
    }

    public class CustomerOrderProductItemDTO
    {
        public int CustomerOrderItemId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; }
    }
}
