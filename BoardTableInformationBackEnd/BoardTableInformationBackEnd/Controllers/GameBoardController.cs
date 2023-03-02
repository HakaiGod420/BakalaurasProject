﻿using DataLayer.Repositories.GameBoard;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTO;
using ServiceLayer.Interfaces;
using ServiceLayer.Services;

namespace BoardTableInformationBackEnd.Controllers
{
    [ApiController]
    [Route("/api/gameboard")]
    public class GameBoardController : ControllerBase
    {
        private readonly IGameBoardService _gameBoardService;
        public GameBoardController(IGameBoardService gameBoardService)
        {
            _gameBoardService = gameBoardService;
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(CreateBoardGame), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CreateBoardGame boardGameModel)
        {
            /*
            if (registerModel.Password != registerModel.RePassword)
            {
                return BadRequest("Password must match");
            }

            var userView = await _userService.RegisterUser(registerModel);

            if (userView == null)
            {
                return NotFound();
            }
            */
            return new CreatedResult(String.Empty, "");
        }
    }
}
