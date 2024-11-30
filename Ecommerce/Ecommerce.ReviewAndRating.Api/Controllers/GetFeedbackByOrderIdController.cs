using Ecommerce.ReviewAndRating.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Ecommerce.ReviewAndRating.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetFeedbackByOrderIdController : ControllerBase
    {
        private readonly IReviewAndRatingService _reviewAndRatingService;

        public GetFeedbackByOrderIdController(IReviewAndRatingService reviewAndRatingService)
        {
            _reviewAndRatingService = reviewAndRatingService;
        }

        [HttpGet("{orderId}")]

        public async Task<IActionResult> GetFeedbackByOrderId(int orderId)
        {
            var feedback = await _reviewAndRatingService.GetFeedbackByOrderId(orderId);
            return Ok(feedback);
        }
    }

}
