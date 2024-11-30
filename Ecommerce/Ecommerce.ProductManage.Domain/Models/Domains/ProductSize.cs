using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Ecommerce.ProductManage.Domain.Models.Domains
{
    public class ProductSize
    {
        [Key]
        public int ProductsizeId { get; set; }

        [Required]
        public string Size { get; set; }

        [Required]
        public double Price { get; set; }

        [JsonIgnore]
        public int ProductId { get; set; }

        [JsonIgnore]
        public Product? Product { get; set; }
    }
}
