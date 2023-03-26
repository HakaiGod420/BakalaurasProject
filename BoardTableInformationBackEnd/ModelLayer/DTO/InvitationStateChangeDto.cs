using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO
{
    public class InvitationStateChangeDto
    {
        public int UserId { get; set; } 
        public string State { get; set; }
        public int InvitationId { get; set; }
    }
}
