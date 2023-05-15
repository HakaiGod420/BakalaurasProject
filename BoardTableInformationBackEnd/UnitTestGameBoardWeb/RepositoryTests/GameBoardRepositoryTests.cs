using DataLayer.DBContext;
using DataLayer.Models;
using DataLayer.Repositories.AditionalFiles;
using DataLayer.Repositories.GameBoard;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestGameBoardWeb.RepositoryTests
{
    public class GameBoardRepositoryTests
    {
        private readonly GameBoardRepository _repository;
        private readonly DbContextOptionsBuilder<DataBaseContext> _optionsBuilder;
        private readonly DataBaseContext _context;

        public GameBoardRepositoryTests()
        {
            _optionsBuilder = new DbContextOptionsBuilder<DataBaseContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase4");
            _context = new DataBaseContext(_optionsBuilder.Options);

            _repository = new GameBoardRepository(_context);
        }

        [Fact]
        public async Task GetBoardsSimple_ReturnsMatchingBoardGames()
        {
            // Arrange
            
            var boardGame1 = new BoardGameEntity { BoardGameId = 1, Title = "Test Game 1", Thubnail_Location = "monopoly.png", Description = "t" };
            var boardGame2 = new BoardGameEntity { BoardGameId = 2, Title = "Test Game 2", Thubnail_Location = "monopoly.png", Description = "t" };
            var boardGame3 = new BoardGameEntity { BoardGameId = 3, Title = "Game 3", Thubnail_Location = "monopoly.png", Description = "t" };

            await _context.BoardGames.AddRangeAsync(boardGame1, boardGame2, boardGame3);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetBoardsSimple("Test");

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("Test Game 1", result[0].Title);
            Assert.Equal("Test Game 2", result[1].Title);
            _context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task GetBoardsSimple_ReturnsEmptyListWhenNoMatches()
        {
            // Arrange
            var boardGame = new BoardGameEntity { BoardGameId = 1, Title = "Test Game 1", Thubnail_Location = "monopoly.png", Description = "t" };

            _context.BoardGames.Add(boardGame);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetBoardsSimple("Another");

            // Assert
            Assert.Empty(result);
            _context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task GetBoardsSimple_ReturnsEmptyListWhenTitlePartIsNull()
        {
            // Arrange

            var boardGame = new BoardGameEntity { BoardGameId = 1, Title = "Test Game 1", Thubnail_Location = "monopoly.png", Description = "t" };

            _context.BoardGames.Add(boardGame);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetBoardsSimple(null);

            // Assert
            Assert.Empty(result);
            _context.Database.EnsureDeleted();
        }


        [Fact]
        public async Task AddGameBoard_SavesNewBoardGame()
        {
            // Arrange

            var boardGame = new BoardGameEntity { BoardGameId = 1, Title = "Test Game", Thubnail_Location = "monopoly.png", Description = "t" };

            // Act
            var result = await _repository.AddGameBoard(boardGame);

            // Assert
            Assert.Equal(boardGame, result);
            Assert.Equal(1, _context.BoardGames.Count());
            Assert.Equal("Test Game", _context.BoardGames.First().Title);
            _context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task AddGameBoard_ThrowsExceptionWhenBoardGameIsNull()
        {
            // Arrange

            // Act + Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _repository.AddGameBoard(null));
        }

        [Fact]
        public async Task ChangeGameBoardState_ChangesIsBlockedFieldAndReturnsTrue()
        {
            // Arrange

            var boardGame = new BoardGameEntity { BoardGameId = 1, Title = "Test Game", Thubnail_Location = "monopoly.png", Description = "t" };
            _context.BoardGames.Add(boardGame);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.ChangeGameBoardState(boardGame.BoardGameId, true);

            // Assert
            Assert.True(result);
            Assert.True(_context.BoardGames.First().IsBlocked);
            Assert.NotNull(_context.BoardGames.First().UpdateTime);
            _context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task ChangeGameBoardState_ReturnsFalseWhenBoardGameIdIsInvalid()
        {

            // Act
            var result = await _repository.ChangeGameBoardState(1, true);

            // Assert
            Assert.False(result);
            _context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task GetGameBoardListForAdmin_ReturnsEmptyListWhenPageIndexIsOutOfRange()
        {


            // Act
            var result = await _repository.GetGameBoardListForAdmin(10, 1);

            // Assert
            Assert.Empty(result.Boards);
            Assert.Equal(0, result.TotalCount);
            _context.Database.EnsureDeleted();
        }
    }
}
