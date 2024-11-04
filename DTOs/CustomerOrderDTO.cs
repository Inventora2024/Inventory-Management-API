using System.ComponentModel.DataAnnotations;

namespace Inventory_Management_API.DTOs
{
    public class CustomerOrderDTO
    {
        [Key]
        public int CustomerOrderId { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
    }
}
