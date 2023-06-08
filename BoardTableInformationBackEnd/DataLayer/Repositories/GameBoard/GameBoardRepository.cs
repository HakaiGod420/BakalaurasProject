using DataLayer.DBContext;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using ModelLayer.DTO;
using ModelLayer.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataLayer.Repositories.GameBoard
{
    public class GameBoardRepository : IGameBoardRepository
    {
        private readonly DataBaseContext _dbContext;

        public GameBoardRepository(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<BoardGameEntity> AddGameBoard(BoardGameEntity tableBoard)
        {
            await _dbContext.BoardGames.AddAsync(tableBoard);
            _dbContext.SaveChanges();
            return tableBoard;
        }

        public async Task<bool> ChangeGameBoardState(int gameBoardId, bool isBlocked)
        {
            var gameBoard = await _dbContext.BoardGames.SingleOrDefaultAsync(x => x.BoardGameId == gameBoardId);

            if (gameBoard == null)
            {
                return false;
            }

            gameBoard.IsBlocked = isBlocked;
            gameBoard.UpdateTime = DateTime.Now;
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task DeleteImage(int imageId)
        {
            var image = await _dbContext.Images.FindAsync(imageId);

            if (image != null)
            {
                _dbContext.Images.Remove(image);
                await _dbContext.SaveChangesAsync();
            }
        }


        public async Task<GalleryForEdit> GalleryForEdit(int gameBoardId)
        {
            var galleryItems = await _dbContext.Images.Where(x => x.BoardGameId == gameBoardId)
                .Select(x => new GalleryElement
                {
                    ImageId = x.ImageId,
                    Location = x.Location + "/" + x.Alias,
                })
                .ToListAsync();

            return new GalleryForEdit
            {
                GalleryElements = galleryItems,
            };
        }

        public async Task<List<BoardGameSimpleDto>> GetBoardsSimple(string titlePart)
        {
            if(titlePart == null)
            {
                return new List<BoardGameSimpleDto>();
            }

            var result = await _dbContext.BoardGames.AsQueryable()
                .Where(x => x.Title.StartsWith(titlePart))
                .Select(x => new BoardGameSimpleDto { Id = x.BoardGameId, Title = x.Title })
                .ToListAsync();
            return result;
        }

        public async Task<SingleGameBoardView?> GetGameBoard(int id)
        {
            var result = await _dbContext.BoardGames
                .Include(a => a.Categories)
                .Include(a=> a.BoardTypes)
                .Include(a=>a.User)
                .Include(a=>a.Images)
                .Include(a=>a.AdditionalFiles)
                .Include(a=>a.Reviews)
                .SingleOrDefaultAsync(x=> x.BoardGameId == id);

            if (result != null)
            {
                var finalObject = new SingleGameBoardView
                {
                    BoardGameId = result.BoardGameId,
                    Title = result.Title,
                    PlayerCount = result.PlayerCount,
                    PlayingTime = result.PlayingTime,
                    PlayableAge = result.PlayableAge,
                    Description = result.Description,
                    CreationTime = result.CreationTime,
                    UpdateDate = result.UpdateTime,
                    Rules = result.Rules,
                    Thumbnail_Location = "Images/" + Regex.Replace(result.Title, @"[^\w\s]+", "").Replace(" ", "_") + "/" + result.Thumbnail_Location,
                    CreatorId = result.UserId,
                    CreatorName = result.User.UserName,
                    Categories = result.Categories.Select(x => x.CategoryName).ToList(),
                    Types = result.BoardTypes.Select(x => x.BoardTypeName).ToList(),
                    Images = result.Images.Select(x => new GetImageDTO { Location = x.Location + "/" + x.Alias, FileName = x.Alias }).ToList(),
                    Files = result.AdditionalFiles.Select(x => new GetAditionalFilesDTO { Location = x.FileLocation + "/" + x.FileName, FileName = x.FileName }).ToList(),
                    Rating = result.Reviews.Any() ? result.Reviews.Average(r => r.Rating) : 0.0,
                    RatingCount = result.Reviews.Count()
            };

                return finalObject;
            }
            else
            {
                return null;
            }

        }

        public async Task<GameCardListResponse> GetGameBoardInfo(int startIndex, int endIndex, string? searchTerm, FilterDTO filter)
        {
            var query = _dbContext.BoardGames
                .Where(x => x.TableBoardStateId == ModelLayer.Enum.TableBoardState.Activated)
                .OrderBy(x => x.Title)
                .Include(x => x.Categories)
                .Include(x => x.BoardTypes)
                .Include(x => x.Reviews)
                .Where(x => filter.Types == null || !filter.Types.Any() || x.BoardTypes.Any(bt => filter.Types.Contains(bt.BoardTypeId)))
                .Where(x => filter.Categories == null || !filter.Categories.Any() || x.Categories.Any(bt => filter.Categories.Contains(bt.CategoryId)))
                .Where(x => !x.IsBlocked)
                .Select(x => new GameBoardCardItemDTO
                {
                    GameBoardId = x.BoardGameId,
                    Title = x.Title,
                    ReleaseDate = x.CreationTime,
                    ThumbnailName = x.Thumbnail_Location,
                    Rating = x.Reviews.Average(r => r.Rating) != null ? x.Reviews.Average(r => r.Rating) : 0.0
                });

            if (searchTerm != null)
            {
                query = query.Where(x => x.Title.StartsWith(searchTerm));
            }

            if(filter.Title != null)
            {
                query = query.Where(x => x.Title.StartsWith(filter.Title));
            }

            if(filter.CreationDate != null)
            {
                query = query.Where(x => x.ReleaseDate > filter.CreationDate);
            }

            if(filter.Rating != null)
            {
                string[] ratings = filter.Rating.Split('-');

                query = query.Where(x => x.Rating >= double.Parse(ratings[0]) && x.Rating <= double.Parse(ratings[1]));
            }


            var count = await query.CountAsync();

            if (count <= endIndex)
            {
                endIndex = count+1;
            }

            var result = await query
                .Skip(startIndex)
                .Take(endIndex - startIndex)
                .ToListAsync();

            return new GameCardListResponse
            {
                BoardGames = result,
                TotalCount = count
            };

        }

        public async Task<EditGameBoardInfo> GetGameBoardInfo(int gameBoardId)
        {
          var result = await _dbContext.BoardGames
                .Include(x => x.Categories)
                .Include(x => x.BoardTypes)
                .Where(x => x.BoardGameId == gameBoardId)
                .Select(x => new EditGameBoardInfo
                {
                    GameBoardId = x.BoardGameId,
                    Title = x.Title,
                    PlayableAge = x.PlayableAge,
                    Description = x.Description,
                    IsBlocked  = x.IsBlocked,
                    PlayerCount = x.PlayerCount,
                    PlayingTime = x.PlayingTime,
                    Rules = x.Rules,
                    SelectedCategories = x.Categories.Select(y => new CategoryDTO { Label = y.CategoryName, Value = y.CategoryId.ToString()}).ToList(),
                    SelectedTypes = x.BoardTypes.Select(y => new TypeDTO { Label = y.BoardTypeName, Value = y.BoardTypeId.ToString() }).ToList(),

                })
                .FirstOrDefaultAsync();

            if(result == null)
            {
                throw new Exception("Game board not found");
            }

            return result;
        }

        public async Task<GameBoardListForAdmin> GetGameBoardListForAdmin(int pageIndex, int pageSize)
        {
            var query = _dbContext.BoardGames
                .Include(x => x.User)
                .Include(x => x.TableBoardState)
                .Select(x => new GameBoardForAdmin
            {
                GameBoardId = x.BoardGameId,
                Title = x.Title,
                GameBoardCreateDate = x.CreationTime,
                CreatorName = x.User.UserName,
                CreatorId = x.UserId,
                State = x.TableBoardState.Name,
                IsBlocked = x.IsBlocked
            });

            var count = await query.CountAsync();
            var result = await query
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new GameBoardListForAdmin
            {
                Boards = result,
                TotalCount = count
            };
        }

        public async Task<GameBoardReviewResponse> GetGameBoardsForReview(GetGameBoardsForReviewRequestDTO request)
        {
            var query = _dbContext.BoardGames
                .Include(x => x.User)
                .Where(x => x.TableBoardStateId == ModelLayer.Enum.TableBoardState.Reviewing)
                .Select(x => new GameBoardReviewItem
                {
                    GameBoardId = x.BoardGameId,
                    GameBoardName = x.Title,
                    GameBoardCreateDate = x.CreationTime,
                    CreatorId = x.UserId,
                    CreatorName = x.User.UserName
                });

            var totalCount = await query.CountAsync();

            var result = await query
                .Skip(request.PageIndex*request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            return new GameBoardReviewResponse
            {
                GameBoardsForReview = result,
                TotalCount = totalCount,
            };
        }

        public async Task<UserCreatedTableTopGamesResponse> GetGameBoardUsers(string username, int pageIndex, int pageSize)
        {
            var query = _dbContext.BoardGames
                .Include(x => x.User)
                .Where(x => x.User.UserName == username && !x.IsBlocked && x.TableBoardStateId == TableBoardState.Activated)
                .Select(x => new UserCreatedGameBoardsItem
                {
                    GameBoardId = x.BoardGameId,
                    Title = x.Title,
                    ImageUrl = "Images/" + Regex.Replace(x.Title, @"[^\w\s]+", "").Replace(" ", "_") + "/" + x.Thumbnail_Location,
                });

            var totalCount = await query.CountAsync();

            var result = await query
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            return new UserCreatedTableTopGamesResponse
            {
                TotalCount = totalCount,
                GameBoards = result
            };
        }

        public async Task<bool> IsGameBoardExist(string gameBoardTitle)
        {
            var isExist = await _dbContext.BoardGames.AnyAsync(x => x.Title.Contains(gameBoardTitle) && (x.TableBoardStateId == TableBoardState.Activated || x.TableBoardStateId == TableBoardState.Reviewing));

            return isExist;
        }

        public async Task<bool> SetGameBoardState(GameBoardApprove approval)
        {
            var game = await _dbContext.BoardGames
                .Include(x => x.Categories)
                .Include(x => x.BoardTypes)
                .FirstOrDefaultAsync(x => x.BoardGameId == approval.GameBoardId);

            if (game == null)
            {
                return false;
            }

            if (approval.IsAproved)
            {
                game.TableBoardStateId = TableBoardState.Activated;

                foreach(var category in game.Categories)
                {
                    category.IsActive = true;
                    _dbContext.Categories.Update(category);
                    await _dbContext.SaveChangesAsync();
                }

                foreach (var type in game.BoardTypes)
                {
                    type.IsActive = true;
                    _dbContext.BoardTypes.Update(type);
                    await _dbContext.SaveChangesAsync();
                }
            }
            else
            {
                game.TableBoardStateId = TableBoardState.Rejected;
            }

            game.UpdateTime = DateTime.Now;
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task UpdateGameBoard(EditGameBoardInfo boardGameModel)
        {
            var gameBoard = await _dbContext.BoardGames
                .Include(x => x.Categories)
                .Include(x => x.BoardTypes)
                .FirstOrDefaultAsync(x => x.BoardGameId == boardGameModel.GameBoardId);

            gameBoard.Title = boardGameModel.Title;
            gameBoard.IsBlocked = boardGameModel.IsBlocked;
            gameBoard.Rules = boardGameModel.Rules;
            gameBoard.PlayableAge = boardGameModel.PlayableAge;
            gameBoard.PlayingTime = boardGameModel.PlayingTime;
            gameBoard.PlayerCount = boardGameModel.PlayerCount;
            gameBoard.Description = boardGameModel.Description;
            gameBoard.UpdateTime = DateTime.Now;

            string[] ids = boardGameModel.SelectedCategories.Select(c => c.Value).ToArray();
            gameBoard.Categories =  await _dbContext.Categories.Where(c => ids.Contains(c.CategoryId.ToString())).ToListAsync();

            string[] idsTypes = boardGameModel.SelectedTypes.Select(c => c.Value).ToArray();
            gameBoard.BoardTypes = await _dbContext.BoardTypes.Where(c => idsTypes.Contains(c.BoardTypeId.ToString())).ToListAsync();

            await _dbContext.SaveChangesAsync();
        }
    }
}
