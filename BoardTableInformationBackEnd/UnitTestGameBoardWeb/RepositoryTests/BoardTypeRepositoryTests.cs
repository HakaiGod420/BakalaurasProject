using DataLayer.DBContext;
using DataLayer.Models;
using DataLayer.Repositories.BoardType;
using DataLayer.Repositories.Reviews;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace UnitTestGameBoardWeb.RepositoryTests
{
    public class BoardTypeRepositoryTests
    {
        private readonly BoardTypeRepository _repository;
        private readonly DbContextOptionsBuilder<DataBaseContext> _optionsBuilder;
        private readonly DataBaseContext _context;

        public BoardTypeRepositoryTests()
        {
            _optionsBuilder = new DbContextOptionsBuilder<DataBaseContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase2");
            _context = new DataBaseContext(_optionsBuilder.Options);

            _repository = new BoardTypeRepository(_context);
        }

        [Fact]
        public async Task GetType_BoardTypeExists_ReturnsBoardTypeEntity()
        {
            // Arrange
            _context.Database.EnsureDeleted();
            var boardType = new BoardTypeEntity { BoardTypeName = "Test BoardType" };
            _context.BoardTypes.Add(boardType);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetType("Test BoardType");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(boardType.BoardTypeId, result.BoardTypeId);
            Assert.Equal(boardType.BoardTypeName, result.BoardTypeName);
            Assert.Equal(boardType.IsActive, result.IsActive);
        }

        [Fact]
        public async Task GetType_BoardTypeDoesNotExist_ReturnsNull()
        {

            // Act
            var result = await _repository.GetType("Nonexistent BoardType");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetTypes_NoBoardTypes_ReturnsEmptyList()
        {

            // Act
            _context.Database.EnsureDeleted();
            var result = await _repository.GetTypes();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetTypes_ActiveBoardTypesExist_ReturnsListWithTypeDTOs()
        {
            // Arrange
            var boardType1 = new BoardTypeEntity { BoardTypeName = "Test BoardType 1", IsActive = true };
            var boardType2 = new BoardTypeEntity { BoardTypeName = "Test BoardType 2", IsActive = true };
            _context.BoardTypes.AddRange(boardType1, boardType2);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetTypes();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal(boardType1.BoardTypeId.ToString(), result[0].Value);
            Assert.Equal(boardType1.BoardTypeName, result[0].Label);
            Assert.Equal(boardType2.BoardTypeId.ToString(), result[1].Value);
            Assert.Equal(boardType2.BoardTypeName, result[1].Label);
        }

        [Fact]
        public async Task GetTypes_ActiveAndInactiveBoardTypesExist_ReturnsListWithOnlyActiveTypeDTOs()
        {
            // Arrange
            _context.Database.EnsureDeleted();
            var boardType1 = new BoardTypeEntity { BoardTypeName = "Test BoardType 1", IsActive = true };
            var boardType2 = new BoardTypeEntity { BoardTypeName = "Test BoardType 2", IsActive = false };
            _context.BoardTypes.AddRange(boardType1, boardType2);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetTypes();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Count);
            Assert.Equal(boardType1.BoardTypeId.ToString(), result[0].Value);
            Assert.Equal(boardType1.BoardTypeName, result[0].Label);
        }

        [Fact]
        public async Task CreateType_ValidBoardType_ReturnsBoardTypeEntity()
        {
            // Arrange
            var boardType = new BoardTypeEntity { BoardTypeName = "Test BoardType" };

            // Act
            var result = await _repository.CreateType(boardType);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(boardType.BoardTypeId, result.BoardTypeId);
            Assert.Equal(boardType.BoardTypeName, result.BoardTypeName);
            Assert.Equal(boardType.IsActive, result.IsActive);
        }

        [Fact]
        public async Task CreateType_NullBoardType_ThrowsArgumentNullException()
        {
            // Act and Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _repository.CreateType(null));
        }
    }
}
