using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTO;
using ServiceLayer.Interfaces;

namespace BoardTableInformationBackEnd.Controllers
{
    [ApiController]
    [Route("/api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCustomerData(int id)
        {
            var userView = await _userService.GetUser(id);

            if (userView == null)
            {
                return NotFound();
            }

            return new OkObjectResult(userView);
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(UserRegisterModel), StatusCodes.Status201Created)]
        public async Task<IActionResult> RegisterUser(UserRegisterModel registerModel)
        {

            if(registerModel.Password != registerModel.RePassword)
            {
                return BadRequest("Password must match");
            }

            var userView = await _userService.RegisterUser(registerModel);

            if (userView == null)
            {
                return NotFound();
            }

            return new CreatedResult(String.Empty,userView);
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginUserModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> LoginUser(LoginUserModel loginModel)
        {
            var result = await _userService.Login(loginModel);

            if (result == null)
            {
                return BadRequest();
            }

            return new OkObjectResult(result);
        }
    }
}
