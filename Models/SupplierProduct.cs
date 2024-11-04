namespace Inventory_Management_API.Models
{
    public class SupplierProduct
    {
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
