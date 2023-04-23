using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DTO
{
    public class InvitationsListResponse
    {
        public List<InvitationItem> Invitations { get; set; }
        public int TotalCount { get; set; }
    }
}
