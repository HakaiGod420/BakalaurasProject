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
    public class UpdateUserNotificationsTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly Mock<IAddressService> _addressServiceMock;
        private readonly UserController _controller;
        private readonly int _userId;

        public UpdateUserNotificationsTests()
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
        public async Task UpdateUserNotifications_Success()
        {
            // Arrange
            var notification = new NotificationsListDto
            {
                Notifications = new List<NotificationSettingsDto>
        {
            new NotificationSettingsDto
            {
                Title = "New message notification",
                IsActive = true
            }
        }
            };
            _userServiceMock.Setup(x => x.UpdateNotifications(It.IsAny<int>(), It.IsAny<NotificationsListDto>()))
                            .ReturnsAsync(true);

            // Act
            var result = await _controller.UpdateUserNotifications(notification) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.IsType<bool>(result.Value);
            Assert.True((bool)result.Value);
        }

        // Test case for invalid notification settings
        [Fact]
        public async Task UpdateUserNotifications_Invalid_Notification()
        {
            // Arrange
            var notification = new NotificationsListDto
            {
                Notifications = new List<NotificationSettingsDto>
        {
            new NotificationSettingsDto
            {
                Title = "Invalid notification title",
                IsActive = true
            }
        }
            };
            _userServiceMock.Setup(x => x.UpdateNotifications(It.IsAny<int>(), It.IsAny<NotificationsListDto>()))
                            .ReturnsAsync(false);

            // Act
            var result = await _controller.UpdateUserNotifications(notification) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
            Assert.IsType<bool>(result.Value);
            Assert.False((bool)result.Value);
        }

    }
}
