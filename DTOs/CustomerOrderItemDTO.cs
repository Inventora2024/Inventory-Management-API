using System.ComponentModel.DataAnnotations;

namespace Inventory_Management_API.DTOs
{
    public class CustomerOrderItemDTO
    {
        [Key]
        public int CustomerOrderItemId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int CustomerOrderId { get; set; }
    }
}
