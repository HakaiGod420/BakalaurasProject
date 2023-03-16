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

        public Task<ActiveGameEntity> AddInvitation(ActiveGameEntity invitation)
        {
            throw new NotImplementedException();
        }
    }
}
