using Ecommerce.userManage.Domain.Models;
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

        [ForeignKey("Id")]
        public int userId { get; set; }
        public float totalPrice { get; set; }
        public string paymentMethod { get; set; }
        public string Address { get; set; }
        public int postalcode { get; set; }
        public string phoneNum { get; set; }
        public bool paymentStatus { get; set; }
        public string OrderStatus { get; set; }
        [JsonIgnore]
        public UserModel? UserModel { get; set; }
    }
}
