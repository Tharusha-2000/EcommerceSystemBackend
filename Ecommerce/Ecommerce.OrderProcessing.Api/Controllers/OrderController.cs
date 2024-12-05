using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ecommerce.OrderProcessing.Domain.Models;
using Ecommerce.OrderProcessing.Application.Services;

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

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await _orderService.GetOrders();
        }

        [Authorize(Roles = "customer,Admin")]
        [HttpGet("{orderId}")]
        public async Task<ActionResult<Order>> GetOrderById(int orderId)
        {
            return await _orderService.GetOrderById(orderId);
        }

       [Authorize(Roles = "customer")]
        [HttpGet("byUser/{userId}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersByUserId(int userId)
        {
            return await _orderService.GetOrdersByUserId(userId);
        }

        [Authorize(Roles = "customer,Admin")]
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            return await _orderService.PostOrder(order);
        }

        [Authorize(Roles = "customer,Admin")]
        [HttpPut("{orderId}")]
        public async Task<ActionResult> PutOrder(int orderId, Order order)
        {
            return await _orderService.PutOrder(orderId, order);
        }

        [Authorize(Roles = "customer,Admin")]
        [HttpDelete("{orderId}")]
        public async Task<ActionResult> DeleteOrder(int orderId)
        {
            return await _orderService.DeleteOrder(orderId);
        }
     
        [HttpPut("byOrderId/{orderId}")]
        public async Task<ActionResult> PutOrderPaymentStatus(int orderId, bool paymentStatus)
        {
            return await _orderService.PutOrderPaymentStatus(orderId, paymentStatus);
        }
    }
}
