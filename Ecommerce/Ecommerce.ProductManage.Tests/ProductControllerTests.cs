using AutoFixture;
using Ecommerce.ProductManage.Api.Controllers;
using Ecommerce.ProductManage.Application.Services;
using Ecommerce.ProductManage.Domain.Models.Domains;
using Ecommerce.ProductManage.Domain.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Ecommerce.ProductManage.Test
{
    public class ProductControllerTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IProductService> _productServiceMock;
        private readonly ProductController _productControllerMock;

        public ProductControllerTests()
        {
            _fixture = new Fixture();
            _productServiceMock = _fixture.Freeze<Mock<IProductService>>();
            _productControllerMock = new ProductController(_productServiceMock.Object);
        }

        [Fact]
        public async Task GetAllProductsAsync_ShouldReturnOk_WhenProductsExist()
        {
            // Arrange
            var mockProducts = new List<Product>
            {
                new Product { ProductId = 1, Name = "Product1" },
                new Product { ProductId = 2, Name = "Product2" }
            };
            _productServiceMock.Setup(service => service.GetAllProductsAsync(null, null, null))
                .ReturnsAsync(mockProducts);

            // Act
            var result = await _productControllerMock.GetAllProductsAsync(null, null, null);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var products = Assert.IsType<List<Product>>(okResult.Value);
            Assert.Equal(2, products.Count);
        }

        [Fact]
        public async Task GetAllProductsAsync_ShouldReturnOk_WhenFilteredProductsExist()
        {
            // Arrange
            var mockProducts = new List<Product>
            {
                new Product { ProductId = 1, Name = "Product1", Categories = ["Chicken", "Cheese"], Sizes = [new ProductSize { ProductsizeId = 1, Size = "S", Price = 850 }]},
                new Product { ProductId = 2, Name = "Product2", Categories = ["Chicken", "Cheese"], Sizes = [new ProductSize { ProductsizeId = 2, Size = "S", Price = 800 }] }
            };
            _productServiceMock.Setup(service => service.GetAllProductsAsync(800, 900, new List<string> { "Chicken", "Cheese" }))
                .ReturnsAsync(mockProducts);

            // Act
            var result = await _productControllerMock.GetAllProductsAsync(800, 900, "Chicken,Cheese");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var products = Assert.IsType<List<Product>>(okResult.Value);
            Assert.Equal(2, products.Count);
        }

        [Fact]
        public async Task GetProductByIdAsync_ShouldReturnNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            _productServiceMock.Setup(service => service.GetProductByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Product)null);

            // Act
            var result = await _productControllerMock.GetProductByIdAsync(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetProductByIdAsync_ShouldReturnEmptyList_WhenFilteredProductsDoesNotExist()
        {
            // Arrange
            var mockProducts = new List<Product>
            {
                new Product { ProductId = 1, Name = "Product1" },
                new Product { ProductId = 2, Name = "Product2" }
            };
            _productServiceMock.Setup(service => service.GetAllProductsAsync(null, null, null))
                .ReturnsAsync(mockProducts);

            // Act
            var result = await _productControllerMock.GetAllProductsAsync(0, 50, "Cheese");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Empty(okResult.ContentTypes);
        }

        [Fact]
        public async Task GetProductByIdAsync_ShouldReturnOk_WhenProductExists()
        {
            // Arrange
            var mockProduct = new Product { ProductId = 1, Name = "Product1" };
            _productServiceMock.Setup(service => service.GetProductByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(mockProduct);

            // Act
            var result = await _productControllerMock.GetProductByIdAsync(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var product = Assert.IsType<Product>(okResult.Value);
            Assert.Equal(1, product.ProductId);
        }

        [Fact]
        public async Task GetProductsByCategory_ShouldReturnBadRequest_WhenCategoryIsEmpty()
        {
            // Act
            var result = await _productControllerMock.GetProductsByCategory("");

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Category cannot be null or empty.", badRequestResult.Value);
        }

        [Fact]
        public async Task CreateProductAsync_ShouldReturnOk_WhenProductIsCreated()
        {
            // Arrange
            var mockProductDto = new ProductDto { Name = "Product1" };
            var mockProduct = new Product { ProductId = 1, Name = "Product1" };
            _productServiceMock.Setup(service => service.CreateProductAsync(mockProductDto))
                .ReturnsAsync(mockProduct);

            // Act
            var result = await _productControllerMock.CreateProductAsync(mockProductDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var product = Assert.IsType<Product>(okResult.Value);
            Assert.Equal(1, product.ProductId);
        }

        [Fact]
        public async Task UpdateProductAsync_ShouldReturnNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            var mockProductDto = new ProductDto { Name = "UpdatedProduct" };
            _productServiceMock.Setup(service => service.UpdateProductAsync(It.IsAny<int>(), mockProductDto))
                .ReturnsAsync((Product)null);

            // Act
            var result = await _productControllerMock.UpdateProductAsync(1, mockProductDto);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteProductAsync_ShouldReturnNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            _productServiceMock.Setup(service => service.DeleteProductAsync(It.IsAny<int>()))
                .ReturnsAsync((Product)null);

            // Act
            var result = await _productControllerMock.DeleteProdcutAsync(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteProductAsync_ShouldReturnOk_WhenProductIsDeleted()
        {
            // Arrange
            var mockProduct = new Product { ProductId = 1, Name = "Product1" };
            _productServiceMock.Setup(service => service.DeleteProductAsync(It.IsAny<int>()))
                .ReturnsAsync(mockProduct);

            // Act
            var result = await _productControllerMock.DeleteProdcutAsync(1);

            // Assert
            Assert.IsType<OkResult>(result);
        }
    }
}