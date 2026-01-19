using Admin.Api.Controllers.Base;
using Common.Logging;
using DTOs.Models.User;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces.Identity;

namespace Admin.Api.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : AppBaseApiController
    {
        private readonly IUserAuthenticationService _userAuthentication;
        public AuthenticateController(IUserAuthenticationService userAuthentication)
        {
            _userAuthentication = userAuthentication;
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            try
            {
                return Ok(await _userAuthentication.LoginUserAsync(model));
            }
            catch (Exception ex)
            {
                AppLogging.LogException(ex.Message); return BadRequest(ErrorMessage(ex.Message));
            }
        }
    }
}

