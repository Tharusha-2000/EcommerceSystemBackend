using Ecommerce.productManage.Domain.Models.Domains;

namespace Ecommerce.productManage.Domain.Models.DTOs
{
    public class ProductSizeDto
    {
        public string Size { get; set; }
    
        public double Price { get; set; }
        
        public int Qty { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
