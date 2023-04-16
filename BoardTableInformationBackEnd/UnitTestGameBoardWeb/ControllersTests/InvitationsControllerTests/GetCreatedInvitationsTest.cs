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

namespace UnitTestGameBoardWeb.ControllersTests.InvitationsControllerTests
{
    public class GetCreatedInvitationsTest
    {
        private readonly Mock<IInvitationService> _invitationServiceMock;
        private readonly InvitationController _invitationController;

        public GetCreatedInvitationsTest()
        {
            _invitationServiceMock = new Mock<IInvitationService>();
            _invitationController = new InvitationController(_invitationServiceMock.Object);
        }

        [Fact]
        public async Task GetInvitations_ReturnsOkObjectResult_WhenInvitationsExist()
        {
            // Arrange
            var userId = 1;
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("UserId", userId.ToString())
            }, "mock"));

            _invitationController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            var invitations = new List<UserInvitationDto>
            {
                new UserInvitationDto { InvitationId = 1, BoardGameTitle = "Game 1" },
                new UserInvitationDto { InvitationId = 2, BoardGameTitle = "Game 2" }
            };
            _invitationServiceMock.Setup(svc => svc.GetCreatedInvitations(userId)).ReturnsAsync(invitations);

            // Act
            var result = await _invitationController.GetCreatedInvitations();

            // Assert
            Assert.IsType<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);

            var model = okResult.Value as List<UserInvitationDto>;
            Assert.NotNull(model);
            Assert.Equal(invitations, model);
        }

        [Fact]
        public async Task GetInvitations_ReturnsOkObjectResult_WhenNoInvitationsExist()
        {
            // Arrange
            var userId = 1;
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("UserId", userId.ToString())
            }, "mock"));

            _invitationController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            _invitationServiceMock.Setup(svc => svc.GetActiveInvitations(userId)).ReturnsAsync(new List<UserInvitationDto>());

            // Act
            var result = await _invitationController.GetActiveInvitations();

            // Assert
            Assert.IsType<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);

            var model = okResult.Value as List<UserInvitationDto>;
            Assert.NotNull(model);
            Assert.Empty(model);
        }
    }
}
