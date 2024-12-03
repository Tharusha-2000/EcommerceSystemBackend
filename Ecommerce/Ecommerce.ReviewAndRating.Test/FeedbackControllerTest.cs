using Ecommerce.ReviewAndRating.Api.Controllers;
using Ecommerce.ReviewAndRating.Application.Services;
using Ecommerce.ReviewAndRating.Domain.DTOs;
using Ecommerce.ReviewAndRating.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;


namespace Ecommerce.ReviewAndRating.Test
{
    public class FeedbackControllerTest
    {
        private readonly Mock<IReviewAndRatingService> _mockService;
        private readonly FeedBackController _controller;


        public FeedbackControllerTest()
        {
            _mockService = new Mock<IReviewAndRatingService>();
            _controller = new FeedBackController(_mockService.Object);
        }

        //For validate the respose body of the API call -  [HttpGet("GetAllFeedbacks")]
        [Fact]
        public async Task GetAllFeedbacksTest()
        {
            var feedbackList = new List<Feedback>

    {
        new Feedback { FeedbackId = 1, FeedbackMessage = "Excellent!", Rate = 5, GivenDate = "2024-12-01" },
        new Feedback { FeedbackId = 2, FeedbackMessage = "Not bad food", Rate = 3, GivenDate = "2024-12-02" }
    };

            _mockService.Setup(service => service.GetAllFeedbacks())
                       .ReturnsAsync(feedbackList);

            var result = await _controller.GetAllFeedbacks();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedFeedbacks = Assert.IsType<List<Feedback>>(okResult.Value);
            Assert.Equal(2, returnedFeedbacks.Count);
            Assert.Equal("Excellent!", returnedFeedbacks[0].FeedbackMessage);
            Assert.Equal(5, returnedFeedbacks[0].Rate);
        }

        //For validate the respose body of the API - [HttpGet("GetProductFeedback/{productId:int}")]
        [Fact]
        public async Task GetProductFeedbackTest()
        {
            
            int productId = 1;
            var feedbackList = new List<DisplayFeedbackDto>
            {
                new DisplayFeedbackDto { feedbackId = 1, firstName = "Dilshan", lastName = "Lakshitha", feedbackMessage = "Great!", rate = 5, givenDate = "2024-12-01" }
            };

            _mockService.Setup(service => service.GetProductFeedback(productId))
                       .ReturnsAsync(feedbackList);
           
            var result = await _controller.GetProductFeedback(productId);

           
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedFeedback = Assert.IsType<List<DisplayFeedbackDto>>(okResult.Value);
            Assert.Single(returnedFeedback);
            Assert.Equal("Dilshan", returnedFeedback[0].firstName);
            Assert.Equal("Lakshitha", returnedFeedback[0].lastName);
            Assert.Equal("Great!", returnedFeedback[0].feedbackMessage);
            Assert.Equal(5, returnedFeedback[0].rate);
            Assert.Equal("2024-12-01", returnedFeedback[0].givenDate);
        }

        //For validate the respose body of the API - [HttpGet("GetFeedbackByOrderId/{orderId:int}")]
        [Fact]
        public async Task GetFeedbackByOrderIdTest()
        {
            int orderId = 123;
            var feedbackResponse = new FeedbackResponseDto
            {
                FeedbackMessage = "Excellent product!",
                Rate = 5
            };

            _mockService.Setup(service => service.GetFeedbackByOrderId(orderId))
                       .ReturnsAsync(feedbackResponse);

            var result = await _controller.GetFeedbackByOrderId(orderId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedFeedback = Assert.IsType<FeedbackResponseDto>(okResult.Value);
            Assert.Equal("Excellent product!", returnedFeedback.FeedbackMessage);
            Assert.Equal(5, returnedFeedback.Rate);
        }

        //Test for valid order ID - [HttpGet("GetFeedbackByOrderId/{orderId:int}")]
        [Fact]
        public async Task GetFeedbackByOrderId_ValidOrderId_ReturnsFeedback()
        {
            int orderId = 123;
            var feedback = new FeedbackResponseDto
            {
                FeedbackMessage = "Good product!",
                Rate = 4
            };

            _mockService.Setup(service => service.GetFeedbackByOrderId(orderId))
                       .ReturnsAsync(feedback);

            var result = await _controller.GetFeedbackByOrderId(orderId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedFeedback = Assert.IsType<FeedbackResponseDto>(okResult.Value);
            Assert.Equal("Good product!", returnedFeedback.FeedbackMessage);
            Assert.Equal(4, returnedFeedback.Rate);
        }

      /*  [Fact]
        public async Task GetFeedbackByOrderId_InvalidOrderId_ReturnsNotFound()
        {
            // Arrange
            int orderId = 999;

            _mockService.Setup(service => service.GetFeedbackByOrderId(orderId))
                        .ReturnsAsync((FeedbackResponseDto)null); // Mock service to return null for invalid order ID


            var result = await _controller.GetFeedbackByOrderId(orderId);

            // Assert
            var actionResult = Assert.IsType<ActionResult<FeedbackResponseDto>>(result); // Validate ActionResult type
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(actionResult.Result); // Validate NotFoundObjectResult type
            Assert.Equal($"No feedback found for Order ID: {orderId}", notFoundResult.Value); // Validate the message
        }
      */

        //For validate the respose body of the API call - [HttpPost("SaveProductFeedback")]
        [Fact]
        public async Task SaveProductFeedbackTest()
        {
            var feedbackDto = new FeedbackRequestDto
            {
                orderId = 123,
                feedbackMessage = "Good quality!",
                rate = 4,
                givenDate = "2024-12-01"
            };

            _mockService.Setup(service => service.SaveProductFeedback(It.IsAny<FeedbackRequestDto>()))
                       .Returns(Task.CompletedTask);

            var result = await _controller.SaveProductFeedback(feedbackDto);

            var okResult = Assert.IsType<OkResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        // Test for no feedback found - [HttpPost("SaveProductFeedback")]
        [Fact]
        public async Task GetAllFeedbacks_NoFeedbacks_ReturnsEmptyList()
        {
            _mockService.Setup(service => service.GetAllFeedbacks())
                       .ReturnsAsync(new List<Feedback>());

            var result = await _controller.GetAllFeedbacks();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedFeedbacks = Assert.IsType<List<Feedback>>(okResult.Value);
            Assert.Empty(returnedFeedbacks);
        }





    }
}
