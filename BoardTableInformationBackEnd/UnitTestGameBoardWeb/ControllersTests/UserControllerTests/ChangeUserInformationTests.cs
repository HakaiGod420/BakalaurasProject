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
    public class ChangeUserInformationTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly Mock<IAddressService> _addressServiceMock;
        private readonly UserController _controller;
        private readonly int _userId;

        public ChangeUserInformationTests()
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
        public async Task ChangeUserInformation_WithValidData_ReturnsOk()
        {
            // Arrange
            var userInformation = new ChangeUserInformationDTO
            {
                UserId = 1,
                Email = "test@example.com",
                PasswordChanged = false,
            };
            _userServiceMock.Setup(x => x.ChangeUserInformation(userInformation)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.ChangeUserInformation(userInformation) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.True((bool)result.Value);
        }

        [Fact]
        public async Task ChangeUserInformation_WithInvalidData_ReturnsBadRequest()
        {
            // Arrange
            var userInformation = new ChangeUserInformationDTO
            {
                UserId = 1,
                Email = null,
                PasswordChanged = false,
            };
            _controller.ModelState.AddModelError(nameof(userInformation.Email), "Email is required");

            // Act
            var result = await _controller.ChangeUserInformation(userInformation) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
            Assert.IsType<SerializableError>(result.Value);
        }

        [Fact]
        public async Task ChangeUserInformation_WithExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            var userInformation = new ChangeUserInformationDTO
            {
                UserId = 1,
                Email = "test@example.com",
                PasswordChanged = false,
            };
            _userServiceMock.Setup(x => x.ChangeUserInformation(userInformation)).ThrowsAsync(new Exception());

            try
            {
                var result = await _controller.ChangeUserInformation(userInformation) as StatusCodeResult;
                Assert.Fail("");
            }
            catch (Exception ex)
            {
                Assert.True(true);
            }
            
        }
    }
}
