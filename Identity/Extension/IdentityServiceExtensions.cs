using Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Extension
{
    public static class IdentityServiceExtensions
    {
        public static void AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppIdentityContext>(options =>options.UseNpgsql(configuration.GetConnectionString("ApplicationConnectionString")));
            services.AddDefaultIdentity<AppUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AppIdentityContext>()
            .AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            });
        }
    }
}
