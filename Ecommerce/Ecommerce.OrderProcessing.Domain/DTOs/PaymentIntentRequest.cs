using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.OrderProcessing.Domain.DTOs
{
    public class PaymentIntentRequest
    {
        public long Amount { get; set; }
        public string Currency { get; set; }
        public List<string> PaymentMethodTypes { get; set; }
    }
}
