using System.ComponentModel.DataAnnotations;


namespace Ecommerce.OrderProcessing.Domain.Models
{
    public class Cart
    {
        [Key]
        public int cartId { get; set; }
        public int userId { get; set; }
        public int productId { get; set; }
        public string pizzaSize { get; set; }
        public int count { get; set; }
        public string productImg { get; set; }
        public string productName { get; set; }
        public double unitPrice { get; set; }
    }
}
