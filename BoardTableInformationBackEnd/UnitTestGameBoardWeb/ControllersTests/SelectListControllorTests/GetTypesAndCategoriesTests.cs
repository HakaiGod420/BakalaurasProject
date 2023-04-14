using BoardTableInformationBackEnd.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTO;
using Moq;
using ServiceLayer.Interfaces;
using System.Security.Claims;

namespace UnitTestGameBoardWeb.ControllersTests.SelectListControllorTests
{
    public class GetTypesAndCategoriesTests
    {
        private readonly Mock<ISelectListService> _selectListServiceMock;
        private readonly SelectListController _selectListController;

        public GetTypesAndCategoriesTests()
        {
            _selectListServiceMock = new Mock<ISelectListService>();

            _selectListController = new SelectListController(_selectListServiceMock.Object);
        }

        [Fact]
        public async Task GetTypesAndCategories_ReturnsTypesAndCategoriesDto()
        {
            // Arrange
            var expectedTypesAndCategoriesDto = new TypesAndCategoriesDto
            {
                Types = new List<TypeDTO> { new TypeDTO { Value = "Strategy", Label = "Strategy" } },
                Categories = new List<CategoryDTO> { new CategoryDTO { Value = "Board Games", Label = "Board Games" } }
            };
            _selectListServiceMock.Setup(x => x.GetTypesAndCategories()).ReturnsAsync(expectedTypesAndCategoriesDto);

            // Act
            var result = await _selectListController.GetTypesAndCategories();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var typesAndCategoriesDto = Assert.IsType<TypesAndCategoriesDto>(okResult.Value);
            Assert.Equal(expectedTypesAndCategoriesDto.Types, typesAndCategoriesDto.Types);
            Assert.Equal(expectedTypesAndCategoriesDto.Categories, typesAndCategoriesDto.Categories);
        }
    }
}
