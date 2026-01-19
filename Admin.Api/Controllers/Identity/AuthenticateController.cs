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
            _userAuthentication=userAuthentication;
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
            //try
            //{
            //    var companyId = 0;
            //    var user = await _userManager.FindByEmailAsync(model.Email);
            //    if (user != null)
            //    {
            //        if (await _userManager.CheckPasswordAsync(user, model.Password))
            //        {
            //            companyId = int.Parse(await _companyService.GetCompanyId(user.Id));
            //            var userRoles = await _userManager.GetRolesAsync(user);

            //            var authClaims = new List<Claim>
            //    {
            //        new Claim(ClaimTypes.Name, user.UserName),
            //        new Claim("UserId", user.Id.ToString()),
            //        new Claim("CompanyId", companyId.ToString()),
            //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            //    };

            //            foreach (var userRole in userRoles)
            //            {
            //                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            //            }

            //            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            //            var expireTime = Convert.ToInt32(_configuration["JWT:expiresIn"]);
            //            var token = new JwtSecurityToken(
            //                issuer: _configuration["JWT:ValidIssuer"],
            //                audience: _configuration["JWT:ValidAudience"],
            //                expires: DateTime.Now.AddHours(expireTime),
            //                claims: authClaims,
            //                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            //                );

            //            return Ok(new
            //            {
            //                token = new JwtSecurityTokenHandler().WriteToken(token),
            //                expiration = token.ValidTo
            //            });
            //        }
            //        return BadRequest(ErrorMessage("Wrong Password"));
            //    }
            //    return BadRequest(ErrorMessage("User does not exist."));
            //}
            //catch (Exception ex) { AppLogging.LogException(ex.Message); return BadRequest(ErrorMessage(ex.Message)); }
        }
    }
    //[Authorize]
    //[HttpPost]
    //[Route("RefreshToken")]
    //public async Task<IActionResult> RefreshToken()
    //{
    //    var user = await _userManager.FindByIdAsync(UserId.ToString());

    //    if (user != null)
    //    {
    //        var userRoles = await _userManager.GetRolesAsync(user);
    //        var companyId = int.Parse(await _companyService.GetCompanyId(user.Id));
    //        var authClaims = new List<Claim>
    //        {
    //            new Claim(ClaimTypes.Name, user.UserName),
    //            new Claim("UserId", user.Id.ToString()),
    //             new Claim("CompanyId", companyId.ToString()),
    //            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
    //        };
    //        foreach (var userRole in userRoles)
    //        {
    //            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
    //        }
    //        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
    //        var expireTime = Convert.ToInt32(_configuration["JWT:expiresIn"]);
    //        var token = new JwtSecurityToken(
    //            issuer: _configuration["JWT:ValidIssuer"],
    //            audience: _configuration["JWT:ValidAudience"],
    //            expires: DateTime.Now.AddHours(expireTime),
    //            claims: authClaims,
    //            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
    //            );

    //        return Ok(new
    //        {
    //            refreshToken = new JwtSecurityTokenHandler().WriteToken(token),
    //            expiration = token.ValidTo
    //        });

    //    }
    //    return BadRequest("UnAuthorized!");
    //}
}

