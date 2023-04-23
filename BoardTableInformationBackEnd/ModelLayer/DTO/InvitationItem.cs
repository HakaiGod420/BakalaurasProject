using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO
{
    public class InvitationItem
    {
        public int InvitationId { get; set; }
        public int BoardGameId { get; set; }
        public string BoardGameTitle { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public int MaxPlayer { get; set; }
        public int AcceptedPlayer { get; set; }
        public string ImageUrl { get; set; }
    }
}
