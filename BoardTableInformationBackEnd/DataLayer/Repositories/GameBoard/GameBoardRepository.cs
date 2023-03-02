using DataLayer.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories.GameBoard
{
    public class GameBoardRepository : IGameBoardService
    {
        private readonly DataBaseContext _dbContext;

        public GameBoardRepository(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
