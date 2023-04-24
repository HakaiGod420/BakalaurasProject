using DataLayer.DBContext;
using DataLayer.Models;
using DataLayer.Repositories.GameBoard;
using DataLayer.Repositories.Reviews;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
