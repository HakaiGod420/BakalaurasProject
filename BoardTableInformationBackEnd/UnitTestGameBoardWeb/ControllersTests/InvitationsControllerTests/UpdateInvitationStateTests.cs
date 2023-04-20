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
        private readonly int _userId;

        public UpdateInvitationStateTests()
        {
            _mockInvitationService = new Mock<IInvitationService>();
            _invitationController = new InvitationController(_mockInvitationService.Object);
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
