using DataLayer.DBContext;
using DataLayer.Models;
using DataLayer.Repositories.BoardType;
using DataLayer.Repositories.Image;
using DataLayer.Repositories.Invitation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ModelLayer.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestGameBoardWeb.RepositoryTests
{
    public class InvitationRepositoryTests
    {
        private InvitationRepository _repository;
        private DbContextOptionsBuilder<DataBaseContext> _optionsBuilder;
        private DataBaseContext _context;

        public InvitationRepositoryTests()
        {
            _optionsBuilder = new DbContextOptionsBuilder<DataBaseContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase");
            _context = new DataBaseContext(_optionsBuilder.Options);

            _repository = new InvitationRepository(_context);
        }

        [Fact]
        public async Task AddInvitation_ValidInvitation_IncreasesInvitationsCount()
        {
            // Arrange
            var initialCount = await _context.ActiveGames.CountAsync();
            var invitation = new ActiveGameEntity
            {
                PlayersNeed = 4,
                RegistredPlayerCount = 0,
                InvitationStateId = ActiveGameState.Open,
                Map_X_Cords = 1.0f,
                Map_Y_Cords = 2.0f,
                CreatorId = 1,
                AddressId = 1,
                MeetDate = DateTime.Now,
                BoardGameId = 1,
            };

            // Act
            await _repository.AddInvitation(invitation);
            var finalCount = await _context.ActiveGames.CountAsync();

            // Assert
            Assert.Equal(initialCount + 1, finalCount);
            _context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task AddInvitation_ValidInvitation_ReturnsSameInvitation()
        {
            // Arrange
            var invitation = new ActiveGameEntity
            {
                PlayersNeed = 4,
                RegistredPlayerCount = 0,
                InvitationStateId = ActiveGameState.Open,
                Map_X_Cords = 1.0f,
                Map_Y_Cords = 2.0f,
                CreatorId = 1,
                AddressId = 1,
                MeetDate = DateTime.Now,
                BoardGameId = 1,
            };

            // Act
            var result = await _repository.AddInvitation(invitation);

            // Assert
            Assert.Equal(invitation, result);
            _context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task AddInvitation_NullInvitation_ThrowsArgumentNullException()
        {
            // Arrange
            ActiveGameEntity invitation = null;

            // Act + Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _repository.AddInvitation(invitation));
            _context.Database.EnsureDeleted();
        }


        
        [Fact]
        public async Task SentInvitation_ValidInvitation_ReturnsFalse()
        {
            // Arrange
            var activeGame = new ActiveGameEntity
            {
                PlayersNeed = 5,
                RegistredPlayerCount = 3,
                InvitationStateId = ActiveGameState.Open,
                Map_X_Cords = 10.0f,
                Map_Y_Cords = 20.0f,
                MeetDate = DateTime.Now.AddDays(1),
                CreatorId = 1,
                BoardGameId = 1,
                AddressId = 1,
            };

            var user = new UserEntity
            {
                UserId = 1,
                UserName = "testuser",
                Email = "TestEmail@gmail.com",
                Password = new byte[9],
                PasswordSalt = new byte[50],
                RegistrationTime = DateTime.Now,
                LastTimeConnection = DateTime.Now
            };
            var invitation = new SentInvitationEntity { SelectedActiveGame = activeGame, User = user, InvitationStateId = 1 };

            // Act
            var result = await _repository.SentInvitation(invitation);

            // Assert
            Assert.False(result);
            _context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task SentInvitation_InvitationIsNull_ReturnsFalse()
        {

            // Act
            var result = await _repository.SentInvitation(null);

            // Assert
            Assert.False(result);
            _context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task SentInvitation_ActiveGameIsZero_ReturnsFalse()
        {
            // Arrange

            var user = new UserEntity
            {
                UserId = 1,
                UserName = "testuser",
                Email = "TestEmail@gmail.com",
                Password = new byte[9],
                PasswordSalt = new byte[50],
                RegistrationTime = DateTime.Now,
                LastTimeConnection = DateTime.Now
            };

            var invitation = new SentInvitationEntity { SelectedActiveGameId = 0, User = user, InvitationStateId = 1 };

            // Act
            var result = await _repository.SentInvitation(invitation);

            // Assert
            Assert.False(result);
            _context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task UpdateStateInvitation_InvitationExistsAndUserMatches_SuccessfullyUpdatesState()
        {
            // Arrange
            var invitation = new SentInvitationEntity
            {
                SelectedActiveGameId = 1,
                UserId = 1,
                InvitationStateId = 1
            };
            _context.SentInvitations.Add(invitation);
            await _context.SaveChangesAsync();

            // Act
            await _repository.UpdateStateInvitation(2, invitation.SentInvitationId, invitation.UserId);
            var updatedInvitation = await _context.SentInvitations.FindAsync(invitation.SentInvitationId);

            // Assert
            Assert.NotNull(updatedInvitation);
            Assert.Equal(2, updatedInvitation.InvitationStateId);
            _context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task UpdatePlayerCount_InvitationNotFound_ReturnsFalse()
        {

            // Act
            var result = await _repository.UpdatePlayerCount(1);

            // Assert
            Assert.False(result);
            _context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task UpdatePlayerCount_GameNotFound_ReturnsFalse()
        {
            // Arrange
            var invitation = new SentInvitationEntity { SentInvitationId = 1, SelectedActiveGameId = 1 };
            _context.SentInvitations.Add(invitation);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.UpdatePlayerCount(1);

            // Assert
            Assert.False(result);
            _context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task UpdatePlayerCount_GameAlreadyFull_ReturnsFalse()
        {
            // Arrange
            var invitation = new SentInvitationEntity { SentInvitationId = 1, SelectedActiveGameId = 1 };
            var game = new ActiveGameEntity { ActiveGameId = 1, PlayersNeed = 1, RegistredPlayerCount = 1, InvitationStateId = ActiveGameState.Open };
            _context.SentInvitations.Add(invitation);
            _context.ActiveGames.Add(game);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.UpdatePlayerCount(1);

            // Assert
            Assert.False(result);
            _context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task UpdatePlayerCount_GameNotFull_ReturnsTrue()
        {
            // Arrange
            var invitation = new SentInvitationEntity { SentInvitationId = 1, SelectedActiveGameId = 1 };
            var game = new ActiveGameEntity { ActiveGameId = 1, PlayersNeed = 2, RegistredPlayerCount = 1, InvitationStateId = ActiveGameState.Open };
            _context.SentInvitations.Add(invitation);
            _context.ActiveGames.Add(game);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.UpdatePlayerCount(1);

            // Assert
            Assert.True(result);
            _context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task UpdatePlayerCount_GameFull_ClosesGame()
        {
            // Arrange
            _context.Database.EnsureDeleted();

            var invitation = new SentInvitationEntity { SentInvitationId = 1, SelectedActiveGameId = 1 };
            var game = new ActiveGameEntity { ActiveGameId = 1, PlayersNeed = 1, RegistredPlayerCount = 0, InvitationStateId = ActiveGameState.Open };
            _context.SentInvitations.Add(invitation);
            _context.ActiveGames.Add(game);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.UpdatePlayerCount(1);

            // Assert
            Assert.True(result);
            Assert.Equal(ActiveGameState.Closed, game.InvitationStateId);
            _context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task ActiveInvitationCount_NoActiveInvitations_ReturnsZero()
        {
            // Arrange
            int userId = 1;
            var invitations = new List<SentInvitationEntity>();
            _context.SentInvitations.AddRange(invitations);
            await _context.SaveChangesAsync();

            // Act
            int result = await _repository.ActiveInvitationCount(userId);

            // Assert
            Assert.Equal(0, result);
            _context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task ActiveInvitationCount_OneActiveInvitation_ReturnsOne()
        {
            // Arrange
            int userId = 1;
            var activeGame = new ActiveGameEntity { ActiveGameId = 1, MeetDate = DateTime.Now.AddDays(1), InvitationStateId = ActiveGameState.Open };
            var invitation = new SentInvitationEntity { SentInvitationId = 1, UserId = userId, SelectedActiveGameId = activeGame.ActiveGameId, SelectedActiveGame = activeGame, InvitationStateId = 1 };
            activeGame.SentInvitations.Add(invitation);
            var invitations = new List<SentInvitationEntity> { invitation };
            _context.SentInvitations.AddRange(invitations);
            _context.ActiveGames.Add(activeGame);
            await _context.SaveChangesAsync();

            // Act
            int result = await _repository.ActiveInvitationCount(userId);

            // Assert
            Assert.Equal(1, result);
            _context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task ActiveInvitationCount_MultipleActiveInvitations_ReturnsCount()
        {
            // Arrange
            int userId = 1;
            var activeGame = new ActiveGameEntity { ActiveGameId = 1, MeetDate = DateTime.Now.AddDays(1), InvitationStateId = ActiveGameState.Open };
            var invitation1 = new SentInvitationEntity { SentInvitationId = 1, UserId = userId, SelectedActiveGameId = activeGame.ActiveGameId, SelectedActiveGame = activeGame, InvitationStateId = 1 };
            var invitation2 = new SentInvitationEntity { SentInvitationId = 2, UserId = userId, SelectedActiveGameId = activeGame.ActiveGameId, SelectedActiveGame = activeGame, InvitationStateId = 1 };
            activeGame.SentInvitations.Add(invitation1);
            activeGame.SentInvitations.Add(invitation2);
            var invitations = new List<SentInvitationEntity> { invitation1, invitation2 }; ;
            _context.SentInvitations.AddRange(invitations);
            _context.ActiveGames.Add(activeGame);
            await _context.SaveChangesAsync();


            // Act
            int result = await _repository.ActiveInvitationCount(userId);
            // Assert
            Assert.Equal(2, result);
            _context.Database.EnsureDeleted();
        }

        [Fact]
        public async Task GetInvitationsByCountry_ReturnsInvitationsByCountry()
        {
            // Arrange
            _context.Database.EnsureDeleted();
            string country = "Canada";
            int pageIndex = 0;
            int pageSize = 10;
            var activeGame1 = new ActiveGameEntity { ActiveGameId = 1, MeetDate = DateTime.Now.AddDays(1), InvitationStateId = ActiveGameState.Open, BoardGameId = 1 };
            var activeGame2 = new ActiveGameEntity { ActiveGameId = 2, MeetDate = DateTime.Now.AddDays(2), InvitationStateId = ActiveGameState.Open, BoardGameId = 2 };
            var address1 = new AddressEntity { AddressId = 1, Country = "Canada", FullAddress = "123 Main St", City = "city1", Province = "t", StreetName = "t", PostalCode = "LT" };
            var address2 = new AddressEntity { AddressId = 2, Country = "USA", FullAddress = "456 Main St", City = "city1", Province = "t", StreetName = "t", PostalCode = "LT" };
            activeGame1.Address = address1;
            activeGame2.Address = address2;
            activeGame1.BoardGame = new BoardGameEntity { BoardGameId = 1, Title = "Monopoly", Thubnail_Location = "monopoly.png", Description = "t" };
            activeGame2.BoardGame = new BoardGameEntity { BoardGameId = 2, Title = "Catan", Thubnail_Location = "catan.png", Description = "t" };
            var activeGames = new List<ActiveGameEntity> { activeGame1, activeGame2 };
            _context.Addresses.AddRange(new List<AddressEntity> { address1, address2 });
            _context.BoardGames.AddRange(new List<BoardGameEntity> { activeGame1.BoardGame, activeGame2.BoardGame });
            _context.ActiveGames.AddRange(activeGames);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetInvitationsByCountry(country, pageIndex, pageSize, 1);

            // Assert
            Assert.Equal(1, result.Invitations.Count);
            Assert.Equal(1, result.TotalCount);
            Assert.Contains(result.Invitations, x => x.BoardGameTitle == activeGame1.BoardGame.Title);
            _context.Database.EnsureDeleted();
        }

    }
}
