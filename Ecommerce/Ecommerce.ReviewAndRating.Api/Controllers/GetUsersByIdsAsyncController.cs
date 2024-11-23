using Ecommerce.ReviewAndRating.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.ReviewAndRating.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetUsersByIdsAsyncController : ControllerBase
    {
        private IInterServiceCommunication _interServiceCommunication;

        public GetUsersByIdsAsyncController(IInterServiceCommunication interServiceCommunication)
        {
            _interServiceCommunication = interServiceCommunication;
        }

        [HttpPost("batch")]
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
    }
}
