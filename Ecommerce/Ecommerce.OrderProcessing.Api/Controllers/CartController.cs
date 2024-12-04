using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.OrderProcessing.Domain.Models;
using Microsoft.AspNetCore.Authorization;

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

       // [Authorize(Roles = "customer")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cart>>> GetCarts()
        {
            return await _cartService.GetCarts();
        }

        [Authorize(Roles = "customer")]
        [HttpGet("{cartId}")]
        public async Task<ActionResult<Cart>> GetCartById(int cartId)
        {
            return await _cartService.GetCartById(cartId);
        }

      //  [Authorize(Roles = "customer")]
        [HttpGet("byUser/{userId}")]
        public async Task<ActionResult<IEnumerable<Cart>>> GetCartsByUserId(int userId)
        {
            return await _cartService.GetCartsByUserId(userId);
        }

        [Authorize(Roles = "customer")]
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
