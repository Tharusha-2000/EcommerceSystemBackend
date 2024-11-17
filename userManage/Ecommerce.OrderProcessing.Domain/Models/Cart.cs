using Ecommerce.userManage.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.OrderProcessing.Domain.Models
{
    public class Cart
    {
        [Key]
        public int cartId { get; set; }
        
        [ForeignKey("Id")]
        public int userId { get; set; }
        public int productId { get; set; }
        public string pizzaSize { get; set; }
        public int count { get; set; }

        public UserModel? UserModel { get; set;}
    }
}
