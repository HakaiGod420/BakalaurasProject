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
    }
}
