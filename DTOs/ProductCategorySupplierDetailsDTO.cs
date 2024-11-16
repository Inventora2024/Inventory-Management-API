using System.Collections.Generic;

namespace Inventory_Management_API.DTOs
{
    public class ProductCategorySupplierDetailsDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int StockQuantity { get; set; }
        public string Category { get; set; }
        public string Nature { get; set; }
        public List<string> Suppliers { get; set; }
    }
}
