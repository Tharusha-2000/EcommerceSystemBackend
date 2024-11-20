using Ecommerce.ReviewAndRating.Domain.DTOs;
using Ecommerce.ReviewAndRating.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.ReviewAndRating.Application.Services
{
    public interface IReviewAndRatingService 
    {
        public Task SaveProductFeedback(FeedbackRequestDto feedbackDto);
        Task<List<DisplayFeedbackDto>> GetProductFeedback(int productId);

    }
}
