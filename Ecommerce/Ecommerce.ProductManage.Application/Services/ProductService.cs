﻿using Ecommerce.ProductManage.Domain.Models.Domains;
using Ecommerce.ProductManage.Domain.Models.DTOs;
using Ecommerce.ProductManage.Infrastructure;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Ecommerce.ProductManage.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDBContext _context;

        public ProductService(AppDBContext context)
        {
            _context = context;
        }

        public async Task<Product?> CreateProductAsync(ProductDto productDto)
        {
            var product = new Product()
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Categories = productDto.Categories,
                ImageUrl = productDto.ImageUrl,
                IsAvailable = productDto.IsAvailable,
                Sizes = productDto.Sizes.Select(s => new ProductSize
                {
                    Size = s.Size,
                    Price = s.Price
                }).ToList(),
            };

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<List<Product>> GetAllProductsAsync(int? minPrice, int? maxPrice, List<string>? categories)
        {
            var products = _context.Products
                .AsNoTracking()
                .Include(p => p.Sizes)
                .AsQueryable();

            if (minPrice.HasValue)
            {
                products = products.Where(p => p.Sizes.Any(s => s.Size == "S" && s.Price >= minPrice));
            }

            if (maxPrice.HasValue)
            {
                products = products.Where(p => p.Sizes.Any(s => s.Size == "S" && s.Price <= maxPrice));
            }

            // Filter by categories
            if (categories != null && categories.Any())
            {
                products = products.Where(p => p.Categories.Any(c => categories.Contains(c)));
            }

            return await products.ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int productId)
        {
            var product = await _context.Products
                .Include(p => p.Sizes)
                .FirstOrDefaultAsync(p => p.ProductId == productId);
            return product;
        }

        public async Task<Product?> UpdateProductAsync(int productId, ProductDto productDto)
        {
            var existingProduct = await _context.Products
                .Include(p => p.Sizes)
                .FirstOrDefaultAsync(p => p.ProductId == productId);

            if (existingProduct == null)
            {
                return null;
            }

            existingProduct.Name = productDto.Name;
            existingProduct.Description = productDto.Description;
            existingProduct.Categories = productDto.Categories;
            existingProduct.ImageUrl = productDto.ImageUrl;
            existingProduct.IsAvailable = productDto.IsAvailable;
            _context.ProductSizes.RemoveRange(existingProduct.Sizes);
            existingProduct.Sizes = productDto.Sizes.Select(s => new ProductSize
            {
                Price = s.Price,
                Size = s.Size
            }).ToList();

            await _context.SaveChangesAsync();

            return existingProduct;

        }

        public async Task<List<Product>> GetProductsByCategoryAsync(string category)
        {
            var productsByCategory = await _context.Products
                .Where(p => p.Categories.Contains(category))
                .Include(p => p.Sizes)
                .ToListAsync();

            return productsByCategory;
        }

        public async Task<Product?> DeleteProductAsync(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) {
                return null;
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return product;
        }
    }
}
