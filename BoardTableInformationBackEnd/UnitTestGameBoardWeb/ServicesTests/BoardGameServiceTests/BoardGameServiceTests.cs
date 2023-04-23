using DataLayer.Models;
using DataLayer.Repositories.Address;
using DataLayer.Repositories.AditionalFiles;
using DataLayer.Repositories.BoardType;
using DataLayer.Repositories.Category;
using DataLayer.Repositories.GameBoard;
using DataLayer.Repositories.Image;
using DataLayer.Repositories.Invitation;
using DataLayer.Repositories.User;
using ModelLayer.DTO;
using Moq;
using ServiceLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestGameBoardWeb.ServicesTests.BoardGameServiceTests
{
    public class BoardGameServiceTests
    {
        private readonly Mock<IGameBoardRepository> _mockGameBoardRepository;
        private readonly Mock<ICategoryRepository> _mockCaregoryRepository;
        private readonly Mock<IBoardTypeRepository> _mockBoardTypeRepository;
        private readonly Mock<IImageRepository> _mockImageRepository;
        private readonly Mock<IAditionalFilesRepository> _mockAditionalFileRepository;
        private readonly BoardGameService _boardGameService;

        public BoardGameServiceTests()
        {
            _mockGameBoardRepository = new Mock<IGameBoardRepository>();
            _mockCaregoryRepository = new Mock<ICategoryRepository>();
            _mockBoardTypeRepository = new Mock<IBoardTypeRepository>();
            _mockImageRepository = new Mock<IImageRepository>();
            _mockAditionalFileRepository = new Mock<IAditionalFilesRepository>();
            _boardGameService = new BoardGameService(_mockGameBoardRepository.Object,
                _mockBoardTypeRepository.Object,
                _mockCaregoryRepository.Object,
                _mockImageRepository.Object,
                _mockAditionalFileRepository.Object);
        }

        [Fact]
        public async Task GetGameBoard_ReturnsSingleGameBoardView_WhenCalledWithBoardId()
        {
            // Arrange
            int boardId = 1;

            SingleGameBoardView expectedGameBoard = new SingleGameBoardView
            {
                BoardGameId = 1,
                Title = "Test Game Board",
                PlayerCount = 4,
                PlayableAge = 12,
                Description = "This is a test game board.",
                CreationTime = DateTime.Now,
                UpdateDate = DateTime.Now,
                Rules = "Test game board rules.",
                Thumbnail_Location = "https://test.com/image.jpg",
                CreatorName = "Test User",
                CreatorId = 1,
                Categories = new List<string> { "Strategy", "Simulation" },
                Types = new List<string> { "Board Game" },
                Images = new List<GetImageDTO> { new GetImageDTO { FileName = "image.jpg", Location = "https://test.com/image.jpg" } },
                Files = new List<GetAditionalFilesDTO> { new GetAditionalFilesDTO { FileName = "file.pdf", Location = "https://test.com/file.pdf" } },
                Rating = 4.5
            };

            _mockGameBoardRepository.Setup(repo => repo.GetGameBoard(boardId)).ReturnsAsync(expectedGameBoard);

            // Act
            var result = await _boardGameService.GetGameBoard(boardId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<SingleGameBoardView>(result);
            Assert.Equal(expectedGameBoard.BoardGameId, result.BoardGameId);
            Assert.Equal(expectedGameBoard.Title, result.Title);
            Assert.Equal(expectedGameBoard.PlayerCount, result.PlayerCount);
            Assert.Equal(expectedGameBoard.PlayableAge, result.PlayableAge);
            Assert.Equal(expectedGameBoard.Description, result.Description);
            Assert.Equal(expectedGameBoard.CreationTime, result.CreationTime);
            Assert.Equal(expectedGameBoard.UpdateDate, result.UpdateDate);
            Assert.Equal(expectedGameBoard.Rules, result.Rules);
            Assert.Equal(expectedGameBoard.Thumbnail_Location, result.Thumbnail_Location);
            Assert.Equal(expectedGameBoard.CreatorName, result.CreatorName);
            Assert.Equal(expectedGameBoard.CreatorId, result.CreatorId);
            Assert.Equal(expectedGameBoard.Categories, result.Categories);
            Assert.Equal(expectedGameBoard.Types, result.Types);
            Assert.Equal(expectedGameBoard.Images[0].FileName, result.Images[0].FileName);
            Assert.Equal(expectedGameBoard.Images[0].Location, result.Images[0].Location);
            Assert.Equal(expectedGameBoard.Files[0].FileName, result.Files[0].FileName);
            Assert.Equal(expectedGameBoard.Files[0].Location, result.Files[0].Location);
            Assert.Equal(expectedGameBoard.Rating, result.Rating);
        }

        [Fact]
        public async Task GetBoardCardItems_ShouldReturnListOfGameBoardCardItemDTO()
        {
            // Arrange
            var expectedList = new List<GameBoardCardItemDTO>()
    {
        new GameBoardCardItemDTO()
        {
            GameBoardId = 1,
            Title = "Board Game 1",
            ReleaseDate = DateTime.Now,
            ThumbnailURL = "thumbnail_url_1",
            ThumbnailName = "thumbnail_name_1",
            Rating = 4.5
        },
        new GameBoardCardItemDTO()
        {
            GameBoardId = 2,
            Title = "Board Game 2",
            ReleaseDate = DateTime.Now,
            ThumbnailURL = "thumbnail_url_2",
            ThumbnailName = "thumbnail_name_2",
            Rating = 3.2
        }
    };
            var expectedTotalCount = 2;

            _mockGameBoardRepository.Setup(x => x.GetGameBoardInfo(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<FilterDTO>()))
                                   .ReturnsAsync(new GameCardListResponse()
                                   {
                                       BoardGames = expectedList,
                                       TotalCount = expectedTotalCount
                                   });

            // Act
            var result = await _boardGameService.GetBoardCardItems(0, 10, null, new FilterDTO());

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedTotalCount, result.TotalCount);
            Assert.Equal(expectedList, result.BoardGames);
        }

        [Fact]
        public async Task GetBoardCardItems_ShouldThrowException_WhenRepositoryThrowsException()
        {
            // Arrange
            _mockGameBoardRepository.Setup(x => x.GetGameBoardInfo(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<FilterDTO>()))
                                   .ThrowsAsync(new Exception("Something went wrong"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => await _boardGameService.GetBoardCardItems(0, 10, null, new FilterDTO()));
        }

        [Fact]
        public async Task GetBoardGamesForSelect_ShouldReturnEmptyList_WhenStringPartIsEmpty()
        {
            // Arrange
            var stringPart = "";
            _mockGameBoardRepository.Setup(repo => repo.GetBoardsSimple(stringPart))
                .ReturnsAsync(new List<BoardGameSimpleDto>());

            // Act
            var result = await _boardGameService.GetBoardGamesForSelect(stringPart);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<BoardGameSimpleDto>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetBoardGamesForSelect_ShouldReturnMatchingGames_WhenStringPartIsProvided()
        {
            // Arrange
            var stringPart = "Catan";
            _mockGameBoardRepository.Setup(repo => repo.GetBoardsSimple(stringPart))
                .ReturnsAsync(new List<BoardGameSimpleDto>
                {
                new BoardGameSimpleDto { Id = 1, Title = "Catan" },
                new BoardGameSimpleDto { Id = 2, Title = "Catan: Seafarers" },
                new BoardGameSimpleDto { Id = 3, Title = "Catan: Cities and Knights" }
                });


            // Act
            var result = await _boardGameService.GetBoardGamesForSelect(stringPart);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<BoardGameSimpleDto>>(result);
            Assert.Equal(3, result.Count);

            Assert.Equal("Catan", result[0].Title);
            Assert.Equal("Catan: Seafarers", result[1].Title);
            Assert.Equal("Catan: Cities and Knights", result[2].Title);
        }

        [Fact]
        public async Task CreateGameBoard_ValidInputs_ReturnsCreatedGameBoard()
        {
            // Arrange
            var board = new CreateBoardGame
            {
                Title = "Test Game",
                PlayerCount = 2,
                PlayingTime = 60,
                PlayableAge = 12,
                Description = "A test game",
                Rules = "Test game rules",
                ThumbnailName = "test_thumbnail.jpg",
                Categories = new List<CreateCategory> { new CreateCategory { CategoryName = "Test Category" } },
                BoardTypes = new List<CreateBoardType> { new CreateBoardType { BoardTypeName = "Test Board Type" } },
                Images = new List<CreateImage> { new CreateImage { Location = "test_image.jpg" } }
            };
            var userId = 1;

            _mockCaregoryRepository.Setup(x => x.GetCategory("Test Category"))
                                   .ReturnsAsync(new CategoryEntity { CategoryName = "Test Category" });
            _mockBoardTypeRepository.Setup(x => x.GetType("Test Board Type"))
                                    .ReturnsAsync(new BoardTypeEntity { BoardTypeName = "Test Board Type" });
            _mockGameBoardRepository.Setup(x => x.AddGameBoard(It.IsAny<BoardGameEntity>()))
                                    .ReturnsAsync(new BoardGameEntity { BoardGameId = 1, Title = "Test Game" });
            _mockImageRepository.Setup(x => x.AddImage(It.IsAny<ImageEntity>()))
                                .ReturnsAsync(new ImageEntity { ImageId = 1, Location = "test_image.jpg" });

            // Act
            var result = await _boardGameService.CreateGameBoard(board, userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.Id);
            Assert.Equal("Test Game", result.Title);
            Assert.True(DateTime.Now.AddSeconds(-1) <= result.CreatedAt && result.CreatedAt <= DateTime.Now);
        }

        [Fact]
        public async Task TestCreateGameBoard_NullInput_ShouldThrowException()
        {
            // Arrange
            CreateBoardGame board = null;
            int userId = 1;

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () => await _boardGameService.CreateGameBoard(board, userId));
        }

        [Fact]
        public async Task TestCreateGameBoard_InvalidCategory_ShouldCreateNewCategory()
        {

            // Set up the mock to return null for an existing category
            _mockCaregoryRepository.Setup(r => r.GetCategory(It.IsAny<string>()))
                                  .ReturnsAsync((CategoryEntity)null);

            // Set up the mock to return a new category entity when CreateCategory is called
            _mockCaregoryRepository.Setup(r => r.CreateCategory(It.IsAny<CategoryEntity>()))
                                  .ReturnsAsync(new CategoryEntity
                                  {
                                      CategoryId = 1,
                                      CategoryName = "Test Category",
                                      IsActive = true
                                  });

            var board = new CreateBoardGame
            {
                Title = "Test Board Game",
                Categories = new List<CreateCategory>(),
                BoardTypes = new List<CreateBoardType>(),
                Images = new List<CreateImage>(),
                AditionalFiles = new List<CreateAditionalFiles>()
            };

            board.Categories.Add(new CreateCategory { CategoryName = "New Category" });

            // Act
            var result = await _boardGameService.CreateGameBoard(board, 1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test Board Game", result.Title);
            Assert.Equal(0, result.Id);
            Assert.True(result.CreatedAt > DateTime.MinValue);

            _mockCaregoryRepository.Verify(r => r.GetCategory("New Category"), Times.Once);
            _mockCaregoryRepository.Verify(r => r.CreateCategory(It.IsAny<CategoryEntity>()), Times.Once);
            _mockGameBoardRepository.Verify(r => r.AddGameBoard(It.IsAny<BoardGameEntity>()), Times.Once);
        }
    }
}
