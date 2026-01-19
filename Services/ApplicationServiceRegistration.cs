using Microsoft.Extensions.DependencyInjection;
using Services.Implementation;
using Services.Implementation.Identity;
using Services.Interfaces;
using Services.Interfaces.Identity;

namespace Services
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            //Register Application Services

            services.AddTransient<IUserAuthenticationService, UserAuthenticationService>();
            services.AddTransient<ICompanyService, CompanyService>();

            return services;
        }
    }
}
