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
        [ProducesResponseType(typeof(List<GameBoardCardItemDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBoardCardItems([FromQuery]int startIndex, [FromQuery] int backIndex)
        {
            var listOfBoards = await _gameBoardService.GetBoardCardItems(startIndex, backIndex);

            foreach (var item in listOfBoards)
            {
                var folderName = Regex.Replace(item.Title, @"[^a-zA-Z0-9 ]+", "_");
                folderName = Regex.Replace(folderName, @"\s+", "_");

                var fileFullLocation = Directory.GetCurrentDirectory() + "\\Files\\Images\\" + folderName + "\\"+item.ThumbnailLocation;
                if (System.IO.File.Exists(fileFullLocation))
                {
                    string fileName = Path.GetFileName(fileFullLocation);

                    string fileExtension = Path.GetExtension(fileFullLocation).ToLower();
                    string contentType = "";

                    switch (fileExtension)
                    {
                        case ".jpg":
                        case ".jpeg":
                            contentType = "image/jpeg";
                            break;
                        case ".png":
                            contentType = "image/png";
                            break;
                        default:
                            contentType = "application/octet-stream";
                            break;
                    }

                    FileStream fileStream = new FileStream(fileFullLocation, FileMode.Open);

                    item.Thumbnail = new FormFile(fileStream, 0, fileStream.Length, fileName, fileName)
                    {
                        Headers = new HeaderDictionary(),
                        ContentType = contentType
                    };
                    fileStream.Close();
                }

            }

            return new OkObjectResult(listOfBoards);
        }

    }
}
