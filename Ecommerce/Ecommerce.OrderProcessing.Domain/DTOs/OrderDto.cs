using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.OrderProcessing.Domain.DTOs
{
    public class OrderDto
    {
        public int orderId { get; set; }
        public int userId { get; set; }
    }
}
