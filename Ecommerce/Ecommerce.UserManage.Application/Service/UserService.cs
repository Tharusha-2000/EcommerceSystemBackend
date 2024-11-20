using Ecommerce.userManage.Domain.Models;
using Ecommerce.userManage.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.userManage.Application.Service
{

    public class UserService:IUserService
    {
        private readonly UserDbContext _context;

        public UserService (UserDbContext context)
        {
            _context = context;
        }

        public void addUser(UserModel userModel)
        {
            var userData = new UserModel
            {
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Email = userModel.Email,
                UserType = userModel.UserType,
                PhoneNo = userModel.PhoneNo,
                Address = userModel.Address

            };
            _context.Users.Add(userData);
            _context.SaveChanges();
        }
    }
}


