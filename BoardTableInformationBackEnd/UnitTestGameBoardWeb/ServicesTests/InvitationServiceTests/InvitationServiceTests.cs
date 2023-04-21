﻿using DataLayer.Models;
using DataLayer.Repositories.Address;
using DataLayer.Repositories.Invitation;
using DataLayer.Repositories.User;
using ModelLayer.DTO;
using ModelLayer.Enum;
using Moq;
using ServiceLayer.Interfaces;
using ServiceLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestGameBoardWeb.ServicesTests.InvitationServiceTests
{
    public class InvitationServiceTests
    {
        private readonly Mock<IAddressRepository> _mockAddressRepository;
        private readonly Mock<IInvitationRepository> _mockInvitationRepository;
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly InvitationService _invitationService;

        public InvitationServiceTests()
        {
            _mockAddressRepository = new Mock<IAddressRepository>();
            _mockInvitationRepository = new Mock<IInvitationRepository>();
            _mockUserRepository = new Mock<IUserRepository>();
            _invitationService = new InvitationService(_mockAddressRepository.Object, _mockInvitationRepository.Object, _mockUserRepository.Object);
        }

        [Fact]
        public async Task ChangeInvitationState_ShouldUpdateStateAndPlayerCount_WhenAcceptingInvitation()
        {
            // Arrange
            var data = new InvitationStateChangeDto
            {
                UserId = 1,
                State = "accept",
                InvitationId = 1
            };
            _mockInvitationRepository.Setup(r => r.UpdateStateInvitation(Convert.ToInt32(InvitationState.Accepted), data.InvitationId, data.UserId))
                .Returns(Task.CompletedTask);
            _mockInvitationRepository.Setup(r => r.UpdatePlayerCount(data.InvitationId))
                .ReturnsAsync(true);

            // Act
            await _invitationService.ChangeInvitationState(data);

            // Assert
            _mockInvitationRepository.Verify(r => r.UpdateStateInvitation(Convert.ToInt32(InvitationState.Accepted), data.InvitationId, data.UserId), Times.Once);
            _mockInvitationRepository.Verify(r => r.UpdatePlayerCount(data.InvitationId), Times.Once);
        }

        [Fact]
        public async Task ChangeInvitationState_ShouldUpdateState_WhenDecliningInvitation()
        {
            // Arrange
            var data = new InvitationStateChangeDto
            {
                UserId = 1,
                State = "decline",
                InvitationId = 1
            };
            _mockInvitationRepository.Setup(r => r.UpdateStateInvitation(Convert.ToInt32(InvitationState.Declined), data.InvitationId, data.UserId))
                .Returns(Task.CompletedTask);

            // Act
            await _invitationService.ChangeInvitationState(data);

            // Assert
            _mockInvitationRepository.Verify(r => r.UpdateStateInvitation(Convert.ToInt32(InvitationState.Declined), data.InvitationId, data.UserId), Times.Once);
            _mockInvitationRepository.Verify(r => r.UpdatePlayerCount(data.InvitationId), Times.Never);
        }

        [Fact]
        public async Task GetActiveInvitations_ShouldReturnListOfInvitations_WhenInvitationsExist()
        {
            // Arrange
            var id = 1;
            var invitations = new List<UserInvitationDto>
        {
            new UserInvitationDto { InvitationId = 1, BoardGameTitle = "Game 1" },
            new UserInvitationDto { InvitationId = 2, BoardGameTitle = "Game 2" },
            new UserInvitationDto { InvitationId = 3, BoardGameTitle = "Game 3" },
        };
            _mockInvitationRepository.Setup(r => r.GetAllActiveInvitations(id)).ReturnsAsync(invitations);

            // Act
            var result = await _invitationService.GetActiveInvitations(id);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<UserInvitationDto>>(result);
            Assert.Equal(invitations, result);
        }

        [Fact]
        public async Task GetActiveInvitations_ShouldReturnEmptyList_WhenNoInvitationsExist()
        {
            // Arrange
            var id = 1;
            _mockInvitationRepository.Setup(r => r.GetAllActiveInvitations(id)).ReturnsAsync(new List<UserInvitationDto>());

            // Act
            var result = await _invitationService.GetActiveInvitations(id);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<UserInvitationDto>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetActiveInvitations_ShouldThrowException_WhenRepositoryThrowsException()
        {
            // Arrange
            var id = 1;
            _mockInvitationRepository.Setup(r => r.GetAllActiveInvitations(id)).ThrowsAsync(new Exception());

            // Act and Assert
            await Assert.ThrowsAsync<Exception>(() => _invitationService.GetActiveInvitations(id));
        }

        [Fact]
        public async Task GetInvitations_ShouldReturnInvitations_WhenInvitationsExist()
        {
            // Arrange
            var userId = 1;
            var invitations = new List<UserInvitationDto>
        {
            new UserInvitationDto { InvitationId = 1, ActiveGameId = 1, BoardGameTitle = "Board Game 1", BoardGameId = 1, EventDate = DateTime.Now, EventFullLocation = "Location 1", MaxPlayerCount = 5, AcceptedCount = 3, Map_X_Cords = 1.0, Map_Y_Cords = 2.0 },
            new UserInvitationDto { InvitationId = 2, ActiveGameId = 2, BoardGameTitle = "Board Game 2", BoardGameId = 2, EventDate = DateTime.Now, EventFullLocation = "Location 2", MaxPlayerCount = 10, AcceptedCount = 6, Map_X_Cords = 3.0, Map_Y_Cords = 4.0 }
        };
            _mockInvitationRepository.Setup(r => r.GetAllInvitations(userId)).ReturnsAsync(invitations);

            // Act
            var result = await _invitationService.GetInvitations(userId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<UserInvitationDto>>(result);
            Assert.Equal(invitations.Count, result.Count);
            for (var i = 0; i < invitations.Count; i++)
            {
                Assert.Equal(invitations[i].InvitationId, result[i].InvitationId);
                Assert.Equal(invitations[i].ActiveGameId, result[i].ActiveGameId);
                Assert.Equal(invitations[i].BoardGameTitle, result[i].BoardGameTitle);
                Assert.Equal(invitations[i].BoardGameId, result[i].BoardGameId);
                Assert.Equal(invitations[i].EventDate, result[i].EventDate);
                Assert.Equal(invitations[i].EventFullLocation, result[i].EventFullLocation);
                Assert.Equal(invitations[i].MaxPlayerCount, result[i].MaxPlayerCount);
                Assert.Equal(invitations[i].AcceptedCount, result[i].AcceptedCount);
                Assert.Equal(invitations[i].Map_X_Cords, result[i].Map_X_Cords);
                Assert.Equal(invitations[i].Map_Y_Cords, result[i].Map_Y_Cords);
            }
        }

        [Fact]
        public async Task GetInvitations_ShouldReturnEmptyList_WhenInvitationsDoNotExist()
        {
            // Arrange
            var userId = 1;
            _mockInvitationRepository.Setup(r => r.GetAllInvitations(userId)).ReturnsAsync(new List<UserInvitationDto>());

            // Act
            var result = await _invitationService.GetInvitations(userId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<UserInvitationDto>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetInvitations_ShouldThrowException_WhenRepositoryThrowsException()
        {
            // Arrange
            var userId = 1;
            _mockInvitationRepository.Setup(r => r.GetAllInvitations(userId)).ThrowsAsync(new Exception());

            // Act + Assert
            await Assert.ThrowsAsync<Exception>(() => _invitationService.GetInvitations(userId));
        }

        [Fact]
        public async Task GetCreatedInvitations_ShouldReturnListOfInvitationDtos_WhenIdIsValid()
        {
            // Arrange
            int id = 1;
            List<UserInvitationDto> expectedInvitations = new List<UserInvitationDto>
        {
            new UserInvitationDto
            {
                InvitationId = 1,
                ActiveGameId = 1,
                BoardGameTitle = "Monopoly",
                BoardGameId = 1,
                EventDate = DateTime.Now.AddDays(1),
                EventFullLocation = "New York City",
                MaxPlayerCount = 6,
                AcceptedCount = 2,
                Map_X_Cords = 40.712776,
                Map_Y_Cords = -74.005974
            },
            new UserInvitationDto
            {
                InvitationId = 2,
                ActiveGameId = 2,
                BoardGameTitle = "Settlers of Catan",
                BoardGameId = 2,
                EventDate = DateTime.Now.AddDays(2),
                EventFullLocation = "Los Angeles",
                MaxPlayerCount = 4,
                AcceptedCount = 1,
                Map_X_Cords = 34.052235,
                Map_Y_Cords = -118.243683
            }
        };
            _mockInvitationRepository.Setup(r => r.GetAllCreatedInvitations(id)).ReturnsAsync(expectedInvitations);

            // Act
            List<UserInvitationDto> actualInvitations = await _invitationService.GetCreatedInvitations(id);

            // Assert
            Assert.Equal(expectedInvitations, actualInvitations);
        }

        [Fact]
        public async Task GetCreatedInvitations_ShouldReturnEmptyList_WhenIdHasNoCreatedInvitations()
        {
            // Arrange
            int id = 1;
            List<UserInvitationDto> expectedInvitations = new List<UserInvitationDto>();
            _mockInvitationRepository.Setup(r => r.GetAllCreatedInvitations(id)).ReturnsAsync(expectedInvitations);

            // Act
            List<UserInvitationDto> actualInvitations = await _invitationService.GetCreatedInvitations(id);

            // Assert
            Assert.Equal(expectedInvitations, actualInvitations);
        }

        [Fact]
        public async Task ActiveInvitationCount_ShouldReturnCount_WhenInvitationsExist()
        {
            // Arrange
            int userId = 1;
            int expectedCount = 2;
            _mockInvitationRepository.Setup(r => r.ActiveInvitationCount(userId))
                                     .ReturnsAsync(expectedCount);

            // Act
            int result = await _invitationService.ActiveInvitationCount(userId);

            // Assert
            Assert.Equal(expectedCount, result);
        }

        [Fact]
        public async Task ActiveInvitationCount_ShouldReturnZero_WhenInvitationsDoNotExist()
        {
            // Arrange
            int userId = 1;
            int expectedCount = 0;
            _mockInvitationRepository.Setup(r => r.ActiveInvitationCount(userId))
                                     .ReturnsAsync(expectedCount);

            // Act
            int result = await _invitationService.ActiveInvitationCount(userId);

            // Assert
            Assert.Equal(expectedCount, result);
        }

        [Fact]
        public async Task ActiveInvitationCount_ShouldThrowException_WhenRepositoryThrowsException()
        {
            // Arrange
            int userId = 1;
            _mockInvitationRepository.Setup(r => r.ActiveInvitationCount(userId))
                                     .ThrowsAsync(new Exception("Repository exception"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _invitationService.ActiveInvitationCount(userId));
        }

        [Fact]
        public async Task SentInvitationToUser_ShouldThrowException_WhenUserNotFound()
        {
            // Arrange
            var invitation = new SingeUserSentInvitationDTO { UserName = "nonexistentuser", ActiveInvitationId = 1 };

            _mockUserRepository.Setup(r => r.GetUserIdByUsername(invitation.UserName))
                               .ReturnsAsync((int?)null);

            // Act and Assert
            await Assert.ThrowsAsync<Exception>(() => _invitationService.SentInvitationToUser(invitation));
        }

        [Fact]
        public async Task SentInvitationToUser_ShouldSendInvitation_WhenValidDataIsProvided()
        {
            // Arrange
            var invitation = new SingeUserSentInvitationDTO { UserName = "testuser", ActiveInvitationId = 1 };
            var userId = 1;

            _mockUserRepository.Setup(r => r.GetUserIdByUsername(invitation.UserName))
                               .ReturnsAsync(userId);

            _mockInvitationRepository.Setup(r => r.SentInvitation(It.IsAny<SentInvitationEntity>()))
                                     .ReturnsAsync(true);

            // Act
            await _invitationService.SentInvitationToUser(invitation);

            // Assert
            _mockInvitationRepository.Verify(r => r.SentInvitation(It.Is<SentInvitationEntity>(i =>
                i.UserId == userId && i.SelectedActiveGameId == invitation.ActiveInvitationId && i.InvitationStateId == (int)InvitationState.Onhold)), Times.Once);
        }
    }
}
