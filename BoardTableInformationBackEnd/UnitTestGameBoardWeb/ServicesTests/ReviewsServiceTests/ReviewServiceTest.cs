using DataLayer.Models;
using DataLayer.Repositories.Reviews;
using ModelLayer.DTO;
using Moq;
using ServiceLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestGameBoardWeb.ServicesTests.ReviewsServiceTests
{
    public class ReviewServiceTest
    {
        private readonly Mock<IReviewRepository> _mockRepository;
        private readonly ReviewService _reviewService;

        public ReviewServiceTest()
        {
            _mockRepository = new Mock<IReviewRepository>();
            _reviewService = new ReviewService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetReviews_ShouldReturnListOfReviews_WhenBoardGameIdIsValid()
        {
            // Arrange
            int boardGameId = 1;
            var expectedReviews = new List<ReviewView>
        {
            new ReviewView { ReviewId = 1, Username = "User1", Rating = 4 },
            new ReviewView { ReviewId = 2, Username = "User2", Rating = 3 },
            new ReviewView { ReviewId = 3, Username = "User3", Rating = 5 }
        };
            _mockRepository.Setup(r => r.GetAllReviews(boardGameId)).ReturnsAsync(expectedReviews);

            // Act
            var result = await _reviewService.GetReviews(boardGameId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<ReviewView>>(result);
            Assert.Equal(expectedReviews, result);
        }

        [Fact]
        public async Task GetReviews_ShouldReturnEmptyList_WhenBoardGameIdHasNoReviews()
        {
            // Arrange
            int boardGameId = 2;
            _mockRepository.Setup(r => r.GetAllReviews(boardGameId)).ReturnsAsync(new List<ReviewView>());

            // Act
            var result = await _reviewService.GetReviews(boardGameId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<ReviewView>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetReviews_ShouldThrowException_WhenRepositoryThrowsException()
        {
            // Arrange
            var boardGameId = 1;
            var exceptionMessage = "Repository exception";
            _mockRepository.Setup(r => r.GetAllReviews(boardGameId))
                                 .Throws(new Exception(exceptionMessage));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _reviewService.GetReviews(boardGameId));
            Assert.Equal(exceptionMessage, exception.Message);
        }

        [Fact]
        public async Task CreateReview_ShouldCreateReview_WhenValidInput()
        {
            // Arrange
            var review = new CreateReviewDto
            {
                BoardGameId = 1,
                Rating = 4,
                Comment = "Great game!"
            };
            var userId = 1;
            _mockRepository.Setup(r => r.CreateReview(It.IsAny<ReviewEntity>()))
                                  .Returns(Task.CompletedTask);

            // Act
            await _reviewService.CreateReview(review, userId);

            // Assert
            _mockRepository.Verify(r => r.CreateReview(It.IsAny<ReviewEntity>()), Times.Once);
        }

        [Fact]
        public async Task CreateReview_ShouldThrowException_WhenReviewEntityNotCreated()
        {
            // Arrange
            var review = new CreateReviewDto
            {
                BoardGameId = 1,
                Rating = 4,
                Comment = "Great game!"
            };
            var userId = 1;
            _mockRepository.Setup(r => r.CreateReview(It.IsAny<ReviewEntity>()))
                                  .Throws(new Exception());

            // Act and Assert
            await Assert.ThrowsAsync<Exception>(async () => await _reviewService.CreateReview(review, userId));
        }

    }
}
