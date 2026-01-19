using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.Data
{
    public class AppIdentityContext : IdentityDbContext<AppUser>
    {
        public AppIdentityContext(DbContextOptions<AppIdentityContext> dbContextOptions) : base(dbContextOptions)
        {
        }
    }
}
