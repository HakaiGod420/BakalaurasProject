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
    public class LoginUserTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly Mock<IAddressService> _addressServiceMock;
        private readonly UserController _controller;
        private readonly int _userId;

        public LoginUserTests()
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
        public async Task LoginUser_ValidModel_ReturnsOkResult()
        {
            // Arrange
            var loginModel = new LoginUserModel
            {
                UserName = "testuser",
                Password = "password"
            };

            _userServiceMock.Setup(x => x.Login(loginModel)).ReturnsAsync("token");

            // Act
            var result = await _controller.LoginUser(loginModel);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.Equal("token", okResult.Value);
        }

        [Fact]
        public async Task LoginUser_InvalidModel_ReturnsBadRequestResult()
        {
            // Arrange
            var loginModel = new LoginUserModel();

            // Act
            var result = await _controller.LoginUser(loginModel);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task LoginUser_NullResult_ReturnsBadRequestResult()
        {
            // Arrange
            var loginModel = new LoginUserModel
            {
                UserName = "testuser",
                Password = "password"
            };

            _userServiceMock.Setup(x => x.Login(loginModel)).ReturnsAsync((string)null);

            // Act
            var result = await _controller.LoginUser(loginModel);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
    }
}
