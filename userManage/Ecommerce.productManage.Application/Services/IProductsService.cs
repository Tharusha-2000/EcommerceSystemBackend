using Ecommerce.productManage.Domain.Models.Domains;
using Ecommerce.productManage.Domain.Models.DTOs;

namespace Ecommerce.productManage.Application.Services
{
    public interface IProductsService
    {
        public Task<List<Product>> GetAllProductsAsync();
        public Task<List<Product>> GetProductsByCategoryAsync(string category);
        public Task<Product?> GetProductByIdAsync(int productId);
        public Task<Product?> CreateProductAsync(ProductDto product);
        public Task<Product?> UpdateProductAsync(int productId, ProductDto product);
    }
}
