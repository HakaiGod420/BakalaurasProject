using BoardTableInformationBackEnd.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTO;
using Moq;
using ServiceLayer.Interfaces;
using System.Security.Claims;

namespace UnitTestGameBoardWeb.ControllersTests.ReviewControllerTests
{
    public class GetReviewsTests
    {
        private readonly ReviewController _reviewController;
        private readonly Mock<IReviewService> _reviewServiceMock = new Mock<IReviewService>();

        public GetReviewsTests()
        {
            _reviewController = new ReviewController(_reviewServiceMock.Object);
        }

        [Fact]
        public async Task GetReviews_ValidInput_ReturnsOkObjectResult()
        {
            // Arrange
            int boardGameId = 1;
            List<ReviewView> expectedReviews = new List<ReviewView>
            {
                new ReviewView
                {
                    ReviewId = 1,
                    ProfileImage = "profile.jpg",
                    Username = "user1",
                    CreatorId = 1,
                    ReviewText = "Great game!",
                    Rating = 4,
                    Written = DateTime.Now.AddDays(-2)
                },
                new ReviewView
                {
                    ReviewId = 2,
                    ProfileImage = "profile2.jpg",
                    Username = "user2",
                    CreatorId = 2,
                    ReviewText = "Awesome game!",
                    Rating = 5,
                    Written = DateTime.Now.AddDays(-1)
                }
            };

            _reviewServiceMock.Setup(x => x.GetReviews(boardGameId))
                .ReturnsAsync(expectedReviews);

            // Act
            var result = await _reviewController.GetReviews(boardGameId);

            // Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var actualReviews = Assert.IsAssignableFrom<List<ReviewView>>(okObjectResult.Value);
            Assert.Equal(expectedReviews.Count, actualReviews.Count);
            for (int i = 0; i < expectedReviews.Count; i++)
            {
                Assert.Equal(expectedReviews[i].ReviewId, actualReviews[i].ReviewId);
                Assert.Equal(expectedReviews[i].ProfileImage, actualReviews[i].ProfileImage);
                Assert.Equal(expectedReviews[i].Username, actualReviews[i].Username);
                Assert.Equal(expectedReviews[i].CreatorId, actualReviews[i].CreatorId);
                Assert.Equal(expectedReviews[i].ReviewText, actualReviews[i].ReviewText);
                Assert.Equal(expectedReviews[i].Rating, actualReviews[i].Rating);
                Assert.Equal(expectedReviews[i].Written, actualReviews[i].Written);
            }
        }

        [Fact]
        public async Task GetReviews_InvalidInput_ReturnsUnprocessableEntity()
        {
            // Arrange
            int boardGameId = -1;

            // Act
            var result = await _reviewController.GetReviews(boardGameId);

            // Assert
            var unprocessableEntityResult = Assert.IsType<UnprocessableEntityObjectResult>(result);
            Assert.Equal(nameof(boardGameId), unprocessableEntityResult.Value);
        }
    }
}
