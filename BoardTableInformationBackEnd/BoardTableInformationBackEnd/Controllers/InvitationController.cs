using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTO;
using ServiceLayer.Interfaces;
using System.Security.Claims;

namespace BoardTableInformationBackEnd.Controllers
{

    [ApiController]
    [Route("/api/gameboardinvitation")]
    public class InvitationController : ControllerBase
    {
        private readonly IInvitationService _invitationService;

        public InvitationController(IInvitationService invitationService)
        {
            _invitationService = invitationService;
        }

        [Authorize]
        [HttpPost("createInvitation")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateInvitation([FromBody] PostInvatationDto data)
        {
            var id = Convert.ToInt32(HttpContext.User.FindFirstValue("UserId"));

            var invitation = await _invitationService.PostInvatation(data, id);

            return new CreatedResult(String.Empty, invitation);
        }

        [HttpGet("invitations")]
        [Authorize]
        [ProducesResponseType(typeof(List<UserInvitationDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetInvitations()
        {
            var id = Convert.ToInt32(HttpContext.User.FindFirstValue("UserId"));

            var invitations = await _invitationService.GetInvitations(id);

            return Ok(invitations);
        }

        [HttpGet("activeInvitations")]
        [Authorize]
        [ProducesResponseType(typeof(List<UserInvitationDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetActiveInvitations()
        {
            var id = Convert.ToInt32(HttpContext.User.FindFirstValue("UserId"));

            var invitations = await _invitationService.GetActiveInvitations(id);

            return Ok(invitations);
        }

        [HttpGet("createdInvitations")]
        [Authorize]
        [ProducesResponseType(typeof(List<UserInvitationDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCreatedInvitations()
        {
            var id = Convert.ToInt32(HttpContext.User.FindFirstValue("UserId"));

            var invitations = await _invitationService.GetCreatedInvitations(id);

            return Ok(invitations);
        }

        [HttpPatch("updateInvitationState")]
        [Authorize]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateInvitationState([FromBody] InvitationStateChangeDto data)
        {
            var id = Convert.ToInt32(HttpContext.User.FindFirstValue("UserId"));
            data.UserId = id;
            await _invitationService.ChangeInvitationState(data);
            return Ok();
        }

        [HttpGet("activeInvitationCount")]
        [Authorize]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetActiveInvitationCount()
        {
            var id = Convert.ToInt32(HttpContext.User.FindFirstValue("UserId"));
            var count = await _invitationService.ActiveInvitationCount(id);
            return Ok(count);
        }

        [HttpPost("sentInvitationToUser")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> SentInvitationToUser([FromBody] SingeUserSentInvitationDTO invitation)
        {
            await _invitationService.SentInvitationToUser(invitation);

            return new CreatedResult(String.Empty, invitation);
        }
    }
}
