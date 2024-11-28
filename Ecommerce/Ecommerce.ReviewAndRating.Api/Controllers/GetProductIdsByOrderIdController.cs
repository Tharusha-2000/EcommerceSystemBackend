using Ecommerce.ReviewAndRating.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.ReviewAndRating.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetProductIdsByOrderIdController : ControllerBase
    {
        private IInterServiceCommunication _interServiceCommunication;

        public GetProductIdsByOrderIdController(IInterServiceCommunication interServiceCommunication)
        {
            _interServiceCommunication = interServiceCommunication;
        }


        [HttpGet("byOrder/{orderId}")]
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
