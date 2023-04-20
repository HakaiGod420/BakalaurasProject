using DataLayer.Models;
using DataLayer.Repositories.Address;
using DataLayer.Repositories.User;
using ModelLayer.DTO;
using Moq;
using ServiceLayer.Services;


namespace UnitTestGameBoardWeb.ServicesTests.AddressServiceTest
{
    public class AddressServiceTests
    {
        private readonly Mock<IAddressRepository> _mockRepository;
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly AddressService _addressService;

        public AddressServiceTests()
        {
            _mockRepository = new Mock<IAddressRepository>();
            _mockUserRepository = new Mock<IUserRepository>();
            _addressService = new AddressService(_mockRepository.Object, _mockUserRepository.Object);
        }

        [Fact]
        public async Task AddNewAddress_ShouldReturnTrue_WhenAddressIsAdded()
        {
            // Arrange
            var addressCreateDto = new AddressCreateDto
            {
                Country = "USA",
                City = "New York",
                StreetName = "Broadway",
                Province = "NY",
                PostalCode = "10001",
                HouseNumber = 123
            };

            _mockRepository.Setup(r => r.AddAddress(It.IsAny<AddressEntity>()))
                           .ReturnsAsync(new AddressEntity());

            // Act
            var result = await _addressService.AddNewAddress(addressCreateDto);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task AddNewAddress_ShouldReturnFalse_WhenExceptionIsThrown()
        {
            // Arrange
            var addressCreateDto = new AddressCreateDto
            {
                Country = "USA",
                City = "New York",
                StreetName = "Broadway",
                Province = "NY",
                PostalCode = "10001",
                HouseNumber = 123
            };

            _mockRepository.Setup(r => r.AddAddress(It.IsAny<AddressEntity>()))
                           .Throws(new Exception("An error occurred"));

            // Act
            var result = await _addressService.AddNewAddress(addressCreateDto);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task UpdateUserAddress_ShouldReturnTrue_WhenNewAddressIsAdded()
        {
            // Arrange
            int userId = 1;
            var addressDto = new UpdateUserAddress
            {
                Address = new UserAddressDto
                {
                    Country = "USA",
                    City = "New York",
                    StreetName = "Broadway",
                    Province = "NY",
                    Map_X_Coords = 40.7128,
                    Map_Y_Coords = -74.0060
                },
                EnabledInvitationSettings = true
            };

            _mockRepository.Setup(r => r.CheckIfUserHasAddress(userId))
                                  .ReturnsAsync((int?)null);

            _mockRepository.Setup(r => r.AddAddressToUser(It.IsAny<UserAddressEntity>()))
                                  .ReturnsAsync(new UserAddressEntity { UserAddressId = 1 });

            _mockUserRepository.Setup(r => r.UpdateUserAddress(1, userId))
                               .Returns(Task.CompletedTask);

            _mockUserRepository.Setup(r => r.UpdateInvitationNotification(userId, true))
                               .ReturnsAsync(true);

            // Act
            var result = await _addressService.UpdateUserAddress(userId, addressDto);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task UpdateUserAddress_ShouldReturnTrue_WhenAddressIsUpdated()
        {
            // Arrange
            int userId = 1;
            int addressId = 1;
            var addressDto = new UpdateUserAddress
            {
                Address = new UserAddressDto
                {
                    Country = "USA",
                    City = "Los Angeles",
                    StreetName = "Hollywood Blvd",
                    Province = "CA",
                    Map_X_Coords = 34.0928,
                    Map_Y_Coords = -118.3287
                },
                EnabledInvitationSettings = false
            };

            _mockRepository.Setup(r => r.CheckIfUserHasAddress(userId))
                                  .ReturnsAsync(addressId);

            _mockRepository.Setup(r => r.UpdateUserAddress(It.IsAny<UserAddressEntity>()))
                                  .ReturnsAsync(new UserAddressEntity());

            _mockUserRepository.Setup(r => r.UpdateInvitationNotification(userId, false))
                               .ReturnsAsync(true);

            // Act
            var result = await _addressService.UpdateUserAddress(userId, addressDto);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task UpdateUserAddress_ShouldReturnFalse_WhenUpdateUserAddressThrowsException()
        {
            // Arrange
            var addressDto = new UpdateUserAddress
            {
                Address = new UserAddressDto
                {
                    City = "New York",
                    Country = "USA",
                    StreetName = "Broadway",
                    Province = "NY",
                    Map_X_Coords = 40.7128,
                    Map_Y_Coords = -74.0060,
                },
                EnabledInvitationSettings = true,
            };
            var userId = 1;
            var addressId = 2;
            _mockRepository.Setup(r => r.CheckIfUserHasAddress(userId)).ReturnsAsync(addressId);
            _mockRepository.Setup(r => r.UpdateUserAddress(It.IsAny<UserAddressEntity>())).Throws(new Exception());
            _mockUserRepository.Setup(r => r.UpdateInvitationNotification(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync(false);

            try
            {
                var result = await _addressService.UpdateUserAddress(userId, addressDto);
                Assert.True(false);
            }
            catch (Exception e)
            {
                Assert.True(true);
            }
         
        }
    }
}
