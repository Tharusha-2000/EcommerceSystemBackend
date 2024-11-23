using Ecommerce.userManage.Domain.Models;
using Ecommerce.userManage.Domain.Models.DTO;
using Ecommerce.userManage.Infrastructure;
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<UserDto>> GetUsersByIdsAsync(List<int> userIds)
        {
            if (userIds == null || !userIds.Any())
                throw new ArgumentException("User IDs cannot be null or empty.");

            var users = await _context.Users
                .Where(u => userIds.Contains(u.Id))
                .ToListAsync();

            // Manual mapping to UserDto
            var userDtos = users.Select(user => new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                
            }).ToList();

            return userDtos;
        }

    }
}


