﻿using DataLayer.DBContext;
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
            .UseInMemoryDatabase(databaseName: "TestDatabase");
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
        }

        [Fact]
        public async Task GetBoardsSimple_ReturnsEmptyListWhenNoMatches()
        {
            // Arrange
            var boardGame = new BoardGameEntity { BoardGameId = 1, Title = "Test Game 1", Thubnail_Location = "monopoly.png", Description = "t" };

            await _context.BoardGames.AddAsync(boardGame);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetBoardsSimple("Another");

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetBoardsSimple_ReturnsEmptyListWhenTitlePartIsNull()
        {
            // Arrange
            var boardGame = new BoardGameEntity { BoardGameId = 1, Title = "Test Game 1", Thubnail_Location = "monopoly.png", Description = "t" };

            await _context.BoardGames.AddAsync(boardGame);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetBoardsSimple(null);

            // Assert
            Assert.Empty(result);
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
            await _context.BoardGames.AddAsync(boardGame);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.ChangeGameBoardState(boardGame.BoardGameId, true);

            // Assert
            Assert.True(result);
            Assert.True(_context.BoardGames.First().IsBlocked);
            Assert.NotNull(_context.BoardGames.First().UpdateTime);
        }

        [Fact]
        public async Task ChangeGameBoardState_ReturnsFalseWhenBoardGameIdIsInvalid()
        {

            // Act
            var result = await _repository.ChangeGameBoardState(1, true);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task GetGameBoardListForAdmin_ReturnsCorrectPageAndPageSize()
        {
            // Arrange
            var user1 = new UserEntity
            {
                UserId = 1,
                UserName = "User1",
                Email = "TestEmail@gmail.com",
                Password = new byte[9],
                PasswordSalt = new byte[50],
                RegistrationTime = DateTime.Now,
                LastTimeConnection = DateTime.Now
            };
            var user2 = new UserEntity
            {
                UserId = 2,
                UserName = "User2",
                Email = "TestEmail@gmail.com",
                Password = new byte[9],
                PasswordSalt = new byte[50],
                RegistrationTime = DateTime.Now,
                LastTimeConnection = DateTime.Now
            };
            var boardGame1 = new BoardGameEntity { BoardGameId = 1, Title = "Test Game 1", Thubnail_Location = "monopoly.png", Description = "t" };
            var boardGame2 = new BoardGameEntity {BoardGameId = 2, Title = "Test Game 2", Thubnail_Location = "monopoly.png", Description = "t" };
            var boardGame3 = new BoardGameEntity {BoardGameId = 3, Title = "Test Game 3", Thubnail_Location = "monopoly.png", Description = "t" };
            await _context.Users.AddRangeAsync(user1, user2);
            await _context.BoardGames.AddRangeAsync(boardGame1, boardGame2, boardGame3);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetGameBoardListForAdmin(2, 1);

            // Assert
            Assert.Equal(3, result.TotalCount);
            Assert.Equal(1, result.Boards.Count);
            Assert.Equal("Test Game 2", result.Boards[0].Title);
            Assert.False(result.Boards[0].IsBlocked);
        }

        [Fact]
        public async Task GetGameBoardListForAdmin_ReturnsEmptyListWhenPageIndexIsOutOfRange()
        {
            // Act
            var result = await _repository.GetGameBoardListForAdmin(10, 1);

            // Assert
            Assert.Empty(result.Boards);
            Assert.Equal(0, result.TotalCount);
        }
    }
}
