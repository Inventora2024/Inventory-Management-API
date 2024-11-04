using System.ComponentModel.DataAnnotations;

namespace Inventory_Management_API.Models
{
    public class CustomerOrder
    {
        [Key]
        public int CustomerOrderId { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        public ICollection<CustomerOrderItem> CustomerOrderItems { get; set; } = new List<CustomerOrderItem>();
    }
}
