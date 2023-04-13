using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO
{
    public class GetGameBoardsForReviewRequestDTO
    {
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }
    }
}
