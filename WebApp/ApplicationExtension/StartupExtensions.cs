using DataAccess;
using Identity.Extension;
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
          
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
           
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();
            app.MapRazorPages();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            return app;
        }
    }
}
