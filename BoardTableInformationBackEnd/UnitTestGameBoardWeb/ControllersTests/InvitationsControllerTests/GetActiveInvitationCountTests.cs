using BoardTableInformationBackEnd.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestGameBoardWeb.ControllersTests.InvitationsControllerTests
{
    public class GetActiveInvitationCountTests
    {
        private readonly Mock<IInvitationService> _invitationServiceMock;
        private readonly InvitationController _invitationController;
        private int _userId;

        public GetActiveInvitationCountTests()
        {
            _invitationServiceMock = new Mock<IInvitationService>();
            _invitationController = new InvitationController(_invitationServiceMock.Object);
            _userId = 1;
            var claims = new List<Claim> { new Claim("UserId", _userId.ToString()) };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var user = new ClaimsPrincipal(identity);
            _invitationController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };
        }

        [Fact]
        public async Task GetActiveInvitationCount_InvitationCountIsZero_ReturnsZero()
        {
            // Arrange
            var userId = 1;
            _invitationServiceMock.Setup(x => x.ActiveInvitationCount(userId)).ReturnsAsync(0);

            // Act
            var result = await _invitationController.GetActiveInvitationCount();

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.Equal(0, okResult.Value);
        }

        [Fact]
        public async Task GetActiveInvitationCount_InvitationCountIsGreaterThanZero_ReturnsCount()
        {
            // Arrange
            var userId = 1;
            var count = 5;
            _invitationServiceMock.Setup(x => x.ActiveInvitationCount(userId)).ReturnsAsync(count);

            // Act
            var result = await _invitationController.GetActiveInvitationCount();

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.Equal(count, okResult.Value);
        }

    }
}
