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

    }
}
