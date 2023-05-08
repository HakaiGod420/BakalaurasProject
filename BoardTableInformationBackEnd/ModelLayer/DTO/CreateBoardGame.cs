using ModelLayer.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO
{
    public class CreateBoardGame
    {

        [Required]  
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        public int PlayerCount { get; set; }

        public int? PlayingTime { get; set; }

        [Required]
        public int PlayableAge { get; set; }

        [Required]
        [MaxLength(5000)]
        public string Description { get; set; }

        [MaxLength(2000)]
        public string? Rules { get; set; }

        public string ThumbnailName { get; set; }

        [MaxLength(10)]
        public List<CreateImage> Images { get; set; }
        public List<CreateCategory> Categories { get; set; }
        public List<CreateBoardType> BoardTypes { get; set; }

        [MaxLength(5)]
        public List<CreateAditionalFiles>? AditionalFiles { get; set; }

        public bool SaveAsDraft { get; set; } = false;
    }
}
