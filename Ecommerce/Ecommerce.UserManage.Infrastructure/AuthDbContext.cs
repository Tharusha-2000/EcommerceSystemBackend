using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.userManage.Infrastructure
{
    public class AuthDbContext:IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            var adminRole = "797654bb-8e4f-4ae3-ac66-fbdc1c882291";
            var customerRole = "0b7e7c7d-834b-473c-9072-19bbb3b3e892";
           


            var roles = new List<IdentityRole>
            {
                new IdentityRole{
                    Id=adminRole,
                    ConcurrencyStamp=adminRole,
                    Name="admin",
                    NormalizedName="ADMIN".ToUpper()
                },
                new IdentityRole
                {
                    Id=customerRole,
                    ConcurrencyStamp=customerRole,
                    Name="customer",
                    NormalizedName="CUSTOMER".ToUpper()
                },
               

            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
