using System.ComponentModel.DataAnnotations;

namespace Inventory_Management_API.Models
{
    public class CustomerOrderItem
    {
        [Key]
        public int CustomerOrderItemId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int CustomerOrderId { get; set; }

        public CustomerOrder CustomerOrder { get; set; }
        public Product Product { get; set; }

    }
}
