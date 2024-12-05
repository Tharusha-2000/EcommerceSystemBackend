using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.userManage.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Ecommerce.userManage.Infrastructure
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options)
       : base(options)
        {
        }

        public DbSet<UserModel> Users { get; set; }

        
    }
}
