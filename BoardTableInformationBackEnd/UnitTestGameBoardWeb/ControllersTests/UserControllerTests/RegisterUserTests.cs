using BoardTableInformationBackEnd.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTO;
using Moq;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestGameBoardWeb.ControllersTests.UserControllerTests
{
    public class RegisterUserTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly Mock<IAddressService> _addressServiceMock;
        private readonly UserController _controller;
        private readonly int _userId;

        public RegisterUserTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _addressServiceMock = new Mock<IAddressService>();
            _controller = new UserController(_userServiceMock.Object, _addressServiceMock.Object);
            _userId = 1;
            var claims = new List<Claim> { new Claim("UserId", _userId.ToString()) };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var user = new ClaimsPrincipal(identity);
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };
        }

        [Fact]
        public async Task RegisterUser_ValidModel_ReturnsCreated()
        {
            // Arrange
            var model = new UserRegisterModel
            {
                UserName = "testuser",
                Email = "testuser@example.com",
                Password = "testpassword",
                RePassword = "testpassword"
            };
            var expectedUserView = new UserViewModel
            {
                Id = 1,
                Username = "testuser",
                Email = "testuser@example.com",
                RegistrationTime = DateTime.Now,
                LastTimeConnection = DateTime.Now,
                Role = "User",
                State = "Active"
            };
            _userServiceMock.Setup(x => x.RegisterUser(model)).ReturnsAsync(expectedUserView);

            // Act
            var result = await _controller.RegisterUser(model);

            // Assert
            var createdResult = Assert.IsType<CreatedResult>(result);
            var userView = Assert.IsType<UserViewModel>(createdResult.Value);
            Assert.Equal(expectedUserView, userView);
        }

        [Fact]
        public async Task RegisterUser_PasswordsDoNotMatch_ReturnsBadRequest()
        {
            // Arrange
            var model = new UserRegisterModel
            {
                UserName = "testuser",
                Email = "testuser@example.com",
                Password = "testpassword",
                RePassword = "wrongpassword"
            };

            // Act
            var result = await _controller.RegisterUser(model);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Password must match", badRequestResult.Value);
        }

        [Fact]
        public async Task RegisterUser_UsernameAlreadyExists_ReturnsBadRequest()
        {
            // Arrange
            var model = new UserRegisterModel
            {
                UserName = "testuser",
                Email = "testuser@example.com",
                Password = "testpassword",
                RePassword = "testpassword"
            };
            _userServiceMock.Setup(x => x.RegisterUser(model)).ReturnsAsync((UserViewModel)null);

            // Act
            var result = await _controller.RegisterUser(model);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Username already exists", badRequestResult.Value);
        }
    }
}
