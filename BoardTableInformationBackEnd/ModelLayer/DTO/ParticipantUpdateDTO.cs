using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO
{
    public class ParticipantUpdateDTO
    {
        public int UserId { get; set; } 
        public int ActiveGameId { get; set; }
        public bool IsBlocked { get; set; }
    }
}
