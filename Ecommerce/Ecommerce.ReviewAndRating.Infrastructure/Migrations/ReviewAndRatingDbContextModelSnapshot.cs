﻿// <auto-generated />
using Ecommerce.ReviewAndRating.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Ecommerce.ReviewAndRating.Infrastructure.Migrations
{
    [DbContext(typeof(ReviewAndRatingDbContext))]
    partial class ReviewAndRatingDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Ecommerce.ReviewAndRating.Domain.Models.Feedback", b =>
                {
                    b.Property<int>("FeedbackId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FeedbackId"));

                    b.Property<string>("FeedbackMessage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GivenDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<double>("Rate")
                        .HasColumnType("float");

                    b.HasKey("FeedbackId");

                    b.ToTable("Feedback");
                });

            modelBuilder.Entity("Ecommerce.ReviewAndRating.Domain.Models.FeedbackWithProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FeedbackId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FeedbackId");

                    b.ToTable("FeedbackWithProduct");
                });

            modelBuilder.Entity("Ecommerce.ReviewAndRating.Domain.Models.FeedbackWithProduct", b =>
                {
                    b.HasOne("Ecommerce.ReviewAndRating.Domain.Models.Feedback", "Feedback")
                        .WithMany("FeedbackWithProducts")
                        .HasForeignKey("FeedbackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Feedback");
                });

            modelBuilder.Entity("Ecommerce.ReviewAndRating.Domain.Models.Feedback", b =>
                {
                    b.Navigation("FeedbackWithProducts");
                });
#pragma warning restore 612, 618
        }
    }
}