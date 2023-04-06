using DataLayer.Models;
using ModelLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories.BoardType
{
    public interface IBoardTypeRepository
    {
        public Task<BoardTypeEntity?> GetType(string typeBoardName);
        public Task<BoardTypeEntity> CreateType(BoardTypeEntity boardType);
        public Task<List<TypeDTO>> GetTypes();
    }
}
