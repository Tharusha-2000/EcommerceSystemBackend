using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Ecommerce.productManage.Domain.Models.Domains
{
    public class ProductSize
    {
        [Key]
        public int ProductsizeId { get; set; }

        [Required]
        public string Size { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public int Qty { get; set; }

        [JsonIgnore]
        public int ProductId { get; set; }

        [JsonIgnore]
        public Product? Product { get; set; }
    }
}
