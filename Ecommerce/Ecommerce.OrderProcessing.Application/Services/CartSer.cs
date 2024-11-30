using Ecommerce.OrderProcessing.API.Controllers;
using Ecommerce.OrderProcessing.Domain.Models;
using Ecommerce.OrderProcessing.Infras;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.OrderProcessing.Application.Services
{
    public class CartSer : ICartSer
    {
        private readonly EcommerceDbContext _dbContext;

        public CartSer(EcommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ActionResult<IEnumerable<Cart>>> GetCarts()
        {
            if (_dbContext.Carts == null)
                return new NotFoundResult();

            return await _dbContext.Carts.ToListAsync();
        }

        public async Task<ActionResult<Cart>> GetCartById(int cartId)
        {
            if (_dbContext.Carts == null)
                return new NotFoundResult();

            var cart = await _dbContext.Carts.FindAsync(cartId);
            if (cart == null)
                return new NotFoundResult();

            return cart;
        }

        public async Task<ActionResult<IEnumerable<Cart>>> GetCartsByUserId(int userId)
        {
            if (_dbContext.Carts == null)
                return new NotFoundResult();

            var carts = await _dbContext.Carts
                .Where(c => c.userId == userId)
                .ToListAsync();

            if (carts == null || !carts.Any())
                return new NotFoundResult();

            return carts;
        }

        public async Task<ActionResult<Cart>> PostCart(Cart cart)
        {
            _dbContext.Carts.Add(cart);
            await _dbContext.SaveChangesAsync();

            return new CreatedAtActionResult(nameof(GetCartById), null, new { cartId = cart.cartId }, cart);
        }

        public async Task<ActionResult> DeleteCart(int cartId)
        {
            if (_dbContext.Carts == null)
                return new NotFoundResult();

            var cart = await _dbContext.Carts.FindAsync(cartId);
            if (cart == null)
                return new NotFoundResult();

            _dbContext.Carts.Remove(cart);
            await _dbContext.SaveChangesAsync();

            return new OkResult();
        }


        public async Task<ActionResult> PutCart(int cartId, int count)
        {
            try
            {
                var cartrow = await _dbContext.Carts.FindAsync(cartId);

                if (cartrow == null)
                {
                    return new NotFoundResult();
                }

                cartrow.count = count;
                await _dbContext.SaveChangesAsync();

                return new OkObjectResult(cartrow);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_dbContext.Carts.Any(c => c.cartId == cartId))
                    return new NotFoundResult();

                throw;
            }
        }
    }
}
