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
        public int feedbackId { get; set; }
        public int orderId { get; set; }
        public string feedbackMessage { get; set; }
        public double rate { get; set; }
        public string givenDate { get; set; }
    }
}
