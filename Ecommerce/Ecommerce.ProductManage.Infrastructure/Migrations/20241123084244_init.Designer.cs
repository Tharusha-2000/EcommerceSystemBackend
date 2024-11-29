﻿// <auto-generated />
using Ecommerce.ProductManage.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Ecommerce.ProductManage.Infrastructure.Migrations
{
    [DbContext(typeof(AppDBContext))]
    [Migration("20241123084244_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Ecommerce.ProductManage.Domain.Models.Domains.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<string>("Categories")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            Categories = "[\"Chicken\",\"Pizza\"]",
                            Description = "Delicious chicken pizza with fresh ingredients",
                            ImageUrl = "http://example.com/pizza.jpg",
                            IsAvailable = true,
                            Name = "Chicken Pizza"
                        });
                });

            modelBuilder.Entity("Ecommerce.ProductManage.Domain.Models.Domains.ProductSize", b =>
                {
                    b.Property<int>("ProductsizeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductsizeId"));

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("Size")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductsizeId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductSizes");

                    b.HasData(
                        new
                        {
                            ProductsizeId = 1,
                            Price = 950.0,
                            ProductId = 1,
                            Size = "M"
                        });
                });

            modelBuilder.Entity("Ecommerce.ProductManage.Domain.Models.Domains.ProductSize", b =>
                {
                    b.HasOne("Ecommerce.ProductManage.Domain.Models.Domains.Product", "Product")
                        .WithMany("Sizes")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Ecommerce.ProductManage.Domain.Models.Domains.Product", b =>
                {
                    b.Navigation("Sizes");
                });
#pragma warning restore 612, 618
        }
    }
}
