﻿using ModelLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface IBoardGameService
    {
        public Task<CreatedGameBoard> CreateGameBoard(CreateBoardGame board,int userId);
        public Task<List<BoardGameSimpleDto>> GetBoardGamesForSelect(string stringPart);
        public Task<GameCardListResponse> GetBoardCardItems(int startIndex, int backIndex);
        public Task<SingleGameBoardView?> GetGameBoard(int boardId);
    }
}
