using Ecommerce.ReviewAndRating.Api.Controllers;
using Ecommerce.ReviewAndRating.Application.Services;
using Ecommerce.ReviewAndRating.Domain.DTOs;
using Ecommerce.ReviewAndRating.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.ReviewAndRating.Test
{
    public class FeedbackControllerTest
    {
        private readonly Mock<IReviewAndRatingService> _mockService;

        public FeedbackControllerTest()
        {
            _mockService = new Mock<IReviewAndRatingService>();
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

            var controller = new FeedBackController(_mockService.Object);

            var result = await controller.GetAllFeedbacks();

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

            var controller = new FeedBackController(_mockService.Object);

           
            var result = await controller.GetProductFeedback(productId);

           
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

            var controller = new FeedBackController(_mockService.Object);

            var result = await controller.GetFeedbackByOrderId(orderId);

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

            var controller = new FeedBackController(_mockService.Object);

            var result = await controller.GetFeedbackByOrderId(orderId);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedFeedback = Assert.IsType<FeedbackResponseDto>(okResult.Value);
            Assert.Equal("Good product!", returnedFeedback.FeedbackMessage);
            Assert.Equal(4, returnedFeedback.Rate);
        }

        //Test for invalid order ID - [HttpGet("GetFeedbackByOrderId/{orderId:int}")]
        [Fact]
        public async Task GetFeedbackByOrderId_InvalidOrderId_ReturnsNotFound()
        {
            int orderId = 999;

            _mockService.Setup(service => service.GetFeedbackByOrderId(orderId))
                       .ReturnsAsync((FeedbackResponseDto)null);

            var controller = new FeedBackController(_mockService.Object);

            var result = await controller.GetFeedbackByOrderId(orderId);

            var notFoundResult = Assert.IsType<NotFoundResult>(result);
        }




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

            var controller = new FeedBackController(_mockService.Object);

            var result = await controller.SaveProductFeedback(feedbackDto);

            var okResult = Assert.IsType<OkResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        // Test for invalid feedback - [HttpPost("SaveProductFeedback")]
      /*  [Fact]
        public async Task SaveProductFeedback_InvalidFeedback_ReturnsBadRequest()
        {
            var feedbackDto = new FeedbackRequestDto
            {
                orderId = 123,
                feedbackMessage = "", // No feedback message
                rate = 5,
                givenDate = "2024-12-01"
            };

            var controller = new FeedBackController(_mockService.Object);

            var result = await controller.SaveProductFeedback(feedbackDto);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResponse = badRequestResult.Value as SerializableError;
            Assert.NotNull(badRequestResponse);
            Assert.True(badRequestResponse.ContainsKey("feedbackMessage"));
            Assert.Equal("Feedback message is required.", badRequestResponse["feedbackMessage"][0]);
        }

*/
        // Test for no feedback found - [HttpPost("SaveProductFeedback")]
        [Fact]
        public async Task GetAllFeedbacks_NoFeedbacks_ReturnsEmptyList()
        {
            _mockService.Setup(service => service.GetAllFeedbacks())
                       .ReturnsAsync(new List<Feedback>());

            var controller = new FeedBackController(_mockService.Object);

            var result = await controller.GetAllFeedbacks();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedFeedbacks = Assert.IsType<List<Feedback>>(okResult.Value);
            Assert.Empty(returnedFeedbacks);
        }





    }
}
