using BoardTableInformationBackEnd.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTO;
using Moq;
using ServiceLayer.Interfaces;
using ServiceLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestGameBoardWeb.ControllersTests.UserControllerTests
{
    public class UpdateUserAddressTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly Mock<IAddressService> _addressServiceMock;
        private readonly UserController _controller;
        private readonly int _userId;

        public UpdateUserAddressTests()
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
        public async Task UpdateUserAddress_ModelStateInvalid_ReturnsBadRequest()
        {
            // Arrange
            _controller.ModelState.AddModelError("Address", "The Address field is required");
            var address = new UpdateUserAddress
            {
                Address = new UserAddressDto(),
                EnabledInvitationSettings = true
            };

            // Act
            var result = await _controller.UpdateUserAddress(address);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task UpdateUserAddress_AddressUpdateFails_ReturnsStatusCode500()
        {
            var address = new UpdateUserAddress
            {
                Address = new UserAddressDto(),
                EnabledInvitationSettings = true
            };

            _addressServiceMock.Setup(s => s.UpdateUserAddress(It.IsAny<int>(), It.IsAny<UpdateUserAddress>())).ReturnsAsync(false);

            // Act
            var result = await _controller.UpdateUserAddress(address);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task UpdateUserAddress_AddressUpdateSucceeds_ReturnsOk()
        {
            // Arrange
            var address = new UpdateUserAddress
            {
                Address = new UserAddressDto(),
                EnabledInvitationSettings = true
            };
            _addressServiceMock.Setup(s => s.UpdateUserAddress(It.IsAny<int>(), It.IsAny<UpdateUserAddress>())).ReturnsAsync(true);

            // Act
            var result = await _controller.UpdateUserAddress(address);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okObjectResult = result as OkObjectResult;
            Assert.IsType<bool>(okObjectResult.Value);
            Assert.True((bool)okObjectResult.Value);
        }
    }
}
