using BoardTableInformationBackEnd.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTO;
using Moq;
using ServiceLayer.Interfaces;
using System.Security.Claims;

namespace UnitTestGameBoardWeb.ControllersTests.ReviewControllerTests
{
    public class ReviewCreateTests
    {
        private readonly ReviewController _reviewController;
        private readonly Mock<IReviewService> _reviewServiceMock;

        public ReviewCreateTests()
        {
            _reviewServiceMock = new Mock<IReviewService>();
            _reviewController = new ReviewController(_reviewServiceMock.Object);
            var userId = 1;
            var claims = new[] { new Claim("UserId", userId.ToString()) };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var user = new ClaimsPrincipal(identity);
            _reviewController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };
        }

        [Fact]
        public async Task CreateReview_ValidReview_CallsCreateReviewWithCorrectParametersAndReturnsOkResult()
        {
            // Arrange
            var createReviewDto = new CreateReviewDto { BoardGameId = 1, Rating = 4 };
            var expectedUserId = 1;
            var expectedCreateReviewDto = new CreateReviewDto { BoardGameId = 1, Rating = 4 };
            _reviewServiceMock.Setup(x => x.CreateReview(expectedCreateReviewDto, expectedUserId));

            // Act
            var result = await _reviewController.CreateReview(createReviewDto);

            // Assert
            _reviewServiceMock.Verify();
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task CreateReview_RatingOutOfRange_ReturnsUnprocessableEntityResult()
        {
            // Arrange
            var createReviewDto = new CreateReviewDto { BoardGameId = 1, Rating = -1 };

            // Act
            var result = await _reviewController.CreateReview(createReviewDto);

            // Assert
            var unprocessableEntityObjectResult = Assert.IsType<UnprocessableEntityObjectResult>(result);
            Assert.Equal(nameof(createReviewDto.Rating), unprocessableEntityObjectResult.Value);
        }
    }
}
