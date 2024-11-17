using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.userManage.Domain.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
       
        public string UserType { get; set; }  // "Admin" or "Customer"
        public string PhoneNo { get; set; }
        
        public string Address { get; set; }
    }
}
