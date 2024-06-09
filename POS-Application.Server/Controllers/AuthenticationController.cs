using Microsoft.AspNetCore.Mvc;

using POS_Application.Server.Models;
using POS_Application.Server.Services.Interfaces;


namespace POS_Application.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public async Task<IActionResult> VerifyUserAsync(Authentication authentication)
        {
            if (await _authenticationService.VerifyUserAsync(authentication))
            {
                return Ok("User authenticated successfully.");
            }
            else
            {
                return Unauthorized("Invalid credentials.");
            }
        }
    }
}
