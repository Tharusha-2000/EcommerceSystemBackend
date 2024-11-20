
using Ecommerce.ProductManage.Domain.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.ProductManage.Infrastructure
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
           .HasMany(p => p.Sizes)
           .WithOne(s => s.Product)
           .HasForeignKey(s => s.ProductId);

            var sizes = new ProductSize()
            {
                ProductsizeId = 1,
                ProductId = 1,
                Size = "M",
                Price = 950,
                Qty = 10,
            };

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 1,
                Name = "Chicken Pizza",
                Description = "Delicious chicken pizza with fresh ingredients",
                ImageUrl = "http://example.com/pizza.jpg",
                Categories = new List<string> { "Chicken", "Pizza" },
                IsAvailable = true
            });

            modelBuilder.Entity<ProductSize>().HasData(sizes);

        }
    }
}
