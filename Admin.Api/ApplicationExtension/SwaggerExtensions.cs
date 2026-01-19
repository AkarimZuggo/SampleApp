using Microsoft.OpenApi.Models;

namespace Admin.Api.ApplicationExtension
{
    public static class SwaggerExtensions
    {
        public static WebApplication UseSwaggerExtension(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.UseStaticFiles();
            return app;
        }
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            try
            {
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "MyProject",
                        Version = "v1",
                        Description = "MyProject API Services.",
                        Contact = new OpenApiContact
                        {
                            Name = "MyProject"
                        },
                    });
                    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                    // Define a custom security scheme without "Bearer" label
                    c.AddSecurityDefinition("JWT", new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer",
                        BearerFormat = "JWT",
                        Description = "Please enter your token without the 'Bearer' label."
                    });

                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
     {
         {
             new OpenApiSecurityScheme
             {
                 Reference = new OpenApiReference
                 {
                     Type = ReferenceType.SecurityScheme,
                     Id = "JWT"
                 }
             },
             new string[] {}
         }
     });
                });
            }
            catch (Exception)
            {
                // Handle exceptions, if any
            }
        }
    }
}
