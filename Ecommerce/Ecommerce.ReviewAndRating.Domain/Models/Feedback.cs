using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.ReviewAndRating.Domain.Models
{
    public class Feedback
    {
        [Key]
        public int FeedbackId { get; set; }
        public int OrderId { get; set; }
        public string FeedbackMessage { get; set; }
        public double Rate { get; set; }
        public string GivenDate { get; set; }

        // Navigation Property for FeedbackWithProduct
        public ICollection<FeedbackWithProduct> FeedbackWithProducts { get; set; }
    }
}
