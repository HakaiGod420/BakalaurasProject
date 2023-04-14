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
        public Task <GameCardListResponse> GetGameBoardInfo(int startIndex, int endIndex,string? searchTerm, FilterDTO filter);
        public Task<SingleGameBoardView?> GetGameBoard(int id);
        public Task<GameBoardReviewResponse> GetGameBoardsForReview(GetGameBoardsForReviewRequestDTO request);
        public Task<bool> SetGameBoardState(GameBoardAprove aproval);

    }
}
