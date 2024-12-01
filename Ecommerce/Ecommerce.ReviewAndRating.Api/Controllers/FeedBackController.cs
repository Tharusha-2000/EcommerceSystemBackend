using Ecommerce.ReviewAndRating.Application.Services;
using Ecommerce.ReviewAndRating.Domain.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.ReviewAndRating.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedBackController : ControllerBase
    {

        private IReviewAndRatingService _reviewAndRatingService;

        public FeedBackController(IReviewAndRatingService reviewAndRatingService)
        {
            _reviewAndRatingService = reviewAndRatingService;
        }

        [HttpGet("GetAllFeedbacks")]
        public async Task<IActionResult> GetAllFeedbacks()
        {
            var allFeedbacks = await _reviewAndRatingService.GetAllFeedbacks();
            return Ok(allFeedbacks);
        }

        [HttpGet("GetProductFeedback/{productId:int}")]
        public async Task<IActionResult> GetProductFeedback(int productId)
        {
            var feedbacks = await _reviewAndRatingService.GetProductFeedback(productId);
            return Ok(feedbacks);
        }

        [HttpGet("GetFeedbackByOrderId/{orderId:int}")]
        public async Task<IActionResult> GetFeedbackByOrderId(int orderId)
        {
            var feedback = await _reviewAndRatingService.GetFeedbackByOrderId(orderId);
            return Ok(feedback);
        }

        [HttpPost("SaveProductFeedback")]
        public async Task<IActionResult> SaveProductFeedback(FeedbackRequestDto feedbackDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _reviewAndRatingService.SaveProductFeedback(feedbackDto);
            return Ok();
        }

        
    }
}
