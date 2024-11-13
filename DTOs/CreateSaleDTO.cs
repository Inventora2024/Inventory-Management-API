namespace Inventory_Management_API.DTOs
{
    public class CreateSaleDTO
    {
        public DateTime OrderDate { get; set; }
        public List<CreateSaleItemDTO> SaleItems { get; set; }
    }

    public class CreateSaleItemDTO
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}