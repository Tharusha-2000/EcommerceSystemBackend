using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Ecommerce.ReviewAndRating.Domain.Models
{
    public class OrderProduct
    {
        [Key]
        public int orderProductId { get; set; }

        [ForeignKey("orderId")]
        public int orderId { get; set; }
         
       
        public int productId { get; set; }
        public string pizzaSize { get; set; }
        public int count { get; set; }
        [JsonIgnore]
        public Order? Order { get; set; }  // Navigation property for Order

        // public Product? Product { get; set; }
    }
}
