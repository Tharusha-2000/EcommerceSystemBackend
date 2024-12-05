using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Ecommerce.OrderProcessing.API.Controllers;
using Ecommerce.OrderProcessing.Domain.Models;




namespace Ecommerce.OrderProcessing.Tests
{
    public class OrderControllerTests
    {
        private readonly Mock<IOrderSer> _orderServiceMock;
        private readonly OrderController _controller;

        public OrderControllerTests()
        {
            _orderServiceMock = new Mock<IOrderSer>();
            _controller = new OrderController(_orderServiceMock.Object);
        }

        [Fact]
        public async Task GetOrders_ShouldReturnAllOrders()
        {
            // Arrange
            var orders = new List<Order>
            {
                new Order { orderId = 1, userId = 101, totalPrice = 200, paymentStatus = true },
                new Order { orderId = 2, userId = 102, totalPrice = 150, paymentStatus = false }
            };

            _orderServiceMock.Setup(service => service.GetOrders()).ReturnsAsync(new ActionResult<IEnumerable<Order>>(orders));

            // Act
            var result = await _controller.GetOrders();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Order>>>(result);
            var value = Assert.IsAssignableFrom<IEnumerable<Order>>(actionResult.Value);
            Assert.Equal(2, value.Count());
        }

        [Fact]
        public async Task GetOrderById_ValidId_ShouldReturnOrder()
        {
            // Arrange
            var order = new Order { orderId = 1, userId = 101, totalPrice = 200, paymentStatus = true };

            _orderServiceMock.Setup(service => service.GetOrderById(1)).ReturnsAsync(new ActionResult<Order>(order));

            // Act
            var result = await _controller.GetOrderById(1);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Order>>(result);
            var value = Assert.IsType<Order>(actionResult.Value);
            Assert.Equal(1, value.orderId);
        }

        [Fact]
        public async Task GetOrderById_InvalidId_ShouldReturnNotFound()
        {
            // Arrange
            _orderServiceMock.Setup(service => service.GetOrderById(999)).ReturnsAsync(new ActionResult<Order>(new NotFoundResult()));

            // Act
            var result = await _controller.GetOrderById(999);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetOrdersByUserId_ValidUserId_ShouldReturnOrders()
        {
            // Arrange
            var orders = new List<Order>
            {
                new Order { orderId = 1, userId = 101, totalPrice = 200, paymentStatus = true }
            };

            _orderServiceMock.Setup(service => service.GetOrdersByUserId(101)).ReturnsAsync(new ActionResult<IEnumerable<Order>>(orders));

            // Act
            var result = await _controller.GetOrdersByUserId(101);

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Order>>>(result);
            var value = Assert.IsAssignableFrom<IEnumerable<Order>>(actionResult.Value);
            Assert.Single(value);
        }

        [Fact]
        public async Task GetOrdersByUserId_InvalidUserId_ShouldReturnNotFound()
        {
            // Arrange
            _orderServiceMock.Setup(service => service.GetOrdersByUserId(999)).ReturnsAsync(new ActionResult<IEnumerable<Order>>(new NotFoundResult()));

            // Act
            var result = await _controller.GetOrdersByUserId(999);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PostOrder_ValidOrder_ShouldCreateOrder()
        {
            // Arrange
            var newOrder = new Order { orderId = 3, userId = 103, totalPrice = 300, paymentStatus = true };

            _orderServiceMock.Setup(service => service.PostOrder(newOrder))
                .ReturnsAsync(new CreatedAtActionResult("GetOrderById", null, new { orderId = newOrder.orderId }, newOrder));

            // Act
            var result = await _controller.PostOrder(newOrder);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var value = Assert.IsType<Order>(createdResult.Value);
            Assert.Equal(3, value.orderId);
        }

        [Fact]
        public async Task PutOrder_ValidOrder_ShouldUpdateOrder()
        {
            // Arrange
            var updatedOrder = new Order { orderId = 1, userId = 101, totalPrice = 250, paymentStatus = true };

            _orderServiceMock.Setup(service => service.PutOrder(1, updatedOrder))
                .ReturnsAsync(new OkResult());

            // Act
            var result = await _controller.PutOrder(1, updatedOrder);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task PutOrder_InvalidId_ShouldReturnBadRequest()
        {
            // Arrange
            var updatedOrder = new Order { orderId = 2, userId = 101, totalPrice = 250, paymentStatus = true };

            _orderServiceMock.Setup(service => service.PutOrder(1, updatedOrder))
                .ReturnsAsync(new BadRequestResult());

            // Act
            var result = await _controller.PutOrder(1, updatedOrder);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteOrder_ValidId_ShouldDeleteOrder()
        {
            // Arrange
            _orderServiceMock.Setup(service => service.DeleteOrder(1)).ReturnsAsync(new OkResult());

            // Act
            var result = await _controller.DeleteOrder(1);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task DeleteOrder_InvalidId_ShouldReturnNotFound()
        {
            // Arrange
            _orderServiceMock.Setup(service => service.DeleteOrder(999)).ReturnsAsync(new NotFoundResult());

            // Act
            var result = await _controller.DeleteOrder(999);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
