using ModelLayer.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class BoardGameEntity
    {
        public BoardGameEntity()
        {
            Images = new HashSet<ImageEntity>();
            Categories = new HashSet<CategoryEntity>();
            BoardTypes = new HashSet<BoardTypeEntity>();
            AditionalFiles = new HashSet<AditionalFileEntity>();
            ActiveGames = new HashSet<ActiveGameEntity>();
            Reviews = new HashSet<ReviewEntity>();
        }
        [Key]
        public int BoardGameId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        public int PlayerCount { get; set; }

        public int? PlayingTime { get; set; }

        [Required]
        public int PlayableAge { get; set; }

        [Required]
        [MaxLength(3000)]
        public string Description { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime? UpdateTime { get; set; }

        [MaxLength(2000)]
        public string? Rules { get; set; }

        public string Thubnail_Location { get; set; }

        [MaxLength(10)]
        public virtual ICollection<ImageEntity> Images { get; set; }
        public virtual ICollection<CategoryEntity> Categories { get; set; }
        public virtual ICollection<BoardTypeEntity> BoardTypes { get; set; }

        [MaxLength(5)]
        public virtual ICollection<AditionalFileEntity> AditionalFiles { get; set; }
        public int UserId { get; set; }
        public virtual UserEntity User { get; set; }
        public TableBoardState TableBoardStateId { get; set; }
        public virtual TableBoardStateEntity TableBoardState { get; set; }
        public virtual ICollection<ActiveGameEntity> ActiveGames { get; set; }
        public virtual ICollection<ReviewEntity> Reviews { get; set; }
    }
}
