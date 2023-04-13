using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO
{
    public class GameBoardReviewResponse
    {
        public List<GameBoardReviewItem> GameBoardsForReview { get; set; }
        public int TotalCount { get; set; }
    }
}
