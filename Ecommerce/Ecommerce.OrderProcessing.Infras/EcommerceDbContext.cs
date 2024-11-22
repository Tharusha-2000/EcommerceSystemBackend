using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ecommerce.OrderProcessing.Domain.Models;

namespace Ecommerce.OrderProcessing.Infras
{
    public class EcommerceDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var connectionString = "server = MSI; database = EcommerceDemo; Integrated Security = True; MultipleActiveResultSets = true; MultipleActiveResultSets = true; TrustServerCertificate = True;";
            //var connectionString = "Server=tcp:ticketmateserver.database.windows.net,1433;Initial Catalog=PTEScentralDb;Persist Security Info=False;User ID=adminPTES;Password=#ticket@MS;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;";
            //var connectionString = "Server=tcp:ptesserver.database.windows.net,1433;Initial Catalog=ptescentral;Persist Security Info=False;User ID=AdminPTES;Password=PTES@admin;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            var connectionString = "Server=tcp:pizzaslicedb.database.windows.net,1433;Initial Catalog=PizzaSliceDB;Encrypt=True;User ID=pizzaSliceDB;Password=EAD123#aa;MultipleActiveResultSets=False;TrustServerCertificate=False;";

            optionsBuilder.UseSqlServer(connectionString);
        }
    }

}
