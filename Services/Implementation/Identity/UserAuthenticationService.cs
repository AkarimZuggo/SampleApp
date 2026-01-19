using Common.Constant;
using DTOs.Models.User;
using Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Interfaces;
using Services.Interfaces.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services.Implementation.Identity
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ICompanyService _companyService;
        public UserAuthenticationService(UserManager<AppUser> userManager,IConfiguration configuration,ICompanyService companyService)
        {
            _userManager = userManager;
            _companyService=companyService;
            _configuration= configuration;
        }
        public async Task<object> LoginUserAsync(LoginModel model)
        {
            var companyId = 0;
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                if (await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    //companyId = int.Parse(await _companyService.GetCompanyId(user.Id));
                    var userRoles = await _userManager.GetRolesAsync(user);

                    var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("UserId", user.Id.ToString()),
                //new Claim("CompanyId", companyId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

                    foreach (var userRole in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    }

                    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                    var expireTime = Convert.ToInt32(_configuration["JWT:expiresIn"]);
                    var token = new JwtSecurityToken(
                        issuer: _configuration["JWT:ValidIssuer"],
                        audience: _configuration["JWT:ValidAudience"],
                        expires: DateTime.Now.AddHours(expireTime),
                        claims: authClaims,
                        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                    return new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    };
                }
                else
                {
                    throw new Exception("Wrong Password");
                }
            }
            else
            {
                throw new Exception("User does not exist.");
            }
        }
        public async Task<bool> RegisterUserAsync(RegisterModel model)
        {

            
                var userExists = await _userManager.FindByEmailAsync(model.Email);
                if (userExists != null)
                    throw new Exception("User already exists!");
                if (model.Password != model.ConfirmedPassword)
                {
                    throw new Exception("Passwords does not match!");
                }
                AppUser user = new AppUser()
                {
                    Email = model.Email,
                    Name = model.Name,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.Email
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                {
                    throw new Exception("Passwords must be at least 6 characters");
                }
                var newUser = await _userManager.FindByEmailAsync(model.Email);
                await AddRole(newUser.Id, IdentityConstant.CompanyOwner);
                await AddUserClaimsAsync(newUser);
                var userRoles = await _userManager.GetRolesAsync(newUser);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, newUser.UserName),
                    new Claim("UserId", newUser.Id.ToString()),
                     new Claim("UserEmail", newUser.Email),
                };
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                return true;
        }
        private async Task<bool> AddRole(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user != null)
            {
                var userRole = await _userManager.GetRolesAsync(user);
                foreach (var rol in userRole)
                {
                    await _userManager.RemoveFromRoleAsync(user, rol);
                }
                await _userManager.AddToRoleAsync(user, role);
                return true;
            }
            return false;
        }
        private async Task AddUserClaimsAsync(AppUser user)
        {
            try
            {
                List<Claim> claminList = new List<Claim>();
                claminList.Add(new Claim("UserId", user.Id.ToString()));
                claminList.Add(new Claim("Email", user.Email));
                claminList.Add(new Claim("UserName", user.UserName));
                await _userManager.AddClaimsAsync(user, claminList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
