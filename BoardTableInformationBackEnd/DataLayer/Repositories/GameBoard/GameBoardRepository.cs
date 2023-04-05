using DataLayer.DBContext;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using ModelLayer.DTO;
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

        public async Task<List<BoardGameSimpleDto>> GetBoardsSimple(string titlePart)
        {
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
                .Include(a=>a.AditionalFiles)
                .SingleOrDefaultAsync(x=> x.BoardGameId == id);

            if (result != null)
            {
                var finalObject = new SingleGameBoardView
                {
                    BoardGameId = result.BoardGameId,
                    Title = result.Title,
                    PlayerCount = result.PlayerCount,
                    PlayableAge = result.PlayableAge,
                    Description = result.Description,
                    CreationTime = result.CreationTime,
                    UpdateDate = result.UpdateTime,
                    Rules = result.Rules,
                    Thumbnail_Location = "Images/" + Regex.Replace(result.Title, @"[^a-zA-Z0-9\s]+|\s+", "_") + "/" + result.Thubnail_Location,
                    CreatorId = result.UserId,
                    CreatorName = result.User.UserName,
                    Categories = result.Categories.Select(x => x.CategoryName).ToList(),
                    Types = result.BoardTypes.Select(x => x.BoardTypeName).ToList(),
                    Images = result.Images.Select(x => new GetImageDTO { Location = x.Location + "/" + x.Alias, FileName = x.Alias }).ToList(),
                    Files = result.AditionalFiles.Select(x => new GetAditionalFilesDTO { Location = x.FileLocation + "/" + x.FileName, FileName = x.FileName }).ToList(),
                };

                return finalObject;
            }
            else
            {
                return null;
            }

        }

        public async Task<GameCardListResponse> GetGameBoardInfo(int startIndex, int endIndex, string? searchTerm)
        {
            var query = _dbContext.BoardGames
                .Where(x => x.TableBoardStateId == ModelLayer.Enum.TableBoardState.Activated)
                .OrderBy(x => x.Title)
                .Select(x => new GameBoardCardItemDTO
                {
                    GameBoardId = x.BoardGameId,
                    Title = x.Title,
                    ReleaseDate = x.CreationTime,
                    ThumbnailName = x.Thubnail_Location,

                });

            if (searchTerm != null)
            {
                query = query.Where(x => x.Title.StartsWith(searchTerm));
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
    }
}
