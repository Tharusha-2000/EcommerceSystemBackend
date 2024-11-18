﻿using System;
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


        public ReviewAndRatingDbContext(DbContextOptions<ReviewAndRatingDbContext> options) : base(options)
        {

        }

        public ReviewAndRatingDbContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "Server=tcp:pizzaslicedb.database.windows.net,1433;Initial Catalog=PizzaSliceDB;Encrypt=True;User ID=pizzaSliceDB;Password=EAD123#aa;MultipleActiveResultSets=False;TrustServerCertificate=False;";
            optionsBuilder.UseSqlServer(connectionString);
        }


    }
}