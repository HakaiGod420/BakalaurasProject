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
    public class UpdateInvitationStateTests
    {
        private readonly InvitationController _invitationController;
        private readonly Mock<IInvitationService> _mockInvitationService;

        public UpdateInvitationStateTests()
        {
            _mockInvitationService = new Mock<IInvitationService>();
            _invitationController = new InvitationController(_mockInvitationService.Object);
            _invitationController.ControllerContext = new ControllerContext();
            _invitationController.ControllerContext.HttpContext = new DefaultHttpContext();
            _invitationController.ControllerContext.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
            new Claim("UserId", "1")
            }));
        }

        [Fact]
        public async Task UpdateInvitationState_ValidData_ReturnsOkResult()
        {
            // Arrange
            var data = new InvitationStateChangeDto()
            {
                InvitationId = 1,
                State = "Accepted"
            };

            _mockInvitationService.Setup(x => x.ChangeInvitationState(It.IsAny<InvitationStateChangeDto>())).Verifiable();

            // Act
            var result = await _invitationController.UpdateInvitationState(data);

            // Assert
            Assert.IsType<OkResult>(result);
            _mockInvitationService.Verify();
        }

        [Fact]
        public async Task UpdateInvitationState_InvalidData_ReturnsBadRequestResult()
        {
            // Arrange
            var data = new InvitationStateChangeDto()
            {
                InvitationId = 1,
                State = null // invalid state
            };

            // Act
            var result = await _invitationController.UpdateInvitationState(data);

            // Assert
            Assert.IsType<UnprocessableEntityObjectResult>(result);
        }

    }
}
