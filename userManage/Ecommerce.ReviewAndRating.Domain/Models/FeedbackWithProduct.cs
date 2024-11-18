using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.ReviewAndRating.Domain.Models
{
    public class FeedbackWithProduct
    {
        [Key]
        public int id { get; set; }
        public int feedbackId { get; set; }
        public int productId { get; set; }

    }
}
