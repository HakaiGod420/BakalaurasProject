using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO
{
    public class CreateReviewDto
    {
        public int BoardGameId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
    }
}
