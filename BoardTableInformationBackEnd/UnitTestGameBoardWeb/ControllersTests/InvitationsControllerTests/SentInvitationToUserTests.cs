using BoardTableInformationBackEnd.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTO;
using Moq;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestGameBoardWeb.ControllersTests.InvitationsControllerTests
{
    public class SentInvitationToUserTests
    {
        private readonly InvitationController _invitationController;
        private readonly Mock<IInvitationService> _invitationServiceMock;

        public SentInvitationToUserTests()
        {
            _invitationServiceMock = new Mock<IInvitationService>();
            _invitationController = new InvitationController(_invitationServiceMock.Object);
        }

        [Fact]
        public async Task SentInvitationToUser_ValidData_ReturnsCreatedResult()
        {
            // Arrange
            var invitation = new SingeUserSentInvitationDTO
            {
                UserName = "testuser",
                ActiveInvitationId = 1
            };

            // Act
            var result = await _invitationController.SentInvitationToUser(invitation);

            // Assert
            Assert.IsType<CreatedResult>(result);
        }

        [Fact]
        public async Task SentInvitationToUser_NullInvitation_ReturnsBadRequest()
        {
            // Arrange
            SingeUserSentInvitationDTO invitation = null;

            // Act
            var result = await _invitationController.SentInvitationToUser(invitation);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task SentInvitationToUser_InvalidData_ReturnsBadRequest()
        {
            // Arrange
            var invitation = new SingeUserSentInvitationDTO
            {
                UserName = "",
                ActiveInvitationId = -1
            };

            // Act
            var result = await _invitationController.SentInvitationToUser(invitation);

            // Assert
            Assert.IsType<UnprocessableEntityObjectResult>(result);
        }
    }
}
