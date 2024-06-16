using Microsoft.AspNetCore.Mvc;

using POS_Application.Server.Models;
using POS_Application.Server.Services.Interfaces;


namespace POS_Application.Server.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// Authenticates a user and returns a login response with a token.
        /// </summary>
        /// <param name="loginRequest">The request containing the user's login credentials.</param>
        /// <returns>A login response with a token if successful, or an unauthorized status if credentials are invalid.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest loginRequest)
        {
            var loginResponse = await _authenticationService.LoginAsync(loginRequest);
            if (loginResponse != null)
            {
                return Ok(loginResponse);
            }
            else
            {
                return Unauthorized("Invalid credentials.");
            }
        }

        /// <summary>
        /// Logs out a user by invalidating their token.
        /// </summary>
        /// <returns>A status message indicating success or failure.</returns>
        [HttpPost("logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            string token = Request.Headers.Authorization;
            if (!await _authenticationService.JwtCheck(token))
            {
                return BadRequest("Token missing or invalid in request headers.");
            }
            await _authenticationService.LogoutAsync(token);
            return Ok();
        }

        /// <summary>
        /// Checks the validity of a user's session token.
        /// </summary>
        /// <returns>Session information if the token is valid, or a bad request status if the token is invalid or missing.</returns>
        [HttpGet("session")]
        public async Task<IActionResult> CheckSessionAsync()
        {
            string token = Request.Headers.Authorization;
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Token missing in request headers.");
            }

            var sessionInfo = await _authenticationService.CheckSessionAsync(token);
            if (sessionInfo == null)
            {
                return BadRequest("Invalid token.");
            }

            return Ok(sessionInfo);
        }
    }
}
