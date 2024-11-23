using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.ReviewAndRating.Domain.Models
{
    public class FeedbackWithProduct
    {
        [Key]
        public int Id { get; set; }

        // Foreign Key for Feedback
        [ForeignKey("Feedback")]
        public int FeedbackId { get; set; }

        // Reference to the Product ID
        public int ProductId { get; set; }

        // Navigation Property for Feedback
        public Feedback Feedback { get; set; }

    }
}
