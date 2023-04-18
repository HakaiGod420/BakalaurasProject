using ModelLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface IAdminService
    {
        public Task<GameBoardReviewResponse> GetBoardForReview(GetGameBoardsForReviewRequestDTO request);
        public Task<bool> ChangeGameBoardState(GameBoardApprove aproval);
        public Task<GameBoardListForAdmin> GetGameBoardsForAdmin(int pageSize,int pageIndex);
        public Task<bool> UpdateIsBlocked(int gameBoardId, bool isBlocked);
    }
}
