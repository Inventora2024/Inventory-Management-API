using System.ComponentModel.DataAnnotations;

namespace Inventory_Management_API.DTOs
{
    public class SupplierDTO
    {
        [Key]
        public int SupplierId { get; set; }
        [Required]
        public string Company { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string SpocEmail { get; set; }
        [Required]
        public string SpocContact { get; set; }
    }
}
