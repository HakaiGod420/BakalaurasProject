using DataLayer.DBContext;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories.ActiveGame
{
    public class ActiveGameRepository : IActiveGameRepository
    {
        private readonly DataBaseContext _dbContext;

        public ActiveGameRepository(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> PostActiveGame(ActiveGameEntity game)
        {
            var v = _dbContext.ActiveGames.Add(game);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
