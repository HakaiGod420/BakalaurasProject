using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO
{
    public class UserInvitationDto
    {
        public int InvitationId { get; set; }
        public int ActiveGameId { get; set; }
        public string BoardGameTitle { get; set; }
        public int BoardGameId { get; set; }
        public DateTime EventDate { get; set; }
        public string EventFullLocation { get; set; }
        public int MaxPlayerCount { get; set; }
        public int AcceptedCount { get; set; }
        public double Map_X_Cords { get; set; }
        public double Map_Y_Cords { get; set; }
    }
}
