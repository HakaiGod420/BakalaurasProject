using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class ReviewEntity
    {
        [Key]
        public int ReviewId { get; set; }
        public int WriterId { get; set; }
        public virtual UserEntity Writer { get; set; }
        public DateTime WriteDate { get; set; }
        public bool IsBlocked { get; set; } = false;
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public int SelectedBoardGameId { get; set; }
        public virtual BoardGameEntity SelectedBoardGame { get; set; }

    }
}
