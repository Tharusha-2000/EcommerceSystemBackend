using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.ReviewAndRating.Domain.DTOs
{
    public class FeedbackRequestDto
    {
        public int orderId { get; set; }

        [Required(ErrorMessage = "Feedback message is required.")]
        public string feedbackMessage { get; set; }

        [Range(1, 5, ErrorMessage = "Rate must be between 1 and 5.")]
        public double rate { get; set; }

        [Required(ErrorMessage = "Given date is required.")]
        public string givenDate { get; set; }
    }
}
