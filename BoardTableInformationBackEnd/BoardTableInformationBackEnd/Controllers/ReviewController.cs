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
        [ProducesResponseType(typeof(string), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> CreateReview([FromBody] CreateReviewDto createReviewDto)
        {
            var id = Convert.ToInt32(HttpContext.User.FindFirstValue("UserId"));

            if(createReviewDto.Rating < 0)
            {
                return UnprocessableEntity(nameof(createReviewDto.Rating));
            }

            await _reviewService.CreateReview(createReviewDto, id);

            return Ok();
        }

        [HttpGet("reviews")]
        [ProducesResponseType(typeof(List<ReviewView>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> GetReviews([FromQuery] int boardGameId)
        {
            if(boardGameId < 0)
            {
                return UnprocessableEntity(nameof(boardGameId));
            }
            var reviews = await _reviewService.GetReviews(boardGameId);

            return new OkObjectResult(reviews);
        }

        [HttpGet("oldReview")]
        [Authorize]
        [ProducesResponseType(typeof(OldReview), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> GetOldReview([FromQuery] int boardGameId)
        {
            var userId = Convert.ToInt32(HttpContext.User.FindFirstValue("UserId"));

            if (boardGameId < 0)
            {
                return UnprocessableEntity(nameof(boardGameId));
            }

            var oldReview = await _reviewService.GetOldReview(userId, boardGameId);

            return new OkObjectResult(oldReview);
        }
    }
}
