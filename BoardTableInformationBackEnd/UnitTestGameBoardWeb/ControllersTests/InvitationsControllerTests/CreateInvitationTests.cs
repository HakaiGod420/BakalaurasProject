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
    public class CreateInvitationTests
    {
        private InvitationController _invitationController;
        private Mock<IInvitationService> _invitationServiceMock;
        private int _userId;

        public CreateInvitationTests()
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
        public async Task CreateInvitation_ValidData_ReturnsCreatedResult()
        {
            // Arrange
            var postInvitationDto = new PostInvatationDto
            {
                ActiveGameId = 1,
                PlayersNeed = 4,
                Map_X_Cords = 50.0f,
                Map_Y_Cords = 50.0f,
                MinimalAge = 18,
                PlayersNeeded = 3,
                InvitationDate = DateTime.Now.ToString(),
                Address = new AddressCreateDto
                {
                    Country = "USA",
                    City = "New York",
                    StreetName = "Broadway",
                    Province = "New York",
                    PostalCode = "12345",
                    HouseNumber = 123
                }
            };
            var id = 1;
            var createdInvitation = new PostInvatationDto
            {

                ActiveGameId = 1,
                PlayersNeed = 4,
                Map_X_Cords = 50.0f,
                Map_Y_Cords = 50.0f,
                MinimalAge = 18,
                PlayersNeeded = 3,
                InvitationDate = DateTime.Now.ToString(),
                Address = new AddressCreateDto
                {
                    Country = "USA",
                    City = "New York",
                    StreetName = "Broadway",
                    Province = "New York",
                    PostalCode = "12345",
                    HouseNumber = 123
                }
            };
            _invitationServiceMock.Setup(x => x.PostInvatation(postInvitationDto, id)).ReturnsAsync(createdInvitation);

            _invitationController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                    new Claim("UserId", id.ToString())
                    }, "TestAuthType"))
                }
            };

            // Act
            var result = await _invitationController.CreateInvitation(postInvitationDto);

            // Assert
            Assert.IsType<CreatedResult>(result);
            var createdResult = (CreatedResult)result;
            Assert.Equal(createdInvitation, createdResult.Value);
        }

        [Fact]
        public async Task CreateInvitation_InvalidData_ReturnsBadRequest()
        {
            // Arrange
            var postInvitationDto = new PostInvatationDto
            {
                ActiveGameId = -1,
                PlayersNeed = -1,
                Map_X_Cords = -91.0f,
                MinimalAge = -1,
                PlayersNeeded = -1,
                InvitationDate = "",
                Address = new AddressCreateDto
                {
                    Country = "",
                    City = "",
                    StreetName = "",
                    Province = "",
                    PostalCode = "",
                    HouseNumber = -1
                }
            };
            _invitationController.ModelState.AddModelError("ActiveGameId", "ActiveGameId is required");

            // Act
            var result = await _invitationController.CreateInvitation(postInvitationDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
