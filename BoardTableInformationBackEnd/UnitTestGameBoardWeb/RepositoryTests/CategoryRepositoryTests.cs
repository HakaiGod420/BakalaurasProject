using DataLayer.DBContext;
using DataLayer.Models;
using DataLayer.Repositories.Category;
using DataLayer.Repositories.Reviews;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestGameBoardWeb.RepositoryTests
{
    public class CategoryRepositoryTests
    {
        private readonly CategoryRepository _repository;
        private readonly DbContextOptionsBuilder<DataBaseContext> _optionsBuilder;
        private readonly DataBaseContext _context;

        public CategoryRepositoryTests()
        {
            _optionsBuilder = new DbContextOptionsBuilder<DataBaseContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase");
            _context = new DataBaseContext(_optionsBuilder.Options);

            _repository = new CategoryRepository(_context);
        }

        [Fact]
        public async Task CreateCategory_ValidCategory_ReturnsCategoryEntityWithId()
        {
            // Arrange
            var category = new CategoryEntity { CategoryName = "Test Category" };

            // Act
            var result = await _repository.CreateCategory(category);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.CategoryId > 0);
            Assert.Equal(category.CategoryName, result.CategoryName);
            Assert.False(result.IsActive);
        }

        [Fact]
        public async Task CreateCategory_CategoryWithSameNameAlreadyExists_RetursCreatedCategories()
        {
            // Arrange
            _context.Database.EnsureDeleted();
            var category1 = new CategoryEntity { CategoryName = "Test Category" };
            var category2 = new CategoryEntity { CategoryName = "Test Category2" };
            _context.Categories.Add(category1);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.CreateCategory(category2);

            // Assert that both categories are present in the database
            var categories = await _context.Categories.ToListAsync();
            Assert.Equal(2, categories.Count);
            Assert.Equal(category1.CategoryId, categories[0].CategoryId);
            Assert.Equal(category1.CategoryName, categories[0].CategoryName);
            Assert.False(categories[0].IsActive);
        }

        [Fact]
        public async Task GetCategories_NoCategories_ReturnsEmptyList()
        {

            // Act
            var result = await _repository.GetCategories();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetCategories_ActiveCategoriesExist_ReturnsListWithCategoryDTOs()
        {
            // Arrange
            _context.Database.EnsureDeleted();
            var category1 = new CategoryEntity { CategoryName = "Test Category 1", IsActive = true };
            var category2 = new CategoryEntity { CategoryName = "Test Category 2", IsActive = true };
            _context.Categories.AddRange(category1, category2);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetCategories();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal(category1.CategoryId.ToString(), result[0].Value);
            Assert.Equal(category1.CategoryName, result[0].Label);
            Assert.Equal(category2.CategoryId.ToString(), result[1].Value);
            Assert.Equal(category2.CategoryName, result[1].Label);
        }

        [Fact]
        public async Task GetCategories_ActiveAndInactiveCategoriesExist_ReturnsListWithOnlyActiveCategoryDTOs()
        {
            // Arrange
            var category1 = new CategoryEntity { CategoryName = "Test Category 1", IsActive = true };
            var category2 = new CategoryEntity { CategoryName = "Test Category 2", IsActive = false };
            _context.Categories.AddRange(category1, category2);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetCategories();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Count);
            Assert.Equal(category1.CategoryId.ToString(), result[0].Value);
            Assert.Equal(category1.CategoryName, result[0].Label);
        }

        [Fact]
        public async Task GetCategory_CategoryExists_ReturnsCategoryEntity()
        {
            // Arrange
            _context.Database.EnsureDeleted();
            var category = new CategoryEntity { CategoryName = "Test Category" };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetCategory("Test Category");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(category.CategoryId, result.CategoryId);
            Assert.Equal(category.CategoryName, result.CategoryName);
            Assert.Equal(category.IsActive, result.IsActive);
        }

        [Fact]
        public async Task GetCategory_CategoryDoesNotExist_ReturnsNull()
        {
            // Act
            var result = await _repository.GetCategory("Nonexistent Category");

            // Assert
            Assert.Null(result);
        }
    }
}
