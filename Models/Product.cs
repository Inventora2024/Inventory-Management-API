﻿using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Inventory_Management_API.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        public int StockQuantity { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public ProductCategory ProductCategory { get; set; }
        public ICollection<CustomerOrderItem> CustomerOrderItems { get; set; } // Updated to ICollection
        public ICollection<StockOrderItem> StockOrderItems { get; set; } // Updated to ICollection
        public ICollection<SupplierProduct> SupplierProducts { get; set; }
    }
}
