using Ecommerce.ReviewAndRating.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.ReviewAndRating.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ISCController : ControllerBase
    {
        private IInterServiceCommunication _interServiceCommunication;

        public ISCController(IInterServiceCommunication interServiceCommunication)
        {
            _interServiceCommunication = interServiceCommunication;
        }

        [HttpPost("GetOrdersByIds/batch")]
        public async Task<IActionResult> GetOrdersByIds([FromBody] List<int> orderIds)
        {
            try
            {
                var orders = await _interServiceCommunication.GetOrdersByIdsAsync(orderIds);
                return Ok(orders);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
                

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred." });
            }
        }

        [HttpPost("GetUsersByIds/batch")]
        public async Task<IActionResult> GetUsersByIds([FromBody] List<int> userIds)
        {
            try
            {
                var users = await _interServiceCommunication.GetUsersByIdsAsync(userIds);
                return Ok(users);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred." });
            }
        }

        [HttpGet("GetProductIdsByOrderId/byOrder/{orderId}")]
        public async Task<IActionResult> GetProductIdsByOrderId(int orderId)
        {
            try
            {
                var productIds = await _interServiceCommunication.GetProductIdFromOrderServicesAsync(orderId);
                return Ok(productIds);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred." });
            }
        }
    }
}
