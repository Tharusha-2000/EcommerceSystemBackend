using Ecommerce.OrderProcessing.API.Controllers;
using Ecommerce.OrderProcessing.Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.OrderProcessing.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetOrdersByIdController : ControllerBase
    {
        private readonly IOrderSer _orderService;

        public GetOrdersByIdController(IOrderSer orderService)
        {
            _orderService = orderService;
        }

        [Authorize(Roles = "customer")]
        [HttpPost("batch")]
        public async Task<ActionResult<List<OrderDto>>> GetOrdersByIdsAsync([FromBody] List<int> orderIds)
        {
            try
            {
                // Call the service to get orders
                var orders = await _orderService.GetOrdersByIdsAsync(orderIds);

                // Validate the result
                if (orders == null || !orders.Any())
                {
                    return NotFound("No orders found for the provided IDs.");
                }

                // Return the result wrapped in Ok()
                return Ok(orders);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


    }
}
