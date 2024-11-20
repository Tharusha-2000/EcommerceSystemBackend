using Ecommerce.ReviewAndRating.Domain.DTOs;
using Ecommerce.ReviewAndRating.Domain.Models;
using Ecommerce.ReviewAndRating.Infrastructure;
using Ecommerce.userManage.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.ReviewAndRating.Application.Services
{
    public class ReviewAndRatingService : IReviewAndRatingService
    {

        private readonly ReviewAndRatingDbContext _context;
        private readonly UserDbContext _userDbContext;

        public ReviewAndRatingService(ReviewAndRatingDbContext context, UserDbContext userDbContext)
       
        {
            _context = context;
           _userDbContext = userDbContext;
        }



        public async Task SaveProductFeedback(FeedbackRequestDto feedbackDto)
        {
            try
            {
                var _feedback = new Feedback
                {

                    orderId = feedbackDto.orderId,
                    feedbackMessage = feedbackDto.feedbackMessage,
                    rate = feedbackDto.rate,
                    givenDate = DateTime.Parse(feedbackDto.givenDate).ToString("yyyy-MM-dd")
                };

                _context.Feedback.Add(_feedback);
                await _context.SaveChangesAsync();

                // Retrieve the FeedbackId of the newly saved feedback
                var savedFeedbackId = _feedback.feedbackId;

                // Step 2: Get ProductIds from the OrderProduct table using the orderId
                var productIds = _context.OrderProduct
                    .Where(op => op.orderId == feedbackDto.orderId)
                    .Select(op => op.productId)
                    .ToList();

                // Step 3: Add entries to FeedbackWithProduct table
                var feedbackWithProductEntries = productIds.Select(productId => new FeedbackWithProduct
                {
                    feedbackId = savedFeedbackId,
                    productId = productId
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
                // Step 1: Get feedbacks and associated orderIds using _context
                var feedbacks = await (from feedback in _context.Feedback
                                       join feedbackWithProduct in _context.FeedbackWithProduct
                                           on feedback.feedbackId equals feedbackWithProduct.feedbackId
                                       join order in _context.Order
                                           on feedback.orderId equals order.orderId
                                       where feedbackWithProduct.productId == productId
                                       select new
                                       {
                                           feedback.feedbackId,
                                           feedback.feedbackMessage,
                                           feedback.rate,
                                           feedback.givenDate,
                                           order.userId
                                       }).ToListAsync();

                // Step 2: Extract userIds from the results
                var userIds = feedbacks.Select(f => f.userId).Distinct().ToList();

                // Step 3: Get user information using _userDbContext
                var users = await _userDbContext.UserModel
                                                .Where(u => userIds.Contains(u.Id))
                                                .Select(u => new { u.Id, u.FirstName, u.LastName })
                                                .ToListAsync();

                // Step 4: Combine feedbacks with user details in memory, because can't access two different DbContexts in a single query
                var feedbackDtos = feedbacks.Join(users,
                                                  f => f.userId,
                                                  u => u.Id,
                                                  (f, u) => new DisplayFeedbackDto
                                                  {
                                                      feedbackId = f.feedbackId,
                                                      firstName = u.FirstName,
                                                      lastName = u.LastName,
                                                      feedbackMessage = f.feedbackMessage,
                                                      rate = f.rate,
                                                      givenDate = f.givenDate
                                                  }).ToList();

                return feedbackDtos;
            }

            catch (InvalidOperationException ex)
            {
                // Handle query execution issues
                throw new ApplicationException("An error occurred while retrieving product feedback.", ex);
            }
            catch (DbUpdateException ex)
            {
                // Handle database-related issues
                throw new InvalidOperationException("A database error occurred while retrieving product feedback.", ex);
            }
            catch (Exception ex)
            {
                // Handle any other unexpected errors
                throw new ApplicationException("An unexpected error occurred while retrieving product feedback.", ex);
            }





        }

    }
}
