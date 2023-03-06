using DataLayer.DBContext;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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

        public async Task<BoardTypeEntity> CreateType(BoardTypeEntity boardType)
        {
            _dbContext.BoardTypes.Add(boardType);
            await _dbContext.SaveChangesAsync();
            return boardType;
        }

        public async Task<BoardTypeEntity?> GetType(string typeBoardName)
        {
            return await _dbContext.BoardTypes.FirstOrDefaultAsync(x => x.BoardTypeName == typeBoardName);
        }
    }
}
