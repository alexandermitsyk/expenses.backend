using expenses.core;
using expenses.core.CustomException;
using expenses.db;
using Microsoft.AspNetCore.Mvc;

namespace expenses.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthenticationController(IUserService userService)
        {
            _userService = userService; 
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(User user)
        {
            try
            {
                var result = await _userService.SignUp(user);

                return Created("", result);
            }
            catch (UsernameAlreadyExistsException e)
            {

                return StatusCode(409, e.Message);
            }
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(User user)
        {
            try
            {
                var result = await _userService.SignIn(user);

                return Ok(result);
            }
            catch (InvalidUsernamePaswwordException e)
            {

                return StatusCode(401, e.Message);
            }
        }
    }
}
