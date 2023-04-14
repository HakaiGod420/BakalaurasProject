using BoardTableInformationBackEnd.Controllers;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTO;
using Moq;
using ServiceLayer.Interfaces;

namespace UnitTestGameBoardWeb.ControllersTests.GameBoardTests
{
    public class GameBoardGameGetBoardsListTest
    {
        private Mock<IBoardGameService> _gameBoardServiceMock;
        private GameBoardController _gameBoardController;

        public GameBoardGameGetBoardsListTest()
        {
            _gameBoardServiceMock = new Mock<IBoardGameService>();
            _gameBoardController = new GameBoardController(_gameBoardServiceMock.Object);
        }

        [Fact]
        public async Task GetBoards_WithNullSearchTerm_ReturnsAllBoardGames()
        {
            // Arrange
            var expectedListOfBoards = new List<BoardGameSimpleDto> { new BoardGameSimpleDto { Id = 1, Title = "Monopoly" } };
            _gameBoardServiceMock.Setup(x => x.GetBoardGamesForSelect(null)).ReturnsAsync(expectedListOfBoards);

            // Act
            var result = await _gameBoardController.GetBoards(null);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var listOfBoardsResult = Assert.IsType<List<BoardGameSimpleDto>>(okResult.Value);
            Assert.Equal(expectedListOfBoards, listOfBoardsResult);
        }

        [Fact]
        public async Task GetBoards_WithEmptySearchTerm_ReturnsAllBoardGames()
        {
            // Arrange
            var expectedListOfBoards = new List<BoardGameSimpleDto> { new BoardGameSimpleDto { Id = 1, Title = "Monopoly" } };
            _gameBoardServiceMock.Setup(x => x.GetBoardGamesForSelect("")).ReturnsAsync(expectedListOfBoards);

            // Act
            var result = await _gameBoardController.GetBoards("");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var listOfBoardsResult = Assert.IsType<List<BoardGameSimpleDto>>(okResult.Value);
            Assert.Equal(expectedListOfBoards, listOfBoardsResult);
        }

        [Fact]
        public async Task GetBoards_WithMatchingSearchTerm_ReturnsMatchingBoardGames()
        {
            // Arrange
            var searchTerm = "Monopoly";
            var expectedListOfBoards = new List<BoardGameSimpleDto> { new BoardGameSimpleDto { Id = 1, Title = searchTerm } };
            _gameBoardServiceMock.Setup(x => x.GetBoardGamesForSelect(searchTerm)).ReturnsAsync(expectedListOfBoards);

            // Act
            var result = await _gameBoardController.GetBoards(searchTerm);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var listOfBoardsResult = Assert.IsType<List<BoardGameSimpleDto>>(okResult.Value);
            Assert.Equal(expectedListOfBoards, listOfBoardsResult);
        }

        [Fact]
        public async Task GetBoards_WithNonMatchingSearchTerm_ReturnsEmptyList()
        {
            // Arrange
            var searchTerm = "Chess";
            var expectedListOfBoards = new List<BoardGameSimpleDto>();
            _gameBoardServiceMock.Setup(x => x.GetBoardGamesForSelect(searchTerm)).ReturnsAsync(expectedListOfBoards);

            // Act
            var result = await _gameBoardController.GetBoards(searchTerm);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var listOfBoardsResult = Assert.IsType<List<BoardGameSimpleDto>>(okResult.Value);
            Assert.Empty(listOfBoardsResult);
        }
    }
}

