using DataLayer.Repositories.GameBoard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTO;
using ServiceLayer.Interfaces;
using ServiceLayer.Services;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

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

        [HttpGet("getBoardCardItems")]
        [ProducesResponseType(typeof(GameCardListResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBoardCardItems([FromQuery]int startIndex, [FromQuery] int backIndex, [FromQuery] string? searchTerm = null)
        {
            var listOfBoards = await _gameBoardService.GetBoardCardItems(startIndex, backIndex,searchTerm);

            foreach (var item in listOfBoards.BoardGames)
            {
                var folderName = Regex.Replace(item.Title, @"[^a-zA-Z0-9 ]+", "_");
                folderName = Regex.Replace(folderName, @"\s+", "_");

                var fileFullLocation = Directory.GetCurrentDirectory() + "\\Files\\Images\\" + folderName + "\\"+item.ThumbnailName;

                if (System.IO.File.Exists(fileFullLocation))
                {
                    item.ThumbnailURL = "/Images/"+folderName+"/" + item.ThumbnailName;
                }

            }

            return new OkObjectResult(listOfBoards);
        }

        [HttpGet("getSingleBoard")]
        [ProducesResponseType(typeof(SingleGameBoardView), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSingleBoard([FromQuery]int boardId)
        {
            var board = await _gameBoardService.GetGameBoard(boardId);
            if (board == null)
            {
               return NotFound("Game board was not found");
            }

            return new OkObjectResult(board);
        }

    }
}
