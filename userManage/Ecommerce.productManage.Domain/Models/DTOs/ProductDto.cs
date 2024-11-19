using Ecommerce.productManage.Domain.Models.Domains;

namespace Ecommerce.productManage.Domain.Models.DTOs
{
    public class ProductDto
    {
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public string ImageUrl { get; set; }
        
        public List<string> Categories { get; set; }
        
        public bool IsAvailable { get; set; }

        public ICollection<ProductSize> Sizes { get; set; } = new List<ProductSize>();
    }
}
