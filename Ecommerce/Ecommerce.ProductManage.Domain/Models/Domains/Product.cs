using System.ComponentModel.DataAnnotations;

namespace Ecommerce.ProductManage.Domain.Models.Domains
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public List<string> Categories { get; set; }
        [Required]
        public bool IsAvailable { get; set; }

        public ICollection<ProductSize> Sizes { get; set; } = new List<ProductSize>();
    }
}
