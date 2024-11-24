using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.OrderProcessing.API.Controllers;
using Ecommerce.OrderProcessing.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce.OrderProcessing.Tests.Controllers
{
    public class CartControllerTests
    {
        private readonly Mock<ICartSer> _mockCartService;
        private readonly CartController _controller;

        public CartControllerTests()
        {
            _mockCartService = new Mock<ICartSer>();
            _controller = new CartController(_mockCartService.Object);
        }

        [Fact]
        public async Task GetCarts_ReturnsListOfCarts()
        {
            // Arrange
            var carts = new List<Cart> { new Cart { cartId = 1, userId = 2, productId = 3, pizzaSize = "Large", count = 1 } };
            _mockCartService.Setup(s => s.GetCarts()).ReturnsAsync(new ActionResult<IEnumerable<Cart>>(carts));

            // Act
            var result = await _controller.GetCarts();
            var okResult = Assert.IsType<ActionResult<IEnumerable<Cart>>>(result);
            var value = Assert.IsAssignableFrom<IEnumerable<Cart>>(okResult.Value);

            // Assert
            Assert.Single(value);
        }

        [Fact]
        public async Task GetCartById_ValidId_ReturnsCart()
        {
            // Arrange
            var cart = new Cart { cartId = 1, userId = 2, productId = 3, pizzaSize = "Medium", count = 1 };
            _mockCartService.Setup(s => s.GetCartById(1)).ReturnsAsync(new ActionResult<Cart>(cart));

            // Act
            var result = await _controller.GetCartById(1);
            var okResult = Assert.IsType<ActionResult<Cart>>(result);
            var value = Assert.IsType<Cart>(okResult.Value);

            // Assert
            Assert.Equal(1, value.cartId);
        }

        [Fact]
        public async Task GetCartsByUserId_ReturnsCartsForUser()
        {
            // Arrange
            var carts = new List<Cart> { new Cart { cartId = 1, userId = 2, productId = 3, pizzaSize = "Small", count = 2 } };
            _mockCartService.Setup(s => s.GetCartsByUserId(2)).ReturnsAsync(new ActionResult<IEnumerable<Cart>>(carts));

            // Act
            var result = await _controller.GetCartsByUserId(2);
            var okResult = Assert.IsType<ActionResult<IEnumerable<Cart>>>(result);
            var value = Assert.IsAssignableFrom<IEnumerable<Cart>>(okResult.Value);

            // Assert
            Assert.Single(value);
        }

        [Fact]
        public async Task PostCart_ValidCart_ReturnsCreatedCart()
        {
            // Arrange
            var cart = new Cart { cartId = 1, userId = 2, productId = 3, pizzaSize = "Large", count = 1 };
            _mockCartService.Setup(s => s.PostCart(cart)).ReturnsAsync(new ActionResult<Cart>(cart));

            // Act
            var result = await _controller.PostCart(cart);
            var okResult = Assert.IsType<ActionResult<Cart>>(result);
            var value = Assert.IsType<Cart>(okResult.Value);

            // Assert
            Assert.Equal(1, value.cartId);
        }

        [Fact]
        public async Task PutCart_ValidCart_ReturnsOk()
        {
            // Arrange
            var cart = new Cart { cartId = 1, userId = 2, productId = 3, pizzaSize = "Medium", count = 1 };
            _mockCartService.Setup(s => s.PutCart(1, cart)).ReturnsAsync(new OkResult());

            // Act
            var result = await _controller.PutCart(1, cart);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task DeleteCart_ValidId_ReturnsOk()
        {
            // Arrange
            _mockCartService.Setup(s => s.DeleteCart(1)).ReturnsAsync(new OkResult());

            // Act
            var result = await _controller.DeleteCart(1);

            // Assert
            Assert.IsType<OkResult>(result);
        }
    }
}
