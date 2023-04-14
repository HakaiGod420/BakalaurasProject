using System.Collections.Generic;
using System.Threading.Tasks;
using BoardTableInformationBackEnd.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTO;
using Moq;
using ServiceLayer.Interfaces;
using Xunit;


namespace UnitTestGameBoardWeb.ControllersTests.GameBoardTests
{
    public class GetSingleBoardTest
    {
        private Mock<IBoardGameService> _gameBoardServiceMock;
        private GameBoardController _gameBoardController;

        public GetSingleBoardTest()
        {
            _gameBoardServiceMock = new Mock<IBoardGameService>();
            _gameBoardController = new GameBoardController(_gameBoardServiceMock.Object);
        }

        [Fact]
        public async Task GetSingleBoard_WithValidId_ReturnsSingleGameBoardView()
        {
            // Arrange
            var boardId = 1;
            var expectedBoard = new SingleGameBoardView { BoardGameId = boardId, Title = "Monopoly" };
            _gameBoardServiceMock.Setup(x => x.GetGameBoard(boardId)).ReturnsAsync(expectedBoard);

            // Act
            var result = await _gameBoardController.GetSingleBoard(boardId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var singleGameBoardResult = Assert.IsType<SingleGameBoardView>(okResult.Value);
            Assert.Equal(expectedBoard, singleGameBoardResult);
        }

        [Fact]
        public async Task GetSingleBoard_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            var boardId = 1;
            _gameBoardServiceMock.Setup(x => x.GetGameBoard(boardId)).ReturnsAsync((SingleGameBoardView)null);

            // Act
            var result = await _gameBoardController.GetSingleBoard(boardId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Game board was not found", notFoundResult.Value);
        }

        [Fact]
        public async Task GetSingleBoard_WithZeroId_ReturnsNotFound()
        {
            // Arrange
            var boardId = 0;
            _gameBoardServiceMock.Setup(x => x.GetGameBoard(boardId)).ReturnsAsync((SingleGameBoardView)null);

            // Act
            var result = await _gameBoardController.GetSingleBoard(boardId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Game board was not found", notFoundResult.Value);
        }
    }
}
