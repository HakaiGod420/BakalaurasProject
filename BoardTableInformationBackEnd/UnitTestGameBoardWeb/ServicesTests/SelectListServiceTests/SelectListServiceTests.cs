using DataLayer.Repositories.BoardType;
using DataLayer.Repositories.Category;
using ModelLayer.DTO;
using Moq;
using ServiceLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestGameBoardWeb.ServicesTests.SelectListServiceTests
{
    public class SelectListServiceTests
    {
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
        private readonly Mock<IBoardTypeRepository> _boardTypeRepositoryMock;
        private readonly SelectListService _selectListService;

        public SelectListServiceTests()
        {
            _boardTypeRepositoryMock = new Mock<IBoardTypeRepository>();
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _selectListService = new SelectListService(_categoryRepositoryMock.Object, _boardTypeRepositoryMock.Object);
        }

        [Fact]
        public async Task GetTypesAndCategories_ShouldReturnTypesAndCategoriesDto()
        {
            // Arrange
            var categories = new List<CategoryDTO>
        {
            new CategoryDTO { Value = "1", Label = "Category 1" },
            new CategoryDTO { Value = "2", Label = "Category 2" }
        };
            _categoryRepositoryMock.Setup(x => x.GetCategories()).ReturnsAsync(categories);

            var types = new List<TypeDTO>
        {
            new TypeDTO { Value = "1", Label = "Type 1" },
            new TypeDTO { Value = "2", Label = "Type 2" }
        };
            _boardTypeRepositoryMock.Setup(x => x.GetTypes()).ReturnsAsync(types);

            // Act
            var result = await _selectListService.GetTypesAndCategories();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<TypesAndCategoriesDto>(result);
            Assert.Equal(2, result.Categories.Count());
            Assert.Equal(2, result.Types.Count());
        }

        [Fact]
        public async Task GetTypesAndCategories_ShouldReturnValidDto_WhenCategoriesExistButTypesAreEmpty()
        {
            // Arrange
            var categories = new List<CategoryDTO>
            {
                new CategoryDTO { Value = "1", Label = "Category 1" },
                new CategoryDTO { Value = "2", Label = "Category 2" },
            };

            var types = new List<TypeDTO>();

            _categoryRepositoryMock.Setup(r => r.GetCategories()).ReturnsAsync(categories);
            _boardTypeRepositoryMock.Setup(r => r.GetTypes()).ReturnsAsync(types);

            // Act
            var result = await _selectListService.GetTypesAndCategories();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(categories, result.Categories);
            Assert.Equal(types, result.Types);
        }

        [Fact]
        public async Task GetTypesAndCategories_ShouldReturnValidDto_WhenTypesExistButCategoriesAreEmpty()
        {
            // Arrange
            var categories = new List<CategoryDTO>();
            var types = new List<TypeDTO>
            {
                new TypeDTO { Value = "1", Label = "Type 1" },
                new TypeDTO { Value = "2", Label = "Type 2" },
            };

            _categoryRepositoryMock.Setup(r => r.GetCategories()).ReturnsAsync(categories);
            _boardTypeRepositoryMock.Setup(r => r.GetTypes()).ReturnsAsync(types);

            // Act
            var result = await _selectListService.GetTypesAndCategories();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(categories, result.Categories);
            Assert.Equal(types, result.Types);
        }

        [Fact]
        public async Task GetTypesAndCategories_ShouldThrowException_When_GetTypes_ThrowsException()
        {
            // Arrange
            var expectedException = new Exception("Some exception message.");
            _categoryRepositoryMock.Setup(r => r.GetCategories()).ReturnsAsync(new List<CategoryDTO>());
            _boardTypeRepositoryMock.Setup(r => r.GetTypes()).ThrowsAsync(expectedException);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _selectListService.GetTypesAndCategories());
        }
    }
}
