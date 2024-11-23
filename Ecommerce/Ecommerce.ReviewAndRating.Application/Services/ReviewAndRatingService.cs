using Ecommerce.ReviewAndRating.Domain.DTOs;
using Ecommerce.ReviewAndRating.Domain.Models;
using Ecommerce.ReviewAndRating.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;


namespace Ecommerce.ReviewAndRating.Application.Services
{
    public class ReviewAndRatingService : IReviewAndRatingService
    {

        private readonly ReviewAndRatingDbContext _context;
        private readonly IInterServiceCommunication _interServiceCommunication;
       


        public ReviewAndRatingService(ReviewAndRatingDbContext context, IInterServiceCommunication interServiceCommunication)
       
        {
            _context = context;
            _interServiceCommunication = interServiceCommunication;
        }


        public async Task SaveProductFeedback(FeedbackRequestDto feedbackDto)
        {
            try
            {
                var _feedback = new Feedback
                {

                    OrderId = feedbackDto.orderId,
                    FeedbackMessage = feedbackDto.feedbackMessage,
                    Rate = feedbackDto.rate,
                    GivenDate = DateTime.Parse(feedbackDto.givenDate).ToString("yyyy-MM-dd")
                };

                _context.Feedback.Add(_feedback);
                await _context.SaveChangesAsync();

                // Retrieve the FeedbackId of the newly saved feedback
                var savedFeedbackId = _feedback.FeedbackId;

                // Step 2: Get ProductIds from the OrderProduct table using the orderId
                var productIds = await _interServiceCommunication.GetProductIdFromOrderServicesAsync(feedbackDto.orderId);

                // Step 3: Add entries to FeedbackWithProduct table
                var feedbackWithProductEntries = productIds.Select(productId => new FeedbackWithProduct
                {
                    FeedbackId = savedFeedbackId,
                    ProductId = productId
                });

                _context.FeedbackWithProduct.AddRange(feedbackWithProductEntries);
                await _context.SaveChangesAsync();

            }

            catch (FormatException ex)
            {
                // Handle invalid date format
                throw new ArgumentException("Invalid date format provided in givenDate", ex);
            }
            catch (DbUpdateException ex)
            {
                // Handle database update errors
                throw new InvalidOperationException("An error occurred while saving feedback to the database.", ex);
            }
            catch (Exception ex)
            {
                // Handle any other errors
                throw new ApplicationException("An unexpected error occurred while saving product feedback.", ex);
            }
        }

        public async Task<List<DisplayFeedbackDto>> GetProductFeedback(int productId)
        {
            try
            {
                // Step 1: Get feedbacks and associated orderIds from the current service
                var feedbacks = await (from feedback in _context.Feedback
                                       join feedbackWithProduct in _context.FeedbackWithProduct
                                           on feedback.FeedbackId equals feedbackWithProduct.FeedbackId
                                       where feedbackWithProduct.ProductId == productId
                                       select new
                                       {
                                           feedback.FeedbackId,
                                           feedback.FeedbackMessage,
                                           feedback.Rate,
                                           feedback.GivenDate,
                                           feedback.OrderId
                                       })
                                        .Distinct()
                                       .ToListAsync();

                if (!feedbacks.Any())
                {
                    return new List<DisplayFeedbackDto>();
                }

                // Step 2: Extract orderIds from feedbacks
                var orderIds = feedbacks.Select(f => f.OrderId).Distinct().ToList();

                // Step 3: Call the Order service to get userIds for the orders
                var orders = await _interServiceCommunication.GetOrdersByIdsAsync(orderIds); // API call to Order service

                if (orders == null || !orders.Any())
                {
                    throw new ApplicationException("Failed to retrieve orders from the Order service.");
                }

                // Step 4: Extract unique userIds from the orders
                var userIds = orders.Select(o => o.userId).Distinct().ToList();

                // Step 5: Call the User service to get user details
                var users = await _interServiceCommunication.GetUsersByIdsAsync(userIds); // API call to User service

                if (users == null || !users.Any())
                {
                    throw new ApplicationException("Failed to retrieve users from the User service.");
                }

                // Step 6: Combine feedbacks with user and order details
                var feedbackDtos = feedbacks.Join(orders,
                                                  f => f.OrderId,
                                                  o => o.orderId,
                                                  (f, o) => new { f, o.userId })
                                            .Join(users,
                                                  fo => fo.userId,
                                                  u => u.Id,
                                                  (fo, u) => new DisplayFeedbackDto
                                                  {
                                                      feedbackId = fo.f.FeedbackId,
                                                      firstName = u.FirstName,
                                                      lastName = u.LastName,
                                                      feedbackMessage = fo.f.FeedbackMessage,
                                                      rate = fo.f.Rate,
                                                      givenDate = fo.f.GivenDate
                                                  })
                                            .Distinct()
                                            .ToList();

                return feedbackDtos;
            }
            catch (Exception ex)
            {
                // Handle exceptions
                throw new ApplicationException("An error occurred while retrieving product feedback.", ex);
            }
        }  

    }
}

