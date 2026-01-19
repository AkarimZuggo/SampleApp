using DataAccess.Context;
using DataAccess.Implementation;
using DataAccess.Implementation.Repository;
using DataAccess.Interfaces;
using DataAccess.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ApplicationConnectionString")));
            //Register Persistence Services
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ICompanyRepository, CompanyRepository>();

            return services;
        }
    }
}
