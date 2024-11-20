using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.ReviewAndRating.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.ReviewAndRating.Infrastructure
{
    public class ReviewAndRatingDbContext : DbContext
    {
       public DbSet<Feedback> Feedback { get; set; }
        public DbSet<FeedbackWithProduct> FeedbackWithProduct { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderProduct> OrderProduct { get; set; }


        public ReviewAndRatingDbContext(DbContextOptions<ReviewAndRatingDbContext> options) : base(options)
        {

        }

        public ReviewAndRatingDbContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "server = DILSHAN; database = EcommerceDemo; Integrated Security = True; MultipleActiveResultSets = true; TrustServerCertificate = True;";
            optionsBuilder.UseSqlServer(connectionString);
        }


    }
}
