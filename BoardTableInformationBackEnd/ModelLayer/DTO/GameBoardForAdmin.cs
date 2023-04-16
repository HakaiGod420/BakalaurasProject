using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO
{
    public class GameBoardForAdmin
    {
        public int GameBoardId { get; set; }
        public string Title { get; set; }
        public DateTime GameBoardCreateDate { get; set; }
        public string State { get; set; }
        public string CreatorName { get; set; }
        public int CreatorId { get; set; }
        public bool IsBlocked { get; set; }

    }
}
