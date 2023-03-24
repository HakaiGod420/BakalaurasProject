using DataLayer.DBContext;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories.Invitation
{
    public class InvitationRepository : IInvitationRepository
    {
        private readonly DataBaseContext _dbContext;
        public InvitationRepository(DataBaseContext dataBaseContext) 
        {
            _dbContext = dataBaseContext;
        }

        public async Task<ActiveGameEntity> AddInvitation(ActiveGameEntity invitation)
        {
            await _dbContext.ActiveGames.AddAsync(invitation);
            await _dbContext.SaveChangesAsync();
            return invitation;
        }

        public async Task<bool> SentInvitation(SentInvitationEntity invitation)
        {
            _dbContext.SentInvitations.Add(invitation);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
