using DataLayer.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories.BoardType
{
    public class BoardTypeRepository : IBoardTypeRepository
    {
        private readonly DataBaseContext _dbContext;

        public BoardTypeRepository(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CheckIfExists(string typeBoardName)
        {
            return await _dbContext.BoardTypes.AnyAsync(x => x.BoardTypeName == typeBoardName);
        }
    }
}
