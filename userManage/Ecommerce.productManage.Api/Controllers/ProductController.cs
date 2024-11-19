using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.productManage.Application.Services;
using Ecommerce.productManage.Domain.Models.Domains;
using Ecommerce.productManage.Domain.Models.DTOs;

namespace Ecommerce.productManage.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductsService _ProductsService;

        public ProductController(IProductsService productsService)
        {
            _ProductsService = productsService;
        }

        [HttpGet("GetAllProducts")]
        public async Task<ActionResult<List<Product>>> GetAllProductsAsync()
        {
            var products = await _ProductsService.GetAllProductsAsync();

            return Ok(products);
        }

        [HttpGet("GetProductById/{productId:int}")]
        public async Task<ActionResult<Product?>> GetProductByIdAsync([FromRoute] int productId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _ProductsService.GetProductByIdAsync(productId);

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

            var productsByCategory = await _ProductsService.GetProductsByCategoryAsync(category);

            return Ok(productsByCategory);
        }

        [HttpPost("CreateProductAsync")]
        public async Task<ActionResult> CreateProductAsync([FromBody] ProductDto productDto)
        {
            if (!ModelState.IsValid) 
            { 
                return BadRequest(ModelState);
            }

            var product = await _ProductsService.CreateProductAsync(productDto);
            if (product == null)
            {
                return BadRequest(new {message="Something went wrong"});
            }

            return Ok(product);
        }

        [HttpPut("UpdateProductAsync/{productId:int}")]
        public async Task<ActionResult> UpdateProductAsync(int productId, [FromBody] ProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _ProductsService.UpdateProductAsync(productId, productDto);

            if(product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }
    }
}
