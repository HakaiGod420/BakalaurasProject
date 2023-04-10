using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO
{
    public class SingleGameBoardView
    {
        public int BoardGameId { get; set; }
        public string Title { get; set; }
        public int PlayerCount { get; set; }
        public int PlayableAge { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? Rules { get; set; }
        public string? Thumbnail_Location { get; set; }
        public string CreatorName { get; set; }
        public int CreatorId { get; set; }
        public List<string> Categories { get; set; }
        public List<string> Types { get; set; }
        public List<GetImageDTO> Images { get; set; }
        public List<GetAditionalFilesDTO> Files { get; set; }
        public double? Rating { get; set; }
    }
}
