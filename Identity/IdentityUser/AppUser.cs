using Microsoft.AspNetCore.Identity;

namespace Identity
{
    public class AppUser : IdentityUser
    {
        public required string Name {  get; set; }
    }
}
