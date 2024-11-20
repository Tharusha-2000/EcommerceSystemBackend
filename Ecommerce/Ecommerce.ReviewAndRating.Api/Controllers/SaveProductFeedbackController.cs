using Ecommerce.ReviewAndRating.Application.Services;
using Ecommerce.ReviewAndRating.Domain.DTOs;
using Ecommerce.ReviewAndRating.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.ReviewAndRating.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaveProductFeedbackController : ControllerBase
    {
        private IReviewAndRatingService _reviewAndRatingService;

        public SaveProductFeedbackController(IReviewAndRatingService reviewAndRatingService)
        {
            _reviewAndRatingService = reviewAndRatingService;
        }

        [HttpPost]
        public async Task<IActionResult> SaveProductFeedback(FeedbackRequestDto feedbackDto)
        {
            await _reviewAndRatingService.SaveProductFeedback(feedbackDto);
            return Ok();
        }
    }
}
