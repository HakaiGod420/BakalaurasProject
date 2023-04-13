using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO
{
    public class GameBoardReviewItem
    {
        public int GameBoardId { get; set; }
        public string GameBoardName { get; set; }
        public DateTime GameBoardCreateDate { get; set; }
        public int CreatorId { get; set; }
        public string CreatorName { get; set; }
        public int CurrentStateId { get; set; }
    }
}
