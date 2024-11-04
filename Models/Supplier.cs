using System.ComponentModel.DataAnnotations;

namespace Inventory_Management_API.Models
{
    public class Supplier
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

        public ICollection<SupplierProduct> SupplierProducts { get; set; } = new List<SupplierProduct>();
    }
}
