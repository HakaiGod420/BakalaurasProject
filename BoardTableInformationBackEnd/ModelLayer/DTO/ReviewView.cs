using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO
{
    public class ReviewView
    {
        public int ReviewId { get; set; }
        public string? ProfileImage { get; set; }
        public string Username { get; set; }
        public int CreatorId { get; set; }
        public string? ReviewText { get; set; }
        public int Rating { get; set; }
        public DateTime Written { get; set; }
    }
}
