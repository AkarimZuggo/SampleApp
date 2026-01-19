using Admin.Api.Controllers.Base;
using Common.Logging;
using DTOs.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces.Identity;

namespace Admin.Api.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : AppBaseApiController
    {
        private readonly IUserAuthenticationService _userAuthentication;
        public AccountController(IUserAuthenticationService userAuthentication)
        {
            _userAuthentication = userAuthentication;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            try
            {
                return Ok(await _userAuthentication.RegisterUserAsync(model));
            }
            catch (Exception ex)
            {
                AppLogging.LogException(ex.Message); return BadRequest(ErrorMessage(ex.Message));
            }
        }
    }
}
