using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO
{
    public class GameBoardListForAdmin
    {
        public List<GameBoardForAdmin> Boards { get; set; }
        public int TotalCount { get; set; }
    }
}
