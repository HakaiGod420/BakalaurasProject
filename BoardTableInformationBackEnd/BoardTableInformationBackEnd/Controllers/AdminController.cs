using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTO;
using ServiceLayer.Interfaces;

namespace BoardTableInformationBackEnd.Controllers
{
    [ApiController]
    [Route("/api/admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("getBoardForReview")]
        [ProducesResponseType(typeof(GameBoardReviewResponse), StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetBoardForReview([FromQuery]GetGameBoardsForReviewRequestDTO request)
        {
            var response = await _adminService.GetBoardForReview(request);

            return Ok(response);
        }

        [HttpPatch("changeGameBoardState")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeGameBoardState([FromBody]GameBoardApprove aproval)
        {
            var response = await _adminService.ChangeGameBoardState(aproval);

            return Ok(response);
        }

        [HttpGet("getGameBoardsForAdmin")]
        [ProducesResponseType(typeof(GameBoardListForAdmin), StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetGameBoardsForAdmin([FromQuery]int pageSize, [FromQuery]int pageIndex)
        {
            var response = await _adminService.GetGameBoardsForAdmin(pageIndex, pageSize);
                
            return Ok(response);
        }

        [HttpPatch("updateIsBlocked")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateIsBlocked([FromBody] UpdateIsBlockedDTO request)
        {
            var response = await _adminService.UpdateIsBlocked(request.GameBoardId, request.IsBlocked);

            return Ok(response);
        }

    }
}
