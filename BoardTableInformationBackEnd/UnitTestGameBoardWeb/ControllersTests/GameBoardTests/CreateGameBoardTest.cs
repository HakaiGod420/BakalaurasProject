using BoardTableInformationBackEnd.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTO;
using Moq;
using ServiceLayer.Interfaces;
using System.Security.Claims;

namespace UnitTestGameBoardWeb.ControllersTests.GameBoardTests
{
    public class CreateGameBoardTest
    {
        private Mock<IBoardGameService> _gameBoardServiceMock;
        private GameBoardController _gameBoardController;
        private int _userId;

        public CreateGameBoardTest()
        {
            _gameBoardServiceMock = new Mock<IBoardGameService>();
            _gameBoardController = new GameBoardController(_gameBoardServiceMock.Object);
            _userId = 1;
            var claims = new List<Claim> { new Claim("UserId", _userId.ToString()) };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var user = new ClaimsPrincipal(identity);
            _gameBoardController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };
        }

        [Fact]
        public async Task CreateGameBoard_ValidModel_ReturnsCreatedGameBoard()
        {
            // Arrange
            var boardGameModel = new CreateBoardGame { Title = "Monopoly", PlayerCount = 4, PlayableAge = 8, Description = "A classic board game" };
            var expectedCreatedGameBoard = new CreatedGameBoard { Id = 1, Title = "Monopoly", CreatedAt = DateTime.Now };
            _gameBoardServiceMock.Setup(x => x.CreateGameBoard(boardGameModel, _userId)).ReturnsAsync(expectedCreatedGameBoard);

            // Act
            var result = await _gameBoardController.CreateGameBoard(boardGameModel);

            // Assert
            var createdResult = Assert.IsType<CreatedResult>(result);
            var createdGameBoard = Assert.IsType<CreatedGameBoard>(createdResult.Value);
            Assert.Equal(expectedCreatedGameBoard, createdGameBoard);
        }

        [Fact]
        public async Task CreateGameBoard_NullModel_ReturnsBadRequest()
        {
            // Arrange

            // Act
            var result = await _gameBoardController.CreateGameBoard(null);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
        }

        [Fact]
        public async Task CreateGameBoard_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            var boardGameModel = new CreateBoardGame { Title = "", PlayerCount = 4, PlayableAge = 8, Description = "A classic board game" };
            _gameBoardController.ModelState.AddModelError("Title", "The Title field is required");

            // Act
            var result = await _gameBoardController.CreateGameBoard(boardGameModel);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
        }
    }

}
