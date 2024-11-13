using System;
using System.Collections.Generic;

namespace Inventory_Management_API.DTOs
{
    public class CreateOrderDTO
    {
        public DateTime OrderDate { get; set; }
        public List<CreateOrderItemDTO> OrderItems { get; set; }
    }

    public class CreateOrderItemDTO
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
