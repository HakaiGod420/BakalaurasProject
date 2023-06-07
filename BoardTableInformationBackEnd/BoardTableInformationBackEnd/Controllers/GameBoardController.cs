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
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateGameBoard([FromForm]CreateBoardGame boardGameModel)
        {
            var id = Convert.ToInt32(HttpContext.User.FindFirstValue("UserId"));

            if(boardGameModel == null)
            {
                return BadRequest("Wrong gameBoardModal");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not correct");
            }

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
        [ProducesResponseType(typeof(string), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> GetBoardCardItems([FromQuery]int startIndex, [FromQuery] int backIndex, [FromQuery] FilterDTO filter, [FromQuery] string? searchTerm = null)
        {

            if(startIndex < 0 )
            {
                return UnprocessableEntity(nameof(startIndex));
            }

            if (backIndex < 0)
            {
                return UnprocessableEntity(nameof(backIndex));
            }

            var listOfBoards = await _gameBoardService.GetBoardCardItems(startIndex, backIndex,searchTerm, filter);

            foreach (var item in listOfBoards.BoardGames)
            {
                var folderName = Regex.Replace(item.Title, @"[^\w\s]+", "").Replace(" ", "_");

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

        [Authorize]
        [HttpGet("gameBoardExist")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GameBoardExist([FromQuery]string gameBoardName)
        {
            var board = await _gameBoardService.IsGameBoardExist(gameBoardName);

            return new OkObjectResult(board);
        }

        [Authorize]
        [HttpGet("getUserCreatedGameBoards")]
        [ProducesResponseType(typeof(UserCreatedTableTopGamesResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserCreatedGameBoards([FromQuery] string username,[FromQuery]int pageIndex, [FromQuery]int pageSize)
        {
            var board = await _gameBoardService.GetGameBoardsCreatedByUser(username, pageIndex, pageSize);

            return new OkObjectResult(board);
        }

        [Authorize(Roles ="Admin")]
        [HttpPut("updateGameBoard")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateGameBoard([FromBody] EditGameBoardInfo boardGameModel)
        {
             await _gameBoardService.UpdateGameBoard(boardGameModel);

            return new OkObjectResult(true);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("updateGameBoard")]
        [ProducesResponseType(typeof(GetUpdateGameInfo), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUpdateGameInfo([FromQuery] int gameBoardId)
        {
            if(gameBoardId == 0 || gameBoardId == null)
            {
                return BadRequest("Wrong game board id");
            }

            var result = await _gameBoardService.GetUpdateGameInfo(gameBoardId);

            return new OkObjectResult(result);
        }
    }
}
