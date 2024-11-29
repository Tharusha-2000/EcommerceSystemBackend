using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.OrderProcessing.Domain.Models;
using Ecommerce.OrderProcessing.Infras;

namespace Ecommerce.OrderProcessing.API.Controllers
{
    public class OrderProductSer : IOrderProductSer
    {
        private readonly EcommerceDbContext _dbContext;

        public OrderProductSer(EcommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ActionResult<IEnumerable<OrderProduct>>> GetOrderProducts()
        {
            if (_dbContext.OrderProducts == null)
                return new NotFoundResult();

            return await _dbContext.OrderProducts.ToListAsync();
        }

        public async Task<ActionResult<OrderProduct>> GetOrderProductById(int orderProductId)
        {
            if (_dbContext.OrderProducts == null)
                return new NotFoundResult();

            var orderProduct = await _dbContext.OrderProducts.FindAsync(orderProductId);
            if (orderProduct == null)
                return new NotFoundResult();

            return orderProduct;
        }

        public async Task<ActionResult<IEnumerable<OrderProduct>>> GetOrderProductsByOrderId(int orderId)
        {
            if (_dbContext.OrderProducts == null)
                return new NotFoundResult();

            var orderProducts = await _dbContext.OrderProducts
                .Where(op => op.orderId == orderId)
                .ToListAsync();

            if (orderProducts == null || !orderProducts.Any())
                return new NotFoundResult();

            return orderProducts;
        }

        public async Task<ActionResult<OrderProduct>> PostOrderProduct(OrderProduct orderProduct)
        {
            _dbContext.OrderProducts.Add(orderProduct);
            await _dbContext.SaveChangesAsync();

            return new CreatedAtActionResult(nameof(GetOrderProductById), null, new { orderProductId = orderProduct.orderProductId }, orderProduct);
        }

        public async Task<ActionResult> PutOrderProduct(int orderProductId, OrderProduct orderProduct)
        {
            if (orderProductId != orderProduct.orderProductId)
                return new BadRequestResult();

            _dbContext.Entry(orderProduct).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_dbContext.OrderProducts.Any(op => op.orderProductId == orderProductId))
                    return new NotFoundResult();

                throw;
            }

            return new OkResult();
        }

        public async Task<ActionResult> DeleteOrderProduct(int orderProductId)
        {
            if (_dbContext.OrderProducts == null)
                return new NotFoundResult();

            var orderProduct = await _dbContext.OrderProducts.FindAsync(orderProductId);
            if (orderProduct == null)
                return new NotFoundResult();

            _dbContext.OrderProducts.Remove(orderProduct);
            await _dbContext.SaveChangesAsync();

            return new OkResult();
        }
    }
}
