using Ecommerce.ProductManage.Application.Services;
using Ecommerce.ProductManage.Domain.Models.Domains;
using Ecommerce.ProductManage.Domain.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.ProductManage.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _ProductService;

        public ProductController(IProductService productsService)
        {
            _ProductService = productsService;
        }

      
        [HttpGet("GetAllProducts")]
        public async Task<ActionResult<List<Product>>> GetAllProductsAsync([FromQuery] int? minPrice, [FromQuery] int? maxPrice, [FromQuery] string? categories)
        {
            var categoryList = categories?.Split(',')?.ToList();
            var products = await _ProductService.GetAllProductsAsync(minPrice, maxPrice, categoryList);

            return Ok(products);
        }

        [HttpGet("GetProductPriceBySize/{productId}")]
        public async Task<ActionResult<List<Product>>> GetProductPriceBySizeAsync([FromRoute] int productId, [FromQuery] string size)
        {
            var product = await _ProductService.GetProductByIdAsync(productId);

            if (product == null) 
            {
                return NotFound("No product for this productId");
            }

            if (!string.IsNullOrEmpty(size)) 
            {
                var sizeWithPrice = product.Sizes.FirstOrDefault(x=>x.Size.Equals(size,StringComparison.OrdinalIgnoreCase));

                if (sizeWithPrice != null) 
                {
                    return Ok(sizeWithPrice.Price);
                }

                return NotFound("Size provided is not applicable for this product");
            }

            return BadRequest();
        }

        [HttpGet("GetProductById/{productId:int}")]
        public async Task<ActionResult<Product?>> GetProductByIdAsync([FromRoute] int productId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Console.WriteLine(productId);

            var product = await _ProductService.GetProductByIdAsync(productId);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpGet("GetProductsByCategory")]
        public async Task<ActionResult<List<Product>>> GetProductsByCategory([FromQuery] string category)
        {
            if (string.IsNullOrEmpty(category))
            {
                return BadRequest("Category cannot be null or empty.");
            }

            var productsByCategory = await _ProductService.GetProductsByCategoryAsync(category);

            return Ok(productsByCategory);
        }

        [HttpPost("CreateProductAsync")]
        public async Task<ActionResult> CreateProductAsync([FromBody] ProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _ProductService.CreateProductAsync(productDto);
            if (product == null)
            {
                return BadRequest(new { message = "Something went wrong" });
            }

            return Ok(product);
        }

        [Authorize(Roles = "admin")]
        [HttpPut("UpdateProductAsync/{productId:int}")]
        public async Task<ActionResult> UpdateProductAsync(int productId, [FromBody] ProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _ProductService.UpdateProductAsync(productId, productDto);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpDelete("{productId:int}")]
        public async Task<ActionResult> DeleteProdcutAsync(int productId)
        {
            var deletedProduct = await _ProductService.DeleteProductAsync(productId);

            if (deletedProduct == null)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
