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
    public class JointInvitationTests
    {
        private readonly Mock<IInvitationService> _mockInvitationService;
        private readonly InvitationController _controller;

        public JointInvitationTests()
        {
            _mockInvitationService = new Mock<IInvitationService>();
            _controller = new InvitationController(_mockInvitationService.Object);
        }

        [Fact]
        public async Task JointInvitation_ValidInvitation_ReturnsCreatedResult()
        {
            // Arrange
            var userId = 1;
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim("UserId", userId.ToString()) }));

            var invitation = new JoinInvitationDTO { SelectedActiveInvitation = 1 };

            // Act
            var result = await _controller.JointInvitation(invitation);

            // Assert
            Assert.IsType<CreatedResult>(result);
            var createdResult = (CreatedResult)result;
            Assert.Equal(invitation, createdResult.Value);
        }

        [Fact]
        public async Task JointInvitation_NullInvitation_ReturnsBadRequest()
        {
            // Arrange
            var userId = 1;
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim("UserId", userId.ToString()) }));

            JoinInvitationDTO invitation = null;

            // Act
            var result = await _controller.JointInvitation(invitation);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task JointInvitation_InvalidModelState_ReturnsBadRequest()
        {
            // Arrange
            var userId = 1;
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim("UserId", userId.ToString()) }));

            var invitation = new JoinInvitationDTO { SelectedActiveInvitation = -1 };
            _controller.ModelState.AddModelError("SelectedActiveInvitation", "The SelectedActiveInvitation field is required.");

            // Act
            var result = await _controller.JointInvitation(invitation);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }


        [Fact]
        public async Task JointInvitation_ValidInvitationAndUserId_ReturnsCreatedResult()
        {
            // Arrange
            var invitation = new JoinInvitationDTO { SelectedActiveInvitation = 1 };
            var userId = 1;
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim("UserId", userId.ToString()) }));

            // Act
            var result = await _controller.JointInvitation(invitation);

            // Assert
            Assert.IsType<CreatedResult>(result);
            var createdResult = (CreatedResult)result;
            Assert.Equal(invitation, createdResult.Value);
        }

        [Fact]
        public async Task JointInvitation_EmptyInvitation_ReturnsBadRequest()
        {
            // Arrange
            var userId = 1;
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
            _controller.ControllerContext.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim("UserId", userId.ToString()) }));

            var invitation = new JoinInvitationDTO();

            // Act
            var result = await _controller.JointInvitation(invitation);

            // Assert
            Assert.IsType<UnprocessableEntityObjectResult>(result);
        }
    }

}
