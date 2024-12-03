using Ecommerce.ReviewAndRating.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.ReviewAndRating.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetOrdersByIdsAsyncController : ControllerBase
    {
        private IInterServiceCommunication _interServiceCommunication;

        public GetOrdersByIdsAsyncController(IInterServiceCommunication interServiceCommunication)
        {
            _interServiceCommunication = interServiceCommunication;
        }

        [Authorize(Roles = "customer,admin")]
        [HttpPost("batch")]
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
    }
}
