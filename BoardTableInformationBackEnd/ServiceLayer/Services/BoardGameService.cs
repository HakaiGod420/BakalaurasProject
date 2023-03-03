using DataLayer.Repositories.GameBoard;
using DataLayer.Repositories.User;
using Microsoft.Extensions.Configuration;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class BoardGameService : IBoardGameService
    {
        private readonly IGameBoardRepository _gameBoardRepository;

        public BoardGameService(IGameBoardRepository gameBoardRepository)
        {
            _gameBoardRepository = gameBoardRepository;
        }
    }
}
