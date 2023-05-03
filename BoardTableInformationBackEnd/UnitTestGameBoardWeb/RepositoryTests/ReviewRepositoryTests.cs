using DataLayer.DBContext;
using DataLayer.Models;
using DataLayer.Repositories.Reviews;
using Microsoft.EntityFrameworkCore;


namespace UnitTestGameBoardWeb.RepositoryTests
{
    public class ReviewRepositoryTests
    {
        private readonly ReviewRepository _repository;
        private readonly DbContextOptionsBuilder<DataBaseContext> _optionsBuilder;
        private readonly DataBaseContext _context;

        public ReviewRepositoryTests()
        {
            _optionsBuilder = new DbContextOptionsBuilder<DataBaseContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase");
            _context = new DataBaseContext(_optionsBuilder.Options);

            _repository = new ReviewRepository(_context);
        }

        [Fact]
        public async Task CreateReview_WithValidData_ShouldAddReviewToDatabase()
        {

            var review = new ReviewEntity
            {
                WriterId = 1,
                WriteDate = DateTime.Now,
                Rating = 5,
                SelectedBoardGameId = 1
            };

            // Act
            await _repository.CreateReview(review);

            // Assert
            var result = await _context.Reviews.FindAsync(review.ReviewId);

            Assert.NotNull(result);
            Assert.Equal(review.WriterId, result.WriterId);
            Assert.Equal(review.WriteDate, result.WriteDate);
            Assert.Equal(review.Rating, result.Rating);
            Assert.Equal(review.SelectedBoardGameId, result.SelectedBoardGameId);
        }

        [Fact]
        public async Task CreateReview_WithNullReview_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _repository.CreateReview(null));
        }

        [Fact]
        public async Task GetAllReviews_BoardGameListIsEmpty_ReturnsEmptyList()
        {
            // Arrange
            var boardGameId = 1;

            // Act
            var result = await _repository.GetAllReviews(boardGameId);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllReviews_BoardGameIdIsNegative_ThrowsArgumentException()
        {
            // Arrange
            var boardGameId = -1;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _repository.GetAllReviews(boardGameId));
        }

        [Fact]
        public async Task GetAllReviews_ReviewsFound_ReturnsListWithReviewViews()
        {
            // Arrange
            _context.Database.EnsureDeleted();
            var boardGameId = 1;
            var writer = new UserEntity {
                UserId = 1, UserName = "testuser",
                Email ="TestEmail@gmail.com",
                Password = new byte[9],
                PasswordSalt = new byte[50],
                RegistrationTime = DateTime.Now, LastTimeConnection = DateTime.Now
            };

            _context.Users.Add(writer);
            await _context.SaveChangesAsync();

            var reviews = new List<ReviewEntity>
            {
                new ReviewEntity { ReviewId = 1, WriterId = writer.UserId, Comment = "Test review 1", Rating = 4, WriteDate = new DateTime(2022, 1, 1), SelectedBoardGameId = boardGameId },
                new ReviewEntity { ReviewId = 2, WriterId = writer.UserId, Comment = "Test review 2", Rating = 3, WriteDate = new DateTime(2022, 2, 1), SelectedBoardGameId = boardGameId }
            };

            _context.Reviews.AddRange(reviews);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetAllReviews(boardGameId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(reviews.Count, result.Count);
            Assert.Equal(reviews[0].ReviewId, result[0].ReviewId);
            Assert.Null(result[0].ProfileImage);
            Assert.Equal(writer.UserName, result[0].Username);
            Assert.Equal(writer.UserId, result[0].CreatorId);
            Assert.Equal(reviews[0].Comment, result[0].ReviewText);
            Assert.Equal(reviews[0].Rating, result[0].Rating);
            Assert.Equal(reviews[0].WriteDate, result[0].Written);
        }
    }
}
