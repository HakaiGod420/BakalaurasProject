using DataLayer.Repositories.GameBoard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTO;
using ServiceLayer.Interfaces;
using ServiceLayer.Services;
using System.Security.Claims;

namespace BoardTableInformationBackEnd.Controllers
{
    [ApiController]
    [Route("/api/gameboard")]
    public class GameBoardController : ControllerBase
    {
        private readonly IBoardGameService _gameBoardService;
        public GameBoardController(IBoardGameService gameBoardService)
        {
            _gameBoardService = gameBoardService;
        }

        [Authorize]
        [HttpPost("create")]
        [ProducesResponseType(typeof(CreatedGameBoard), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromForm]CreateBoardGame boardGameModel)
        {
            var id = Convert.ToInt32(HttpContext.User.FindFirstValue("UserId"));

            var createdGameBoard = await _gameBoardService.CreateGameBoard(boardGameModel,id);

            return new CreatedResult(String.Empty, createdGameBoard);
        }

        [HttpGet("getBoardsSimple")]
        [ProducesResponseType(typeof(List<BoardGameSimpleDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBoards(string searchTerm)
        {
          

            var listOfBoards = await _gameBoardService.GetBoardGamesForSelect(searchTerm);

            return new OkObjectResult(listOfBoards);
        }

    }
}
