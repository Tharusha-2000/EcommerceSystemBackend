using Ecommerce.OrderProcessing.API.Controllers;
using Ecommerce.OrderProcessing.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Ecommerce.OrderProcessing.Tests.Controllers
{
    public class OrderProductControllerTests
    {
        private readonly Mock<IOrderProductSer> _mockService;
        private readonly OrderProductController _controller;

        public OrderProductControllerTests()
        {
            _mockService = new Mock<IOrderProductSer>();
            _controller = new OrderProductController(_mockService.Object);
        }

        [Fact]
        public async Task GetOrderProducts_ReturnsAllOrderProducts()
        {
            // Arrange
            var mockOrderProducts = new List<OrderProduct>
            {
                new OrderProduct { orderProductId = 1, orderId = 101, productId = 1, pizzaSize = "Medium", count = 2 },
                new OrderProduct { orderProductId = 2, orderId = 102, productId = 2, pizzaSize = "Large", count = 1 }
            };
            _mockService.Setup(service => service.GetOrderProducts()).ReturnsAsync(new ActionResult<IEnumerable<OrderProduct>>(mockOrderProducts));

            // Act
            var result = await _controller.GetOrderProducts();

            // Assert
            var okResult = Assert.IsType<ActionResult<IEnumerable<OrderProduct>>>(result);
            var items = Assert.IsAssignableFrom<IEnumerable<OrderProduct>>(okResult.Value);
            Assert.Equal(2, ((List<OrderProduct>)items).Count);
        }

        [Fact]
        public async Task GetOrderProductById_ReturnsOrderProduct_WhenFound()
        {
            // Arrange
            var mockOrderProduct = new OrderProduct { orderProductId = 1, orderId = 101, productId = 1, pizzaSize = "Medium", count = 2 };
            _mockService.Setup(service => service.GetOrderProductById(1)).ReturnsAsync(new ActionResult<OrderProduct>(mockOrderProduct));

            // Act
            var result = await _controller.GetOrderProductById(1);

            // Assert
            var okResult = Assert.IsType<ActionResult<OrderProduct>>(result);
            Assert.Equal(mockOrderProduct, okResult.Value);
        }

        [Fact]
        public async Task GetOrderProductById_ReturnsNotFound_WhenNotFound()
        {
            // Arrange
            _mockService.Setup(service => service.GetOrderProductById(1)).ReturnsAsync(new NotFoundResult());

            // Act
            var result = await _controller.GetOrderProductById(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetOrderProductsByOrderId_ReturnsOrderProducts_WhenFound()
        {
            // Arrange
            var mockOrderProducts = new List<OrderProduct>
            {
                new OrderProduct { orderProductId = 1, orderId = 101, productId = 1, pizzaSize = "Medium", count = 2 }
            };
            _mockService.Setup(service => service.GetOrderProductsByOrderId(101)).ReturnsAsync(new ActionResult<IEnumerable<OrderProduct>>(mockOrderProducts));

            // Act
            var result = await _controller.GetOrderProductsByOrderId(101);

            // Assert
            var okResult = Assert.IsType<ActionResult<IEnumerable<OrderProduct>>>(result);
            var items = Assert.IsAssignableFrom<IEnumerable<OrderProduct>>(okResult.Value);
            Assert.Single(items);
        }

        [Fact]
        public async Task PostOrderProduct_CreatesOrderProduct()
        {
            // Arrange
            var mockOrderProduct = new OrderProduct { orderProductId = 1, orderId = 101, productId = 1, pizzaSize = "Medium", count = 2 };
            _mockService.Setup(service => service.PostOrderProduct(mockOrderProduct)).ReturnsAsync(new CreatedAtActionResult("GetOrderProductById", null, new { orderProductId = mockOrderProduct.orderProductId }, mockOrderProduct));

            // Act
            var result = await _controller.PostOrderProduct(mockOrderProduct);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal("GetOrderProductById", createdResult.ActionName);
            Assert.Equal(mockOrderProduct, createdResult.Value);
        }

        [Fact]
        public async Task PutOrderProduct_UpdatesOrderProduct_WhenIdMatches()
        {
            // Arrange
            var mockOrderProduct = new OrderProduct { orderProductId = 1, orderId = 101, productId = 1, pizzaSize = "Medium", count = 2 };
            _mockService.Setup(service => service.PutOrderProduct(1, mockOrderProduct)).ReturnsAsync(new OkResult());

            // Act
            var result = await _controller.PutOrderProduct(1, mockOrderProduct);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task PutOrderProduct_ReturnsBadRequest_WhenIdDoesNotMatch()
        {
            // Arrange
            var mockOrderProduct = new OrderProduct { orderProductId = 2, orderId = 101, productId = 1, pizzaSize = "Medium", count = 2 };
            _mockService.Setup(service => service.PutOrderProduct(1, mockOrderProduct)).ReturnsAsync(new BadRequestResult());

            // Act
            var result = await _controller.PutOrderProduct(1, mockOrderProduct);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteOrderProduct_RemovesOrderProduct_WhenFound()
        {
            // Arrange
            _mockService.Setup(service => service.DeleteOrderProduct(1)).ReturnsAsync(new OkResult());

            // Act
            var result = await _controller.DeleteOrderProduct(1);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task DeleteOrderProduct_ReturnsNotFound_WhenNotFound()
        {
            // Arrange
            _mockService.Setup(service => service.DeleteOrderProduct(1)).ReturnsAsync(new NotFoundResult());

            // Act
            var result = await _controller.DeleteOrderProduct(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
