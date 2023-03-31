using DataLayer.Models;
using ModelLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories.GameBoard
{
    public interface IGameBoardRepository
    {
        public Task<BoardGameEntity> AddGameBoard(BoardGameEntity tableBoard);
        public Task<List<BoardGameSimpleDto>> GetBoardsSimple(string titlePart);
        public Task<List<GameBoardCardItemDTO>> GetGameBoardInfo(int startIndex, int endIndex);
    }
}
