using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.ReviewAndRating.Domain.DTOs
{
    public class FeedbackResponseDto
    {
        public string FeedbackMessage { get; set; }
        public double Rate { get; set; }
    }
}
