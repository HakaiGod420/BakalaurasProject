using DataLayer.Models;
using DataLayer.Repositories.AditionalFiles;
using DataLayer.Repositories.BoardType;
using DataLayer.Repositories.Category;
using DataLayer.Repositories.GameBoard;
using DataLayer.Repositories.Image;
using ModelLayer.DTO;
using ServiceLayer.Interfaces;

namespace ServiceLayer.Services
{
    public class BoardGameService : IBoardGameService
    {
        private readonly IGameBoardRepository _gameBoardRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBoardTypeRepository _boardTypeRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IAditionalFilesRepository _aditionalFilesRepository;

        public BoardGameService(IGameBoardRepository gameBoardRepository,
            IBoardTypeRepository boardTypeRepository,
            ICategoryRepository categoryRepository,
            IImageRepository imageRepository,
            IAditionalFilesRepository aditionalFilesRepository)
        {
            _gameBoardRepository = gameBoardRepository;
            _categoryRepository = categoryRepository;
            _boardTypeRepository = boardTypeRepository;
            _imageRepository = imageRepository;
            _aditionalFilesRepository = aditionalFilesRepository;
        }

        public async Task<CreatedGameBoard> CreateGameBoard(CreateBoardGame board,int userId)
        {
            if(board == null)
            {
                throw new ArgumentNullException(nameof(board));
            }

            var gameBoard = new BoardGameEntity
            {
                Title = board.Title,
                PlayerCount = board.PlayerCount,
                PlayingTime = board.PlayingTime,
                PlayableAge = board.PlayableAge,
                Description = board.Description,
                CreationTime = DateTime.Now,
                UpdateTime = null,
                Rules = board.Rules,
                Thubnail_Location = board.ThumbnailName,
                UserId = userId,
                TableBoardStateId = ModelLayer.Enum.TableBoardState.Reviewing,
            };

            foreach(var item in board.Categories)
            {
                var category = await GetCategory(item.CategoryName);
                gameBoard.Categories.Add(category);
            }

            foreach(var item in board.BoardTypes)
            {
                var type = await GetType(item.BoardTypeName);
                gameBoard.BoardTypes.Add(type);
            }
            var boardEntity = await _gameBoardRepository.AddGameBoard(gameBoard);

            foreach (var item in board.Images)
            {
                var image = await AddImage(item,boardEntity.BoardGameId);
                gameBoard.Images.Add(image);
            }

            if(board.AditionalFiles!= null)
            {
                foreach (var item in board.AditionalFiles)
                {
                    var file = await AddFile(item,boardEntity.BoardGameId);
                    gameBoard.AditionalFiles.Add(file);
                }
            }

            
            return new CreatedGameBoard
            {
                Id = gameBoard.BoardGameId,
                Title = gameBoard.Title,
                CreatedAt = gameBoard.CreationTime,

            };
        }

        private async Task<CategoryEntity> GetCategory(string categoryName)
        {
            var category = await _categoryRepository.GetCategory(categoryName);

            if(category == null)
            {
                var newCategory = new CategoryEntity { CategoryName = categoryName };
                var createdCategory = await _categoryRepository.CreateCategory(newCategory);
                return createdCategory;
            }

            return category;
        }

        private async Task<BoardTypeEntity> GetType(string typeName)
        {
            var type = await _boardTypeRepository.GetType(typeName);

            if (type == null)
            {
                var newType = new BoardTypeEntity { BoardTypeName = typeName };
                var createdType = await _boardTypeRepository.CreateType(newType);
                return createdType;
            }

            return type;
        }

        private async Task<ImageEntity> AddImage(CreateImage image, int? id = null)
        {
            var newImage = new ImageEntity
            {
                Location = image.Location,
                Alias = image.Alias,
                BoardGameId = id,
            };
            return await _imageRepository.AddImage(newImage);
        }

        private async Task<AditionalFileEntity> AddFile(CreateAditionalFiles file,int id)
        {
            var newFile = new AditionalFileEntity
            {
                FileLocation = file.Location,
                FileName = file.Name,
                BoardGameId = id
            };
            return await _aditionalFilesRepository.AddFile(newFile);
        }

        public async Task<List<BoardGameSimpleDto>> GetBoardGamesForSelect(string stringPart)
        {
            return await _gameBoardRepository.GetBoardsSimple(stringPart);
        }

        public async Task<GameCardListResponse> GetBoardCardItems(int startIndex, int backIndex, string? searchTerm, FilterDTO filter)
        {
            return await _gameBoardRepository.GetGameBoardInfo(startIndex,backIndex, searchTerm, filter);
        }

        public async Task<SingleGameBoardView?> GetGameBoard(int boardId)
        {
            return await _gameBoardRepository.GetGameBoard(boardId);
        }

        public Task<bool> IsGameBoardExist(string gameBoardTitle)
        {
            return _gameBoardRepository.IsGameBoardExist(gameBoardTitle);
        }

        public async Task<UserCreatedTableTopGamesResponse> GetGameBoardsCreatedByUser(string username, int pageIndex, int pageSize)
        {
            return await _gameBoardRepository.GetGameBoardUsers(username, pageIndex, pageSize);
        }

        public async Task UpdateGameBoard()
        {
            return await _gameBoardRepository.UpdateGameBoard();
        }
    }
}
