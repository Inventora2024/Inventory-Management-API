﻿namespace Inventory_Management_API.DTOs
{
    public class ProductWithCategoryDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int StockQuantity { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public string Nature { get; set; }
    }
}
