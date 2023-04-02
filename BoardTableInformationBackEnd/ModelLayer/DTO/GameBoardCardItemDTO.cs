using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO
{
    public class GameBoardCardItemDTO
    {
        public int GameBoardId { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string ThumbnailURL { get; set; }
        public string ThumbnailName { get; set; }
    }
}
