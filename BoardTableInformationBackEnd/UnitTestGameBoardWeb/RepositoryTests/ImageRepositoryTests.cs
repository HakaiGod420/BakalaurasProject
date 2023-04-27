using DataLayer.DBContext;
using DataLayer.Models;
using DataLayer.Repositories.AditionalFiles;
using DataLayer.Repositories.Image;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestGameBoardWeb.RepositoryTests
{
    public class ImageRepositoryTests
    {
        private readonly ImageRepository _repository;
        private readonly DbContextOptionsBuilder<DataBaseContext> _optionsBuilder;
        private readonly DataBaseContext _context;

        public ImageRepositoryTests()
        {
            _optionsBuilder = new DbContextOptionsBuilder<DataBaseContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase");
            _context = new DataBaseContext(_optionsBuilder.Options);

            _repository = new ImageRepository(_context);
        }

        [Fact]
        public async Task AddImage_ValidImage_ReturnsImageEntity()
        {
            // Arrange
            var image = new ImageEntity { Location = "/test/image/path", Alias = "Test Image", BoardGameId = 1 };

            // Act
            var result = await _repository.AddImage(image);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(image.ImageId, result.ImageId);
            Assert.Equal(image.Location, result.Location);
            Assert.Equal(image.Alias, result.Alias);
            Assert.Equal(image.BoardGameId, result.BoardGameId);
        }

        [Fact]
        public async Task AddImage_NullImage_ThrowsArgumentNullException()
        {

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _repository.AddImage(null));
        }
    }
}
