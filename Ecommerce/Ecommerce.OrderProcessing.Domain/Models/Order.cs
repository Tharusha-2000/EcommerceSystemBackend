using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Ecommerce.OrderProcessing.Domain.Models
{
    public class Order
    {
        [Key]
        public int orderId { get; set; }
        public int userId { get; set; }
        public float totalPrice { get; set; }
        public string paymentMethod { get; set; }
        public string type { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string country { get; set; }
        public int postalcode { get; set; }
        public string phoneNum { get; set; }
        public bool paymentStatus { get; set; }
        public string OrderStatus { get; set; }
        public string Date { get; set; } 
    }
}
