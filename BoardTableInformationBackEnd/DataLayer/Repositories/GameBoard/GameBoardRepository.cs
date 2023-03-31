using DataLayer.DBContext;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using ModelLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<List<GameBoardCardItemDTO>> GetGameBoardInfo(int startIndex, int endIndex)
        {
            var query = _dbContext.BoardGames
                .Where(x => x.TableBoardStateId == ModelLayer.Enum.TableBoardState.Activated)
                .OrderBy(x => x.Title)
                .Select(x => new GameBoardCardItemDTO
                {
                    GameBoardId = x.BoardGameId,
                    Title = x.Title,
                    ReleaseDate = x.CreationTime,
                    ThumbnailLocation = x.Thubnail_Location,

                });

            var count = await query.CountAsync();
            if (count <= endIndex)
            {
                endIndex = count+1;
            }

            var result = await query
                .Skip(startIndex)
                .Take(endIndex - startIndex)
                .ToListAsync();

            return result;

        }
    }
}
