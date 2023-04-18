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
    public class GetUserInformationTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly Mock<IAddressService> _addressServiceMock;
        private readonly UserController _controller;
        private readonly int _userId;

        public GetUserInformationTests()
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
        public async Task GetUserInformation_ReturnsOkObjectResult_WhenUserExists()
        {
            // Arrange
            string userName = "testUser";
            var userInformation = new UserInformationDTO
            {
                UserId = 1,
                UserName = "testUser",
                Role = "user",
                InvititationsCreated = 0,
                TableTopGamesCreated = 0,
                State = "active",
                RegisteredOn = DateTime.Now.AddDays(-7),
                LastLogin = DateTime.Now.AddMinutes(-30)
            };

            _userServiceMock.Setup(x => x.GetUserInformation(userName)).ReturnsAsync(userInformation);

            // Act
            var result = await _controller.GetUserInformation(userName);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<UserInformationDTO>(okResult.Value);
            Assert.Equal(userInformation.UserId, model.UserId);
            Assert.Equal(userInformation.UserName, model.UserName);
            Assert.Equal(userInformation.Role, model.Role);
            Assert.Equal(userInformation.InvititationsCreated, model.InvititationsCreated);
            Assert.Equal(userInformation.TableTopGamesCreated, model.TableTopGamesCreated);
            Assert.Equal(userInformation.State, model.State);
            Assert.Equal(userInformation.RegisteredOn, model.RegisteredOn);
            Assert.Equal(userInformation.LastLogin, model.LastLogin);
        }

        [Fact]
        public async Task GetUserInformation_ReturnsNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            string userName = "nonExistingUser";
            _userServiceMock.Setup(x => x.GetUserInformation(userName)).ReturnsAsync((UserInformationDTO)null);

            // Act
            var result = await _controller.GetUserInformation(userName);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
