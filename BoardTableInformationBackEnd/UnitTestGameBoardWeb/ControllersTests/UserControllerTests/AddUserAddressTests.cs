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
    public class AddUserAddressTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly Mock<IAddressService> _addressServiceMock;
        private readonly UserController _controller;
        private readonly int _userId;

        public AddUserAddressTests()
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
        public async Task AddUserAddress_Returns_CreatedResult_With_True_Value()
        {
            // Arrange
            var address = new AddressCreateDto
            {
                Country = "USA",
                City = "New York",
                StreetName = "Broadway",
                Province = "NY",
                PostalCode = "10001",
                HouseNumber = 1234
            };
            _addressServiceMock.Setup(service => service.AddNewAddress(address))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.AddUserAddress(address);

            // Assert
            var createdResult = Assert.IsType<CreatedResult>(result);
            Assert.Equal(StatusCodes.Status201Created, createdResult.StatusCode);
            Assert.True((bool)createdResult.Value);
        }

        [Fact]
        public async Task AddUserAddress_Returns_BadRequest_When_AddNewAddress_Returns_False()
        {
            // Arrange
            var address = new AddressCreateDto
            {
                Country = "USA",
                City = "New York",
                StreetName = "Broadway",
                Province = "NY",
                PostalCode = "10001",
                HouseNumber = 1234
            };
            _addressServiceMock.Setup(service => service.AddNewAddress(address))
                .ReturnsAsync(false);

            // Act
            var result = await _controller.AddUserAddress(address);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestResult>(result);
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
        }

        [Fact]
        public async Task AddUserAddress_Returns_BadRequest_When_Model_State_Is_Invalid()
        {
            // Arrange
            var address = new AddressCreateDto(); // Required properties are not set

            // Arrange - Add an error to the ModelState to simulate invalid model state
            _controller.ModelState.AddModelError("Country", "The Country field is required.");

            // Act
            var result = await _controller.AddUserAddress(address);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }
    }
}
