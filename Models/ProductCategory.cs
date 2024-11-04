using System.ComponentModel.DataAnnotations;

namespace Inventory_Management_API.Models
{
    public class ProductCategory
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Nature { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
