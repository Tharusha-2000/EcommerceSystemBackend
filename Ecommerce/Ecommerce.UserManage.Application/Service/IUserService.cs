using Ecommerce.userManage.Domain.Models;
using Ecommerce.userManage.Domain.Models.DTO;
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

        public List<UserModel> getUserById(int Id);

        public void updateUser(UserModel userModel);
        public List<UserModel> getUserByEmail(string email);

        Task<List<UserDto>> GetUsersByIdsAsync(List<int> userIds);

        public void deleteUser(int Id);

        public List<UserModel> getAllUsers();

    }
}