using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.OrderProcessing.Domain.Models;

namespace Ecommerce.OrderProcessing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartSer _cartService;

        public CartController(ICartSer cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cart>>> GetCarts()
        {
            return await _cartService.GetCarts();
        }

        [HttpGet("{cartId}")]
        public async Task<ActionResult<Cart>> GetCartById(int cartId)
        {
            return await _cartService.GetCartById(cartId);
        }

        [HttpGet("byUser/{userId}")]
        public async Task<ActionResult<IEnumerable<Cart>>> GetCartsByUserId(int userId)
        {
            return await _cartService.GetCartsByUserId(userId);
        }

        [HttpPost]
        public async Task<ActionResult<Cart>> PostCart(Cart cart)
        {
            return await _cartService.PostCart(cart);
        }


        //[HttpPut("{cartId}")]
        //public async Task<ActionResult> PutCart(int cartId, Cart cart)
        //{
        //    return await _cartService.PutCart(cartId, cart);
        //}

        [HttpDelete("{cartId}")]
        public async Task<ActionResult> DeleteCart(int cartId)
        {
            return await _cartService.DeleteCart(cartId);
        }

        [HttpPut("{cartId}")]
        public async Task<ActionResult> PutCart(int cartId, [FromQuery] int count)
        {
            return await _cartService.PutCart(cartId, count);
        }

    }
}
