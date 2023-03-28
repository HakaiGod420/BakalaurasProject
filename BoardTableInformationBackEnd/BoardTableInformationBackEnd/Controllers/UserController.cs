using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTO;
using ServiceLayer.Interfaces;
using System.Security.Claims;

namespace BoardTableInformationBackEnd.Controllers
{
    [ApiController]
    [Route("/api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAddressService _addressService;

        public UserController(IUserService userService,IAddressService addressService)
        {
            _userService = userService;
            _addressService = addressService;
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
        [AllowAnonymous]
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
                return BadRequest("Username already exists");
            }

            return new CreatedResult(String.Empty,userView);
        }

        [HttpPost("login")]
        [AllowAnonymous]
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

        [HttpPost("addAddress")]
        [Authorize]
        [ProducesResponseType(typeof(bool), StatusCodes.Status201Created)]
        public async Task<IActionResult> AddUserAddress(AddressCreateDto address)
        {
            var result  = await _addressService.AddNewAddress(address);

            if(result == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }
           
            return new CreatedResult(String.Empty, result);
        }

        [HttpPatch("updateNotifications")]
        [Authorize]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateUserNotifications(
            [FromBody]NotificationsListDto notification)
        {
            var id = Convert.ToInt32(HttpContext.User.FindFirstValue("UserId"));

            var result = await _userService.UpdateNotifications(id, notification);

            if (result == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }

            return new OkObjectResult(result);
        }

        [HttpGet("getUserSettings")]
        [Authorize]
        [ProducesResponseType(typeof(UserSettings), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserSettings()
        {
            var id = Convert.ToInt32(HttpContext.User.FindFirstValue("UserId"));

            var result = await _userService.GetUserSettings(id);

            if (result == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }

            return new OkObjectResult(result);
        }

        [HttpPut("updateAddress")]
        [Authorize]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateUserAddress([FromBody] UpdateUserAddress address)
        {
            var id = Convert.ToInt32(HttpContext.User.FindFirstValue("UserId"));

            var result = await _addressService.UpdateUserAddress(id,address);

            if (result == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }
            return new OkObjectResult(result);
        }

        [HttpGet("getUserInformation")]
        [Authorize]
        [ProducesResponseType(typeof(UserInformationDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUserInformation(string userName)
        {
            var result = await _userService.GetUserInformation(userName);

            if (result == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }
            return new OkObjectResult(result);
        }
    }
}
