using DataLayer.DBContext;
using DataLayer.Models;
using DataLayer.Repositories.Address;
using DataLayer.Repositories.AditionalFiles;
using DataLayer.Repositories.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;


namespace UnitTestGameBoardWeb.RepositoryTests
{
    public class AddressRepositoryTests
    {
        private readonly AddressRepository _repository;
        private readonly DbContextOptionsBuilder<DataBaseContext> _optionsBuilder;
        private readonly DataBaseContext _context;

        public AddressRepositoryTests()
        {
            _optionsBuilder = new DbContextOptionsBuilder<DataBaseContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase");
            _context = new DataBaseContext(_optionsBuilder.Options);

            _repository = new AddressRepository(_context);
        }

        [Fact]
        public async Task AddAddress_ValidAddress_ReturnsAddressEntity()
        {
            // Arrange
            var address = new AddressEntity
            {
                Country = "Test Country",
                City = "Test City",
                StreetName = "Test Street",
                Province = "Test Province",
                PostalCode = "12345",
                FullAddress = "Test Full Address"
            };

            // Act
            var result = await _repository.AddAddress(address);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(address.AddressId, result.AddressId);
            Assert.Equal(address.Country, result.Country);
            Assert.Equal(address.City, result.City);
            Assert.Equal(address.StreetName, result.StreetName);
            Assert.Equal(address.Province, result.Province);
            Assert.Equal(address.PostalCode, result.PostalCode);
            Assert.Equal(address.FullAddress, result.FullAddress);
        }

        [Fact]
        public async Task AddAddress_NullAddress_ThrowsArgumentNullException()
        {
            // Act and Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _repository.AddAddress(null));
        }

        [Fact]
        public async Task AddAddressToUser_ValidAddress_ReturnsUserAddressEntity()
        {
            // Arrange
            var address = new UserAddressEntity
            {
                Country = "Test Country",
                City = "Test City",
                StreetName = "Test Street",
                Province = "Test Province",
                Map_X_Coords = 123.456,
                Map_Y_Coords = 789.012
            };

            // Act
            var result = await _repository.AddAddressToUser(address);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(address.UserAddressId, result.UserAddressId);
            Assert.Equal(address.Country, result.Country);
            Assert.Equal(address.City, result.City);
            Assert.Equal(address.StreetName, result.StreetName);
            Assert.Equal(address.Province, result.Province);
            Assert.Equal(address.Map_X_Coords, result.Map_X_Coords);
            Assert.Equal(address.Map_Y_Coords, result.Map_Y_Coords);
        }

        [Fact]
        public async Task AddAddressToUser_NullAddress_ThrowsArgumentNullException()
        {

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _repository.AddAddressToUser(null));
        }

        [Fact]
        public async Task CheckIfExistAddress_ExistingAddress_ReturnsAddressEntity()
        {
            // Arrange
            var address = new AddressEntity
            {
                Country = "Test Country",
                City = "Test City",
                StreetName = "Test Street",
                Province = "Test Province",
                PostalCode = "12345",
                FullAddress = "Test Full Address"
            };
            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.CheckIfExistAddress("Test Full Address");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(address.AddressId, result.AddressId);
            Assert.Equal(address.Country, result.Country);
            Assert.Equal(address.City, result.City);
            Assert.Equal(address.StreetName, result.StreetName);
            Assert.Equal(address.Province, result.Province);
            Assert.Equal(address.PostalCode, result.PostalCode);
            Assert.Equal(address.FullAddress, result.FullAddress);
        }

        [Fact]
        public async Task CheckIfExistAddress_NonexistentAddress_ReturnsNull()
        {

            // Act
            var result = await _repository.CheckIfExistAddress("Nonexistent Full Address");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CheckIfUserHasAddress_UserWithAddress_ReturnsAddressId()
        {
            // Arrange
            var user = new UserEntity {
                UserId = 1,
                UserName = "testuser",
                Email = "TestEmail@gmail.com",
                Password = new byte[9],
                PasswordSalt = new byte[50],
                RegistrationTime = DateTime.Now,
                LastTimeConnection = DateTime.Now,AddressId = 1
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.CheckIfUserHasAddress(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.AddressId, result);
        }

        [Fact]
        public async Task CheckIfUserHasAddress_UserWithoutAddress_ReturnsNull()
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
                LastTimeConnection = DateTime.Now,
                AddressId = null
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.CheckIfUserHasAddress(2);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CheckIfUserHasAddress_NonexistentUser_ReturnsNull()
        {

            // Act
            var result = await _repository.CheckIfUserHasAddress(3);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateUserAddress_ValidAddress_ReturnsUpdatedAddressEntity()
        {
            // Arrange
            var address = new UserAddressEntity
            {
                UserAddressId = 1,
                Country = "Test Country",
                City = "Test City",
                StreetName = "Test Street",
                Province = "Test Province",
                Map_X_Coords = 123.456,
                Map_Y_Coords = 789.012
            };
            _context.UserAddress.Add(address);
            await _context.SaveChangesAsync();

            // Update the address
            address.StreetName = "Updated Street";
            address.Map_X_Coords = 111.222;
            address.Map_Y_Coords = 333.444;

            // Act
            var result = await _repository.UpdateUserAddress(address);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(address.UserAddressId, result.UserAddressId);
            Assert.Equal(address.Country, result.Country);
            Assert.Equal(address.City, result.City);
            Assert.Equal(address.StreetName, result.StreetName);
            Assert.Equal(address.Province, result.Province);
            Assert.Equal(address.Map_X_Coords, result.Map_X_Coords);
            Assert.Equal(address.Map_Y_Coords, result.Map_Y_Coords);
        }

        [Fact]
        public async Task UpdateUserAddress_NullAddress_ThrowsArgumentNullException()
        {

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _repository.UpdateUserAddress(null));
        }
    }
}
