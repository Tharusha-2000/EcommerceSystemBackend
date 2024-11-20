using Ecommerce.ReviewAndRating.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.ReviewAndRating.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetProductFeedBackController : ControllerBase
    {

        private IReviewAndRatingService _reviewAndRatingService;

        public GetProductFeedBackController(IReviewAndRatingService reviewAndRatingService)
        {
            _reviewAndRatingService = reviewAndRatingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductFeedback(int productId)
        {
            var feedbacks = await _reviewAndRatingService.GetProductFeedback(productId);
            return Ok(feedbacks);
        }   
    }
}
