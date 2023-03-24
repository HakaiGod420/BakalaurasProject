using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories.Invitation
{
    public interface IInvitationRepository
    {
        public Task<ActiveGameEntity> AddInvitation(ActiveGameEntity invitation);
        public Task<bool> SentInvitation(SentInvitationEntity invitation);
    }
}
