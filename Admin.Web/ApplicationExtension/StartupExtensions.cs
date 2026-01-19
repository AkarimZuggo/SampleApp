using DataAccess;
using Identity;
using Identity.Extension;
using Microsoft.AspNetCore.Identity;
using Services;

namespace WebApp.ApplicationExtension
{
    public static class StartupExtensions
    {
        public static WebApplication ConfigureServices(
        this WebApplicationBuilder builder)
        {
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();
            builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
            builder.Services.AddApplicationServices();
            builder.Services.AddPersistenceServices(builder.Configuration);
            builder.Services.AddIdentityServices(builder.Configuration);
            return builder.Build();
        }
        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.AddSeedData();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapRazorPages();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.Run();
            return app;
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

                    IdentitySeedDataInitializer.SeedData
                        (userManager, roleManager);

                }
                catch (Exception ex)
                {
                    //logger.Error($"Error while seeding data:{ex}");
                }
            }
        }
    }
}
