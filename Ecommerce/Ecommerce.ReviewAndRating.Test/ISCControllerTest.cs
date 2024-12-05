using Ecommerce.ReviewAndRating.Api.Controllers;
using Ecommerce.ReviewAndRating.Application.Services;
using Ecommerce.ReviewAndRating.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net.Http;

namespace Ecommerce.ReviewAndRating.Test
{
    public class ISCControllerTest
    {
        private readonly Mock<IInterServiceCommunication> _mockService;
        private readonly ISCController _controller;

        public ISCControllerTest()
        {
            _mockService = new Mock<IInterServiceCommunication>();
            _controller = new ISCController(_mockService.Object);
        }

        //For validate the respose body of the API call -  [HttpPost("GetOrdersByIds/batch")]
        [Fact]
        public async Task GetOrdersByIdsTest()
        {
            var orderIds = new List<int> { 1, 2, 3 };
            var expectedOrders = new List<OrderDto>
            {
                new OrderDto { orderId = 1, userId = 101 },
                new OrderDto { orderId = 2, userId = 102 }
            };

            _mockService.Setup(s => s.GetOrdersByIdsAsync(orderIds))
                        .ReturnsAsync(expectedOrders);

            var result = await _controller.GetOrdersByIds(orderIds);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expectedOrders, okResult.Value);
        }

        /* [Fact]
         public async Task GetOrdersByIds_ShouldReturn500_WhenApplicationExceptionThrown()
         {
             // Arrange
             var orderIds = new List<int> { 1, 2, 3 };
             var expectedErrorMessage = "Service error";

             // Mock the InterServiceCommunication service to throw ApplicationException
             _mockService.Setup(s => s.GetOrdersByIdsAsync(orderIds))
                 .ThrowsAsync(new ApplicationException(expectedErrorMessage));

             // Act
             var result = await _controller.GetOrdersByIds(orderIds);

             // Assert
             var statusCodeResult = Assert.IsType<ObjectResult>(result);
             Assert.Equal(500, statusCodeResult.StatusCode);

             var resultValue = Assert.IsType<Dictionary<string, string>>(statusCodeResult.Value);
             Assert.Equal(expectedErrorMessage, resultValue["message"]);
         }
        */


        //For validate the respose body of the API - [HttpPost("GetUsersByIds/batch")]  
        [Fact]
        public async Task GetUsersByIdsTest()
        {
           
            var userIds = new List<int> { 1, 2 };
            var expectedUsers = new List<UserDto>
            {
                new UserDto { Id = 1, FirstName = "John", LastName = "Doe" },
                new UserDto { Id = 2, FirstName = "Jane", LastName = "Smith" }
            };

            _mockService.Setup(s => s.GetUsersByIdsAsync(userIds))
                        .ReturnsAsync(expectedUsers);

         
            var result = await _controller.GetUsersByIds(userIds);

            
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expectedUsers, okResult.Value);
        }

        /*  [Fact]
          public async Task GetUsersByIds_ShouldReturn500_WhenApplicationExceptionThrown()
          {
              // Arrange
              var userIds = new List<int> { 1, 2 };

              _mockService.Setup(s => s.GetUsersByIdsAsync(userIds))
                          .ThrowsAsync(new ApplicationException("Service error"));

              // Act
              var result = await _controller.GetUsersByIds(userIds);

              // Assert
              var statusCodeResult = Assert.IsType<ObjectResult>(result);
              Assert.Equal(500, statusCodeResult.StatusCode);
              Assert.Equal("Service error", ((dynamic)statusCodeResult.Value).message);
          }
        */


        //For validate the respose body of the API - [HttpGet("GetProductIdsByOrderId/byOrder/{orderId}")]
        [Fact]
        public async Task GetProductIdsByOrderIdTest()
        {
           
            int orderId = 1;
            var expectedProductIds = new List<int> { 101, 102, 103 };

            _mockService.Setup(s => s.GetProductIdFromOrderServicesAsync(orderId))
                        .ReturnsAsync(expectedProductIds);

            
            var result = await _controller.GetProductIdsByOrderId(orderId);

            
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expectedProductIds, okResult.Value);
        }

       /* [Fact]
        public async Task GetProductIdsByOrderId_ShouldReturn404_WhenNoProductsFound()
        {
            // Arrange
            int orderId = 1;

            _mockService.Setup(s => s.GetProductIdFromOrderServicesAsync(orderId))
                        .ThrowsAsync(new InvalidOperationException("No products found"));

            // Act
            var result = await _controller.GetProductIdsByOrderId(orderId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("No products found", ((dynamic)notFoundResult.Value).message);
        }
       */

      /*
        [Fact]
        public async Task GetProductIdsByOrderId_ShouldReturn503_WhenServiceUnavailable()
        {
            // Arrange
            int orderId = 1;

            _mockService.Setup(s => s.GetProductIdFromOrderServicesAsync(orderId))
                        .ThrowsAsync(new HttpRequestException("Service is unavailable"));

            // Act
            var result = await _controller.GetProductIdsByOrderId(orderId);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(503, statusCodeResult.StatusCode);
            Assert.Equal("Service is unavailable", ((dynamic)statusCodeResult.Value).message);
        }
      */
    }

}