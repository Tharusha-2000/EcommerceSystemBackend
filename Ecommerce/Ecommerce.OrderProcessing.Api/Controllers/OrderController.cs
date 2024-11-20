using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ecommerce.OrderProcessing.Domain.Models;

namespace Ecommerce.OrderProcessing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderSer _orderService;

        public OrderController(IOrderSer orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _orderService.GetOrders();
        }

        [HttpGet("{orderId}")]
        public async Task<ActionResult<Order>> GetOrderById(int orderId)
        {
            return await _orderService.GetOrderById(orderId);
        }

        [HttpGet("byUser/{userId}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersByUserId(int userId)
        {
            return await _orderService.GetOrdersByUserId(userId);
        }

        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            return await _orderService.PostOrder(order);
        }

        [HttpPut("{orderId}")]
        public async Task<ActionResult> PutOrder(int orderId, Order order)
        {
            return await _orderService.PutOrder(orderId, order);
        }

        [HttpDelete("{orderId}")]
        public async Task<ActionResult> DeleteOrder(int orderId)
        {
            return await _orderService.DeleteOrder(orderId);
        }
    }
}
