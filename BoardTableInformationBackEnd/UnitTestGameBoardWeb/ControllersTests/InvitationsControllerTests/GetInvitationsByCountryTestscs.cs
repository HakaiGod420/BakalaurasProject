using BoardTableInformationBackEnd.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class GetInvitationsByCountryTestscs
    {
        private readonly InvitationController _controller;
        private readonly Mock<IInvitationService> _invitationServiceMock;

        public GetInvitationsByCountryTestscs()
        {
            _invitationServiceMock = new Mock<IInvitationService>();
            _controller = new InvitationController(_invitationServiceMock.Object);
        }

        [Fact]
        public async Task GetInvitationsByCountry_NullCountry_ReturnsBadRequest()
        {
            // Arrange
            const string country = null;
            const int pageIndex = 1;
            const int pageSize = 10;

            // Act
            var response = await _controller.GetInvitationsByCountry(country, pageIndex, pageSize) as BadRequestResult;

            // Assert
            Assert.NotNull(response);
            Assert.Equal(StatusCodes.Status400BadRequest, response.StatusCode);
        }

        [Theory]
        [InlineData(-1, 10)]
        [InlineData(1, -10)]
        [InlineData(-1, -10)]
        public async Task GetInvitationsByCountry_InvalidPageIndexOrPageSize_ReturnsBadRequest(int pageIndex, int pageSize)
        {
            // Arrange
            const string country = "Canada";

            // Act
            var response = await _controller.GetInvitationsByCountry(country, pageIndex, pageSize) as BadRequestResult;

            // Assert
            Assert.NotNull(response);
            Assert.Equal(StatusCodes.Status400BadRequest, response.StatusCode);
        }
    }
}
