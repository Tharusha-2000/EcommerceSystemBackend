using Ecommerce.userManage.Application.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.UserManage.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetUsersByIdsController : ControllerBase
    {
        private readonly IUserService _userService;

        public GetUsersByIdsController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("batch")]
        public async Task<IActionResult> GetUsersByIds([FromBody] List<int> userIds)
        {
            try
            {
                // Call the service method
                var users = await _userService.GetUsersByIdsAsync(userIds);

                // Return results
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
