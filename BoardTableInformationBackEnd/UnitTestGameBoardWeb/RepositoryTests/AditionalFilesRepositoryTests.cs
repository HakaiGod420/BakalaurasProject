using DataLayer.DBContext;
using DataLayer.Models;
using DataLayer.Repositories.AditionalFiles;
using DataLayer.Repositories.BoardType;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestGameBoardWeb.RepositoryTests
{
    public class AditionalFilesRepositoryTests
    {
        private readonly AditionalFilesRepository _repository;
        private readonly DbContextOptionsBuilder<DataBaseContext> _optionsBuilder;
        private readonly DataBaseContext _context;

        public AditionalFilesRepositoryTests()
        {
            _optionsBuilder = new DbContextOptionsBuilder<DataBaseContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase");
            _context = new DataBaseContext(_optionsBuilder.Options);

            _repository = new AditionalFilesRepository(_context);
        }

        [Fact]
        public async Task AddFile_ValidFile_ReturnsAditionalFileEntity()
        {
            // Arrange
            var file = new AdditionalFileEntity { FileName = "Test File", FileLocation = "/test/file/path",BoardGameId = 1 };

            // Act
            var result = await _repository.AddFile(file);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(file.AditionalFilesId, result.AditionalFilesId);
            Assert.Equal(file.FileName, result.FileName);
            Assert.Equal(file.FileLocation, result.FileLocation);
            Assert.Equal(file.BoardGameId, result.BoardGameId);
        }

        [Fact]
        public async Task AddFile_NullFile_ThrowsArgumentNullException()
        {
            // Act and Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _repository.AddFile(null));
        }
    }
}
