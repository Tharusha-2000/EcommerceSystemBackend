using Xunit;
using Moq;
using Ecommerce.userManage.Application.Service;
using Ecommerce.userManage.Domain.Models;
using Ecommerce.userManage.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

public class UserControllerTests
{
    private readonly Mock<IUserService> _mockUserService;
    private readonly UserController _userController;

    public UserControllerTests()
    {
        _mockUserService = new Mock<IUserService>();
        _userController = new UserController(_mockUserService.Object);
    }

    [Fact]
    public void AddUser_ShouldReturnOkResult_WhenUserIsAddedSuccessfully()
    {
        // Arrange
        var userModel = new UserModel
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            UserType = "customer",
            PhoneNo = "123456789",
            Address = "123 Street"
        };

        _mockUserService.Setup(s => s.addUser(userModel));

        // Act
        var result = _userController.addUser(userModel);

        // Assert
        Assert.IsType<OkObjectResult>(result);
        Assert.Equal("user added successfully", ((OkObjectResult)result).Value);
    }

    [Fact]
    public void GetUserById_ShouldReturnUser_WhenUserExists()
    {
        // Arrange
        var userId = 1;
        var userList = new List<UserModel>
        {
            new UserModel { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" }
        };

        _mockUserService.Setup(s => s.getUserById(userId)).Returns(userList);

        // Act
        var result = _userController.getUserById(userId);

        // Assert
        Assert.IsType<OkObjectResult>(result);
        var returnedUser = ((OkObjectResult)result).Value as List<UserModel>;
        Assert.Single(returnedUser);
        Assert.Equal("John", returnedUser[0].FirstName);
    }

    [Fact]
    public void DeleteUser_ShouldReturnOkResult_WhenUserIsDeleted()
    {
        // Arrange
        var userId = 1;
        _mockUserService.Setup(s => s.deleteUser(userId));

        // Act
        var result = _userController.deleteUser(userId);

        // Assert
        Assert.IsType<OkObjectResult>(result);
        Assert.Equal("user deleted successfully", ((OkObjectResult)result).Value);
    }
}
