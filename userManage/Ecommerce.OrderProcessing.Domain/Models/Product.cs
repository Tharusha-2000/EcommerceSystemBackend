using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.OrderProcessing.Domain.Models
{
    public class Product
    {
        public int productId { get; set; }
        public string productName { get; set; }
        public string description { get; set; }
        public string image { get; set; }
    }
}
