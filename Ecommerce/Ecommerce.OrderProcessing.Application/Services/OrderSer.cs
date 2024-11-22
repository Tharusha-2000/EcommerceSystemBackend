using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.OrderProcessing.Domain.Models;
using Ecommerce.OrderProcessing.Infras;

namespace Ecommerce.OrderProcessing.API.Controllers
{
    public class OrderSer : IOrderSer
    {
        private readonly EcommerceDbContext _dbContext;

        public OrderSer(EcommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            if (_dbContext.Orders == null)
                return new NotFoundResult();

            return await _dbContext.Orders.ToListAsync();
        }

        public async Task<ActionResult<Order>> GetOrderById(int orderId)
        {
            if (_dbContext.Orders == null)
                return new NotFoundResult();

            var order = await _dbContext.Orders.FindAsync(orderId);
            if (order == null)
                return new NotFoundResult();

            return order;
        }

        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersByUserId(int userId)
        {
            if (_dbContext.Orders == null)
                return new NotFoundResult();

            var orders = await _dbContext.Orders
                .Where(o => o.userId == userId)
                .ToListAsync();

            if (orders == null || !orders.Any())
                return new NotFoundResult();

            return orders;
        }

        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();

            return new CreatedAtActionResult(nameof(GetOrderById), null, new { orderId = order.orderId }, order);
        }

        public async Task<ActionResult> PutOrder(int orderId, Order order)
        {
            if (orderId != order.orderId)
                return new BadRequestResult();

            _dbContext.Entry(order).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_dbContext.Orders.Any(o => o.orderId == orderId))
                    return new NotFoundResult();

                throw;
            }

            return new OkResult();
        }

        public async Task<ActionResult> DeleteOrder(int orderId)
        {
            if (_dbContext.Orders == null)
                return new NotFoundResult();

            var order = await _dbContext.Orders.FindAsync(orderId);
            if (order == null)
                return new NotFoundResult();

            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync();

            return new OkResult();
        }
    }
}
