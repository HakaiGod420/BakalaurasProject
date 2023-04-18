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
    public class GetUserSettingsTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly Mock<IAddressService> _addressServiceMock;
        private readonly UserController _controller;
        private readonly int _userId;

        public GetUserSettingsTests()
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
        public async Task GetUserSettings_ReturnsOkObjectResult_WhenValidUserId()
        {
            // Arrange
            var userSettings = new UserSettings
            {
                Address = new UserAddressDto
                {
                    Country = "TestCountry",
                    City = "TestCity",
                    StreetName = "TestStreetName",
                    Province = "TestProvince",
                    Map_X_Coords = 123.45,
                    Map_Y_Coords = 67.89
                },
                EnabledInvitationSettings = true
            };
            _userServiceMock.Setup(x => x.GetUserSettings(_userId)).ReturnsAsync(userSettings);

            // Act
            var result = await _controller.GetUserSettings();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedSettings = Assert.IsType<UserSettings>(okResult.Value);
            Assert.Equal(userSettings.Address.Country, returnedSettings.Address.Country);
            Assert.Equal(userSettings.Address.City, returnedSettings.Address.City);
            Assert.Equal(userSettings.Address.StreetName, returnedSettings.Address.StreetName);
            Assert.Equal(userSettings.Address.Province, returnedSettings.Address.Province);
            Assert.Equal(userSettings.Address.Map_X_Coords, returnedSettings.Address.Map_X_Coords);
            Assert.Equal(userSettings.Address.Map_Y_Coords, returnedSettings.Address.Map_Y_Coords);
            Assert.Equal(userSettings.EnabledInvitationSettings, returnedSettings.EnabledInvitationSettings);
        }

        [Fact]
        public async Task GetUserSettings_ReturnsStatusCode500_WhenUserServiceReturnsNull()
        {
            // Arrange
            _userServiceMock.Setup(x => x.GetUserSettings(_userId)).ReturnsAsync((UserSettings)null);

            // Act
            var result = await _controller.GetUserSettings();

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
