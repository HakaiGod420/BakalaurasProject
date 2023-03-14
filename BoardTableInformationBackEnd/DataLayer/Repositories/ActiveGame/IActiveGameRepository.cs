using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories.ActiveGame
{
    public interface IActiveGameRepository
    {
        public Task<bool> PostActiveGame(ActiveGameEntity game);
    }
}
