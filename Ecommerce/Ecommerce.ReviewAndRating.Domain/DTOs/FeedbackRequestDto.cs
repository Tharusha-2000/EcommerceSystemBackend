using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.ReviewAndRating.Domain.DTOs
{
    public class FeedbackRequestDto
    {
        public int orderId { get; set; }
        public string feedbackMessage { get; set; }
        public double rate { get; set; }
        public string givenDate { get; set; }
    }
}
