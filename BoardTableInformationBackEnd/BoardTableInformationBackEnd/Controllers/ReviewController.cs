using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using ModelLayer.DTO;
using ServiceLayer.Interfaces;
using System.Security.Claims;

namespace BoardTableInformationBackEnd.Controllers
{
    [ApiController]
    [Route("/api/review")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost("review")]
        [Authorize]
        [ProducesResponseType(typeof(void), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateReview([FromBody] CreateReviewDto createReviewDto)
        {
            var id = Convert.ToInt32(HttpContext.User.FindFirstValue("UserId"));

            if(createReviewDto.Rating < 0)
            {
                return BadRequest("Wrong rating");
            }

            await _reviewService.CreateReview(createReviewDto, id);
            return Ok();
        }

        [HttpGet("reviews")]
        [ProducesResponseType(typeof(List<ReviewView>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetReviews([FromQuery] int boardGameId)
        {
            var reviews = await _reviewService.GetReviews(boardGameId);

            return new OkObjectResult(reviews);
        }
    }
}
