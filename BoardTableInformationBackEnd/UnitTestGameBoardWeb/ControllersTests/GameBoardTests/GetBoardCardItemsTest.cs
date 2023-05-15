using BoardTableInformationBackEnd.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTO;
using Moq;
using ServiceLayer.Interfaces;
using System.Security.Claims;

namespace UnitTestGameBoardWeb.ControllersTests.GameBoardTests
{
    public class GetBoardCardItemsTest
    {
        private Mock<IBoardGameService> _gameBoardServiceMock;
        private GameBoardController _gameBoardController;

        public GetBoardCardItemsTest()
        {
            _gameBoardServiceMock = new Mock<IBoardGameService>();
            _gameBoardController = new GameBoardController(_gameBoardServiceMock.Object);
        }

        [Fact]
        public async Task GetBoardCardItems_ValidInputs_ReturnsGameCardListResponse()
        {
            // Arrange
            var startIndex = 0;
            var backIndex = 10;
            var searchTerm = "Monopoly";
            var filter = new FilterDTO();
            var expectedGameCardListResponse = new GameCardListResponse
            {
                BoardGames = new List<GameBoardCardItemDTO> { new GameBoardCardItemDTO { GameBoardId = 1, Title = "Monopoly", ReleaseDate = DateTime.Now, ThumbnailName = "monopoly.jpg" } },
                TotalCount = 1
            };

            _gameBoardServiceMock.Setup(x => x.GetBoardCardItems(startIndex, backIndex, searchTerm, filter)).ReturnsAsync(expectedGameCardListResponse);

            // Act
            var result = await _gameBoardController.GetBoardCardItems(startIndex, backIndex, filter, searchTerm);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var gameCardListResponse = Assert.IsType<GameCardListResponse>(okResult.Value);
            Assert.Equal(expectedGameCardListResponse, gameCardListResponse);
        }

        [Fact]
        public async Task GetBoardCardItems_StartIndexLessThanZero_ReturnsUnprocessableEntity()
        {
            // Arrange
            var startIndex = -1;
            var backIndex = 10;
            var filter = new FilterDTO();

            // Act
            var result = await _gameBoardController.GetBoardCardItems(startIndex, backIndex, filter, null);

            // Assert
            var unprocessableEntityResult = Assert.IsType<UnprocessableEntityObjectResult>(result);
            Assert.Equal(nameof(startIndex), unprocessableEntityResult.Value);
        }

        [Fact]
        public async Task GetBoardCardItems_BackIndexLessThanZero_ReturnsUnprocessableEntity()
        {
            // Arrange
            var startIndex = 0;
            var backIndex = -1;
            var filter = new FilterDTO();

            // Act
            var result = await _gameBoardController.GetBoardCardItems(startIndex, backIndex, filter, null);

            // Assert
            var unprocessableEntityResult = Assert.IsType<UnprocessableEntityObjectResult>(result);
            Assert.Equal(nameof(backIndex), unprocessableEntityResult.Value);
        }

        [Fact]
        public async Task GetBoardCardItems_ThumbnailFileExists_SetsThumbnailURL()
        {
            // Arrange
            var startIndex = 0;
            var backIndex = 10;
            var filter = new FilterDTO();
            var searchTerm = "Monopoly";
            var expectedGameCardListResponse = new GameCardListResponse
            {
                BoardGames = new List<GameBoardCardItemDTO> { new GameBoardCardItemDTO { GameBoardId = 1, Title = "Monopoly", ReleaseDate = DateTime.Now, ThumbnailName = "monopoly.jpg" } },
                TotalCount = 1
            };
            _gameBoardServiceMock.Setup(x => x.GetBoardCardItems(startIndex, backIndex, searchTerm, filter)).ReturnsAsync(expectedGameCardListResponse);
            var folderPath = Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\Files\\Images\\Monopoly");
            File.Create(folderPath.FullName + "\\monopoly.jpg").Close();

            // Act
            var result = await _gameBoardController.GetBoardCardItems(startIndex, backIndex, filter, searchTerm);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var gameCardListResponse = Assert.IsType<GameCardListResponse>(okResult.Value);
            var gameBoardCardItemDTO = Assert.Single(gameCardListResponse.BoardGames);
            Assert.Equal("/Images/Monopoly/monopoly.jpg", gameBoardCardItemDTO.ThumbnailURL);

            // Clean up
            folderPath.Delete(true);
        }

        [Fact]
        public async Task GetBoardCardItems_ThumbnailFileDoesNotExist_DoesNotSetThumbnailURL()
        {
            // Arrange
            var startIndex = 0;
            var backIndex = 10;
            var filter = new FilterDTO();
            var searchTerm = "Monopoly";
            var expectedGameCardListResponse = new GameCardListResponse
            {
                BoardGames = new List<GameBoardCardItemDTO> { new GameBoardCardItemDTO { GameBoardId = 1, Title = "Monopoly", ReleaseDate = DateTime.Now, ThumbnailName = "monopoly.jpg" } },
                TotalCount = 1
            };
            _gameBoardServiceMock.Setup(x => x.GetBoardCardItems(startIndex, backIndex, searchTerm, filter)).ReturnsAsync(expectedGameCardListResponse);

            // Act
            var result = await _gameBoardController.GetBoardCardItems(startIndex, backIndex, filter, searchTerm);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var gameCardListResponse = Assert.IsType<GameCardListResponse>(okResult.Value);
            var gameBoardCardItemDTO = Assert.Single(gameCardListResponse.BoardGames);
            Assert.Null(gameBoardCardItemDTO.ThumbnailURL);
        }
    }
}
