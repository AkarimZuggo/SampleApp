using DTOs.Models.User;

namespace Services.Interfaces.Identity
{
    public interface IUserAuthenticationService
    {
        Task<bool> RegisterUserAsync(RegisterModel model);
        Task<object> LoginUserAsync(LoginModel model);
    }
}
