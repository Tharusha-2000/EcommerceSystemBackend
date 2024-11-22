using Ecommerce.ReviewAndRating.Domain.DTOs;
using Ecommerce.ReviewAndRating.Domain.Models;
using Ecommerce.ReviewAndRating.Infrastructure;
//using Ecommerce.userManage.Infrastructure;
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
        // private readonly UserDbContext _userDbContext;
        private readonly HttpClient _httpClient;


        public ReviewAndRatingService(ReviewAndRatingDbContext context, HttpClient httpClient)
       
        {
            _context = context;
            //_userDbContext = userDbContext;
            _httpClient = httpClient;
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
                // Step 1: Get feedbacks and associated orderIds from the current service
                var feedbacks = await (from feedback in _context.Feedback
                                       join feedbackWithProduct in _context.FeedbackWithProduct
                                           on feedback.feedbackId equals feedbackWithProduct.feedbackId
                                       where feedbackWithProduct.productId == productId
                                       select new
                                       {
                                           feedback.feedbackId,
                                           feedback.feedbackMessage,
                                           feedback.rate,
                                           feedback.givenDate,
                                           feedback.orderId
                                       }).ToListAsync();

                if (!feedbacks.Any())
                {
                    return new List<DisplayFeedbackDto>();
                }

                // Step 2: Extract orderIds from feedbacks
                var orderIds = feedbacks.Select(f => f.orderId).Distinct().ToList();

                // Step 3: Call the Order service to get userIds for the orders
                var orders = await GetOrdersByIdsAsync(orderIds); // API call to Order service

                if (orders == null || !orders.Any())
                {
                    throw new ApplicationException("Failed to retrieve orders from the Order service.");
                }

                // Step 4: Extract unique userIds from the orders
                var userIds = orders.Select(o => o.userId).Distinct().ToList();

                // Step 5: Call the User service to get user details
                var users = await GetUsersByIdsAsync(userIds); // API call to User service

                if (users == null || !users.Any())
                {
                    throw new ApplicationException("Failed to retrieve users from the User service.");
                }

                // Step 6: Combine feedbacks with user and order details
                var feedbackDtos = feedbacks.Join(orders,
                                                  f => f.orderId,
                                                  o => o.orderId,
                                                  (f, o) => new { f, o.userId })
                                            .Join(users,
                                                  fo => fo.userId,
                                                  u => u.Id,
                                                  (fo, u) => new DisplayFeedbackDto
                                                  {
                                                      feedbackId = fo.f.feedbackId,
                                                      firstName = u.FirstName,
                                                      lastName = u.LastName,
                                                      feedbackMessage = fo.f.feedbackMessage,
                                                      rate = fo.f.rate,
                                                      givenDate = fo.f.givenDate
                                                  })
                                            .ToList();

                return feedbackDtos;
            }
            catch (Exception ex)
            {
                // Handle exceptions
                throw new ApplicationException("An error occurred while retrieving product feedback.", ex);
            }
        }


        public async Task<List<OrderDto>> GetOrdersByIdsAsync(List<int> orderIds)
        {
            var apiUrl = "https://localhost:7242/api/GetOrdersById/batch"; // Replace with actual endpoint

            var response = await _httpClient.PostAsJsonAsync(apiUrl, orderIds);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<List<OrderDto>>();
            }

            throw new ApplicationException($"Failed to fetch orders. Status code: {response.StatusCode}");
        }

        public async Task<List<UserDto>> GetUsersByIdsAsync(List<int> userIds)
        {
            var apiUrl = "http://user-service/api/users/batch"; // Replace with actual endpoint

            var response = await _httpClient.PostAsJsonAsync(apiUrl, userIds);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<List<UserDto>>();
            }

            throw new ApplicationException($"Failed to fetch users. Status code: {response.StatusCode}");
        }
    }
}
