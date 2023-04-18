using DataLayer.Repositories.GameBoard;
using ModelLayer.DTO;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class AdminService : IAdminService
    {
        public readonly IGameBoardRepository _gameBoardRepository;
        
        public AdminService(IGameBoardRepository gameBoardRepository)
        {
            _gameBoardRepository = gameBoardRepository;
        }

        public async Task<bool> ChangeGameBoardState(GameBoardApprove aproval)
        {
            return await _gameBoardRepository.SetGameBoardState(aproval);
        }

        public async Task<GameBoardReviewResponse> GetBoardForReview(GetGameBoardsForReviewRequestDTO request)
        {
            return await _gameBoardRepository.GetGameBoardsForReview(request);
        }

        public async Task<GameBoardListForAdmin> GetGameBoardsForAdmin(int pageSize, int pageIndex)
        {
            return await _gameBoardRepository.GetGameBoardListForAdmin(pageSize, pageIndex);
        }

        public Task<bool> UpdateIsBlocked(int gameBoardId, bool isBlocked)
        {
            return _gameBoardRepository.ChangeGameBoardState(gameBoardId, isBlocked);
        }
    }
}
