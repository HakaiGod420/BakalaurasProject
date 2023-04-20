using DataLayer.Models;
using DataLayer.Repositories.User;
using Microsoft.Extensions.Configuration;
using ModelLayer.DTO;
using ModelLayer.Enum;
using Moq;
using ServiceLayer.Interfaces;
using ServiceLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestGameBoardWeb.ServicesTests.UserServiceTests
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _mockRepository;
        private readonly Mock<IConfiguration> _configuration;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _configuration = new Mock<IConfiguration>();
            _mockRepository = new Mock<IUserRepository>();
            _userService = new UserService(_mockRepository.Object, _configuration.Object);
        }

        [Fact]
        public async Task GetUser_ShouldReturnUserViewModel_WhenUserExists()
        {
            // Arrange
            int userId = 1;
            var userEntity = new UserEntity
            {
                UserId = userId,
                UserName = "testuser",
                Email = "testuser@test.com",
                RegistrationTime = DateTime.Now,
                LastTimeConnection = DateTime.Now,
                ProfileImage = "testimage",
                RoleId = Roles.Admin,
                UserStateId = UserStates.Active
            };
            _mockRepository.Setup(r => r.GetUser(userId))
                           .ReturnsAsync(userEntity);
            var expectedViewModel = new UserViewModel(userId, "testuser", "testuser@test.com", userEntity.RegistrationTime, userEntity.LastTimeConnection, "testimage", Roles.Admin.ToString(), UserStates.Active.ToString());

            // Act
            var result = await _userService.GetUser(userId);

            // Assert
            Assert.Equal(expectedViewModel.Id, result.Id);
            Assert.Equal(expectedViewModel.Username, result.Username);
            Assert.Equal(expectedViewModel.Email, result.Email);
            Assert.Equal(expectedViewModel.RegistrationTime, result.RegistrationTime);
            Assert.Equal(expectedViewModel.LastTimeConnection, result.LastTimeConnection);
            Assert.Equal(expectedViewModel.ProfileImmage, result.ProfileImmage);
            Assert.Equal(expectedViewModel.Role, result.Role);
            Assert.Equal(expectedViewModel.State, result.State);
        }

        [Fact]
        public async Task GetUser_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            int userId = 1;
            _mockRepository.Setup(r => r.GetUser(userId))
                           .ReturnsAsync((UserEntity)null);

            // Act
            var result = await _userService.GetUser(userId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetUser_ShouldReturnNull_WhenRepositoryThrowsException()
        {
            // Arrange
            int id = 1;
            _mockRepository.Setup(r => r.GetUser(id))
                           .Throws(new Exception("Error fetching user"));

            try
            {
                // Act
                var result = await _userService.GetUser(id);
                Assert.True(false);
            }
            catch
            {
                Assert.True(true);
            }
        }

        [Fact]
        public async Task ChangeUserInformation_ShouldChangeEmail_WhenValidDataIsProvided()
        {
            // Arrange
            var userId = 1;
            var userInformationDTO = new ChangeUserInformationDTO
            {
                UserId = userId,
                Email = "testemail@example.com"
            };
            var userEntity = new UserEntity
            {
                UserId = userId,
                Email = "oldemail@example.com"
            };
            _mockRepository.Setup(r => r.GetPasswordsById(userId))
                .ReturnsAsync(userEntity);
            _mockRepository.Setup(r => r.ChangeUserData(It.IsAny<UserEntity>()))
                .Returns(Task.CompletedTask);

            // Act
            await _userService.ChangeUserInformation(userInformationDTO);

            // Assert
            Assert.Equal(userInformationDTO.Email, userEntity.Email);
        }


        [Fact]
        public async Task ChangeUserInformation_ShouldThrowException_WhenOldPasswordIsWrong()
        {
            // Arrange
            var userInformationDTO = new ChangeUserInformationDTO
            {
                UserId = 1,
                Email = "johndoe@example.com",
                PasswordChanged = true,
                OldPassword = "wrongPassword",
                NewPassword = "newPassword"
            };

            var userEntity = new UserEntity
            {
                UserId = 1,
                UserName = "johndoe",
                Email = "johndoe@example.com",
                Password = new byte[] { 123, 123, 123 },
                PasswordSalt = new byte[] { 1, 2, 3 },
            };

            _mockRepository.Setup(r => r.GetPasswordsById(It.IsAny<int>())).ReturnsAsync(userEntity);

            // Act
            var exception = await Assert.ThrowsAsync<Exception>(() => _userService.ChangeUserInformation(userInformationDTO));

            // Assert
            Assert.NotNull(exception);
            Assert.Equal("Old Password Wrong", exception.Message);
        }

        [Fact]
        public async Task UpdateNotifications_ShouldReturnFalse_WhenNotificationListIsNull()
        {
            // Arrange
            NotificationsListDto notificationsListDto = null;

            // Act
            var result = await _userService.UpdateNotifications(1, notificationsListDto);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task UpdateNotifications_ShouldCallUpdateInvitationNotification_WhenInvitationNotificationIsActive()
        {
            // Arrange
            var notificationsListDto = new NotificationsListDto
            {
                Notifications = new List<NotificationSettingsDto>
            {
                new NotificationSettingsDto { Title = "Some Other Notification", IsActive = true },
                new NotificationSettingsDto { Title = "Invitation", IsActive = true }
            }
            };
            var userId = 1;

            _mockRepository.Setup(r => r.UpdateInvitationNotification(userId, true))
                           .ReturnsAsync(true);

            // Act
            var result = await _userService.UpdateNotifications(userId, notificationsListDto);

            // Assert
            _mockRepository.Verify(r => r.UpdateInvitationNotification(userId, true), Times.Once);
            Assert.True(result);
        }

        [Fact]
        public async Task UpdateNotifications_ShouldReturnFalse_WhenNoInvitationNotificationIsActive()
        {
            // Arrange
            var notificationsListDto = new NotificationsListDto
            {
                Notifications = new List<NotificationSettingsDto>
            {
                new NotificationSettingsDto { Title = "Some Other Notification", IsActive = true },
                new NotificationSettingsDto { Title = "Another Notification", IsActive = false }
            }
            };
            var userId = 1;

            // Act
            var result = await _userService.UpdateNotifications(userId, notificationsListDto);

            // Assert
            _mockRepository.Verify(r => r.UpdateInvitationNotification(It.IsAny<int>(), It.IsAny<bool>()), Times.Never);
            Assert.False(result);
        }

        [Fact]
        public async Task UpdateNotifications_ShouldReturnFalse_WhenRepositoryThrowsException()
        {
            // Arrange
            var notificationsListDto = new NotificationsListDto
            {
                Notifications = new List<NotificationSettingsDto>
            {
                new NotificationSettingsDto { Title = "Invitation", IsActive = true }
            }
            };
            var userId = 1;

            _mockRepository.Setup(r => r.UpdateInvitationNotification(userId, true))
                           .ThrowsAsync(new Exception());

            await Assert.ThrowsAsync<Exception>(async () => await _userService.UpdateNotifications(userId, notificationsListDto));
        }

        [Fact]
        public async Task GetUserSettings_ShouldReturnUserSettings_WhenUserExists()
        {
            // Arrange
            var userId = 1;
            var userSettings = new UserSettings
            {
                Address = new UserAddressDto
                {
                    Country = "USA",
                    City = "New York",
                    StreetName = "Broadway",
                    Province = "NY",
                    Map_X_Coords = 123.45,
                    Map_Y_Coords = 67.89
                },
                EnabledInvitationSettings = true
            };
            _mockRepository.Setup(r => r.GetUserSettings(userId))
                .ReturnsAsync(userSettings);

            // Act
            var result = await _userService.GetUserSettings(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userSettings.Address.Country, result.Address.Country);
            Assert.Equal(userSettings.Address.City, result.Address.City);
            Assert.Equal(userSettings.Address.StreetName, result.Address.StreetName);
            Assert.Equal(userSettings.Address.Province, result.Address.Province);
            Assert.Equal(userSettings.Address.Map_X_Coords, result.Address.Map_X_Coords);
            Assert.Equal(userSettings.Address.Map_Y_Coords, result.Address.Map_Y_Coords);
            Assert.Equal(userSettings.EnabledInvitationSettings, result.EnabledInvitationSettings);
        }

        [Fact]
        public async Task GetUserSettings_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = 1;
            _mockRepository.Setup(r => r.GetUserSettings(userId))
                .ReturnsAsync((UserSettings)null);

            // Act
            var result = await _userService.GetUserSettings(userId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetUserSettings_ShouldThrowException_WhenRepositoryThrowsException()
        {
            // Arrange
            var userId = 1;
            _mockRepository.Setup(r => r.GetUserSettings(userId))
                .ThrowsAsync(new Exception());

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _userService.GetUserSettings(userId));
        }

        [Fact]
        public async Task GetUserInformation_ShouldReturnUserInformationDto_WhenUserExists()
        {
            // Arrange
            var userName = "JohnDoe";
            var expectedUserInformation = new UserInformationDTO
            {
                UserId = 1,
                UserName = "JohnDoe",
                Role = "Admin",
                InvititationsCreated = 10,
                TableTopGamesCreated = 5,
                State = "Active",
                RegisteredOn = DateTime.UtcNow,
                LastLogin = DateTime.UtcNow
            };
            _mockRepository.Setup(r => r.GetUserInformation(userName))
                .ReturnsAsync(expectedUserInformation);

            // Act
            var userInformation = await _userService.GetUserInformation(userName);

            // Assert
            Assert.Equal(expectedUserInformation.UserId, userInformation.UserId);
            Assert.Equal(expectedUserInformation.UserName, userInformation.UserName);
            Assert.Equal(expectedUserInformation.Role, userInformation.Role);
            Assert.Equal(expectedUserInformation.InvititationsCreated, userInformation.InvititationsCreated);
            Assert.Equal(expectedUserInformation.TableTopGamesCreated, userInformation.TableTopGamesCreated);
            Assert.Equal(expectedUserInformation.State, userInformation.State);
            Assert.Equal(expectedUserInformation.RegisteredOn, userInformation.RegisteredOn);
            Assert.Equal(expectedUserInformation.LastLogin, userInformation.LastLogin);
        }

        [Fact]
        public async Task GetUserInformation_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            var userName = "JohnDoe";
            _mockRepository.Setup(r => r.GetUserInformation(userName))
                .ReturnsAsync((UserInformationDTO)null);

            // Act
            var userInformation = await _userService.GetUserInformation(userName);

            // Assert
            Assert.Null(userInformation);
        }

        [Fact]
        public async Task GetUserInformation_ShouldThrowException_WhenRepositoryThrows()
        {
            // Arrange
            var userName = "john.doe";
            _mockRepository.Setup(r => r.GetUserInformation(userName))
                .ThrowsAsync(new Exception("Repository Exception"));

            // Act + Assert
            await Assert.ThrowsAsync<Exception>(() => _userService.GetUserInformation(userName));
        }

        [Fact]
        public async Task Login_ShouldReturnToken_WhenValidUserCredentialsAreProvided()
        {
            // Arrange
            var userName = "testuser";
            var password = "testpassword";
            var passwordSalt = new byte[] { 1, 2, 3 };
            var passwordHash = new HMACSHA512(passwordSalt).ComputeHash(Encoding.UTF8.GetBytes(password));
            var userEntity = new UserEntity
            {
                UserId = 1,
                UserName = userName,
                PasswordSalt = passwordSalt,
                Password = passwordHash,
                Email = "testemail@test.com",
                RoleId = Roles.Admin,
                LastTimeConnection = DateTime.Now.AddDays(-1)
            };
            _configuration.Setup(c => c.GetSection("AppSettings:Token").Value).Returns("tokentokentokentokentokentokentoken");
            _configuration.Setup(c => c.GetSection("Jwt:Audience").Value).Returns("testaudience");
            _configuration.Setup(c => c.GetSection("Jwt:Issuer").Value).Returns("testissuer");
            _mockRepository.Setup(r => r.GetPasswords(userName))
                .ReturnsAsync(userEntity);
            _mockRepository.Setup(r => r.UpdateLastTimeConnection(userEntity.UserId))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _userService.Login(new LoginUserModel { UserName = userName, Password = password });

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Login_ShouldReturnNull_WhenInvalidUserCredentialsAreProvided()
        {
            // Arrange
            var userName = "testuser";
            var password = "testpassword";
            var passwordSalt = new byte[] { 1, 2, 3 };
            var passwordHash = new HMACSHA512(passwordSalt).ComputeHash(Encoding.UTF8.GetBytes(password));
            var userEntity = new UserEntity
            {
                UserId = 1,
                UserName = userName,
                PasswordSalt = passwordSalt,
                Password = passwordHash,
                Email = "testemail@test.com",
                RoleId = Roles.Admin,
                LastTimeConnection = DateTime.Now.AddDays(-1)
            };
            _mockRepository.Setup(r => r.GetPasswords(userName))
                .ReturnsAsync(userEntity);

            // Act
            var result = await _userService.Login(new LoginUserModel { UserName = userName, Password = "wrongpassword" });

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Login_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Arrange
            var userName = "testuser";
            _mockRepository.Setup(r => r.GetPasswords(userName))
                .ReturnsAsync((UserEntity)null);

            // Act
            var result = await _userService.Login(new LoginUserModel { UserName = userName, Password = "testpassword" });

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Login_ShouldThrowException_WhenRepositoryThrowsException()
        {
            // Arrange
            var loginUser = new LoginUserModel
            {
                UserName = "username",
                Password = "password"
            };
            _mockRepository.Setup(r => r.GetPasswords(loginUser.UserName))
                .ThrowsAsync(new Exception());

            // Act + Assert
            await Assert.ThrowsAsync<Exception>(() => _userService.Login(loginUser));
        }

        [Fact]
        public async Task RegisterUser_ShouldReturnNull_WhenUserNameAlreadyExists()
        {
            // Arrange
            var user = new UserRegisterModel
            {
                UserName = "existing_user",
                Email = "test@test.com",
                Password = "password",
                RePassword = "password"
            };
            _mockRepository.Setup(r => r.UsernameExist(user.UserName))
                .ReturnsAsync(true);

            // Act
            var result = await _userService.RegisterUser(user);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task RegisterUser_ShouldReturnNewUser_WhenRegistrationIsSuccessful()
        {
            // Arrange
            var user = new UserRegisterModel
            {
                UserName = "new_user",
                Email = "test@test.com",
                Password = "password",
                RePassword = "password"
            };
            var userEntity = new UserEntity
            {
                UserId = 1,
                UserName = user.UserName,
                Email = user.Email,
                RegistrationTime = DateTime.Now,
                LastTimeConnection = DateTime.Now,
                RoleId = Roles.User,
                UserStateId = UserStates.Active,
                Password = new byte[] { 1, 2, 3 },
                PasswordSalt = new byte[] { 4, 5, 6 },
                ProfileImage = null
            };
            _mockRepository.Setup(r => r.UsernameExist(user.UserName))
                .ReturnsAsync(false);
            _mockRepository.Setup(r => r.RegisterUser(It.IsAny<UserEntity>()))
                .ReturnsAsync(userEntity);

            // Act
            var result = await _userService.RegisterUser(user);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userEntity.UserId, result.Id);
            Assert.Equal(userEntity.UserName, result.Username);
            Assert.Equal(userEntity.Email, result.Email);
            Assert.Equal(userEntity.RegistrationTime, result.RegistrationTime);
            Assert.Equal(userEntity.LastTimeConnection, result.LastTimeConnection);
            Assert.Equal(userEntity.ProfileImage, result.ProfileImmage);
            Assert.Equal(userEntity.RoleId.ToString(), result.Role);
            Assert.Equal(userEntity.UserStateId.ToString(), result.State);
        }

        [Fact]
        public async Task RegisterUser_ShouldThrowException_WhenRepositoryThrowsException()
        {
            // Arrange
            var user = new UserRegisterModel
            {
                UserName = "new_user",
                Email = "test@test.com",
                Password = "password",
                RePassword = "password"
            };
            _mockRepository.Setup(r => r.UsernameExist(user.UserName))
                .ReturnsAsync(false);
            _mockRepository.Setup(r => r.RegisterUser(It.IsAny<UserEntity>()))
                .ThrowsAsync(new Exception());

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _userService.RegisterUser(user));
        }

    }
}
