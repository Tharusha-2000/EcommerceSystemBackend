﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ecommerce.OrderProcessing.Domain.Models;

namespace Ecommerce.OrderProcessing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderProductController : ControllerBase
    {
        private readonly IOrderProductSer _orderProductService;

        public OrderProductController(IOrderProductSer orderProductService)
        {
            _orderProductService = orderProductService;
        }

        [Authorize(Roles = "customer,Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderProduct>>> GetOrderProducts()
        {
            return await _orderProductService.GetOrderProducts();
        }

        [Authorize(Roles = "customer,Admin")]
        [HttpGet("{orderProductId}")]
        public async Task<ActionResult<OrderProduct>> GetOrderProductById(int orderProductId)
        {
            return await _orderProductService.GetOrderProductById(orderProductId);
        }

      
        [HttpGet("byOrder/{orderId}")]
        public async Task<ActionResult<IEnumerable<OrderProduct>>> GetOrderProductsByOrderId(int orderId)
        {
            return await _orderProductService.GetOrderProductsByOrderId(orderId);
        }

        [Authorize(Roles = "customer,Admin")]
        [HttpPost]
        public async Task<ActionResult<OrderProduct>> PostOrderProduct(OrderProduct orderProduct)
        {
            return await _orderProductService.PostOrderProduct(orderProduct);
        }

        [Authorize(Roles = "customer,Admin")]
        [HttpPut("{orderProductId}")]
        public async Task<ActionResult> PutOrderProduct(int orderProductId, OrderProduct orderProduct)
        {
            return await _orderProductService.PutOrderProduct(orderProductId, orderProduct);
        }

        [Authorize(Roles = "customer,Admin")]
        [HttpDelete("{orderProductId}")]
        public async Task<ActionResult> DeleteOrderProduct(int orderProductId)
        {
            return await _orderProductService.DeleteOrderProduct(orderProductId);
        }
    }
}
