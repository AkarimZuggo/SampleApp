using Common.Logging;
using DataAccess;
using DataAccess.Context;
using Entities.DBEntities;
using Identity;
using Identity.Extension;
using Infrastructure.ResponseHandler.Handlers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services;
using System.Text.Json.Serialization;

namespace Admin.Api.ApplicationExtension
{
    public static class StartupExtensions
    {
        public static WebApplication ConfigureServices(
        this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers().AddJsonOptions(x =>{x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());});
            builder.Services.AddCors(options => {options.AddDefaultPolicy( builder =>{builder.AllowAnyOrigin().AllowAnyHeader() .AllowAnyMethod();});});
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddDbContext<AppDbContext>(otp => otp.UseNpgsql(builder.Configuration.GetConnectionString("ApplicationConnectionString")));
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddAuthentication();
            builder.Services.AddPersistenceServices(builder.Configuration);
            builder.Services.AddApplicationServices();
            builder.Services.AddIdentityServices(builder.Configuration);
            builder.Services.ConfigureJWT(builder.Configuration);
            builder.Services.ConfigureSwagger();
            #region Api Versioning
            //builder.Services.AddEndpointsApiExplorer();
            //// Add API Versioning to the Project
            //builder.Services.AddApiVersioning(opt =>
            //{
            //    opt.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
            //    opt.AssumeDefaultVersionWhenUnspecified = true;
            //    opt.ReportApiVersions = true;
            //    opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
            //                                                    new HeaderApiVersionReader("x-api-version"),
            //                                                    new MediaTypeApiVersionReader("x-api-version"));
            //});
            //// Add ApiExplorer to discover versions
            //builder.Services.AddVersionedApiExplorer(setup =>
            //{
            //    setup.GroupNameFormat = "'v'VVV";
            //    setup.SubstituteApiVersionInUrl = true;
            //});
            #endregion
            return builder.Build();
        }
        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            //app.UseMiddleware<ResponseHandlerMiddleware>();
            app.UseCors();
            app.AddSeedData();
            return SwaggerExtensions.UseSwaggerExtension(app);
        }
        public static void AddSeedData(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                try
                {
                    var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
                    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                    IdentitySeedDataInitializer.SeedData(userManager, roleManager);
                }
                catch (Exception ex)
                {
                    AppLogging.LogException($"Error while seeding data:{ex}");
                }
            }
        }

    }
}
