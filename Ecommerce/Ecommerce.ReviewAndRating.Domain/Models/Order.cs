/*
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
//using Ecommerce.userManage.Domain.Models;

namespace Ecommerce.ReviewAndRating.Domain.Models
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
        //public UserModel? UserModel { get; set; } // Navigation property for UserModel
        //[JsonIgnore]
        public ICollection<OrderProduct>? OrderProduct { get; set; } // Navigation property for OrderProduct

        [JsonIgnore]
        public ICollection<Feedback>? Feedback { get; set; } // Navigation property for Feedback
    }
}
*/