using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;
using Ecommerce.userManage.Api.Controllers;
using Ecommerce.userManage.Domain.Models.DTO;
using Ecommerce.userManage.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce.userManage.Tests
{
    public class AuthControllerTests
    {
        private readonly Mock<UserManager<IdentityUser>> _userManagerMock;
        private readonly Mock<ITokenRepository> _tokenRepositoryMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            var userStoreMock = new Mock<IUserStore<IdentityUser>>();
            _userManagerMock = new Mock<UserManager<IdentityUser>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);
            _tokenRepositoryMock = new Mock<ITokenRepository>();
            _configurationMock = new Mock<IConfiguration>();
            _controller = new AuthController(_userManagerMock.Object, _tokenRepositoryMock.Object, _configurationMock.Object);
        }

      

        [Fact]
        public async Task Login_InvalidUser_ReturnsBadRequest()
        {
            // Arrange
            var loginDto = new LoginRequestDto
            {
                Username = "nonexistentuser@example.com",
                Password = "Password123!"
            };
            _userManagerMock.Setup(um => um.FindByNameAsync(loginDto.Username))
                .ReturnsAsync((IdentityUser)null);

            // Act
            var result = await _controller.Login(loginDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Username or password incorrect", badRequestResult.Value.ToString());
        }

       
    }
}
