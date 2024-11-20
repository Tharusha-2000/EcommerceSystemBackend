
using Ecommerce.ProductManage.Domain.Models.Domains;
using Ecommerce.ProductManage.Domain.Models.DTOs;

namespace Ecommerce.ProductManage.Application.Services
{
    public interface IProductService
    {
        public Task<List<Product>> GetAllProductsAsync();
        public Task<List<Product>> GetProductsByCategoryAsync(string category);
        public Task<Product?> GetProductByIdAsync(int productId);
        public Task<Product?> CreateProductAsync(ProductDto product);
        public Task<Product?> UpdateProductAsync(int productId, ProductDto product);
    }
}
