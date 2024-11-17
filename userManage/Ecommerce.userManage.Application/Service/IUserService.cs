using Ecommerce.userManage.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.userManage.Application.Service
{
    public interface IUserService
    {
        public void addUser(UserModel userModel);
    }
}
