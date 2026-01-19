
using WebApp.ApplicationExtension;

var builder = WebApplication.CreateBuilder(args);
//var connectionString = builder.Configuration.GetConnectionString("ApplicationConnectionString") ?? throw new InvalidOperationException("Connection string 'AppIdentityContextConnection' not found.");

//builder.Services.AddDbContext<AppIdentityContext>(options => options.UseSqlServer(connectionString));

//builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<AppIdentityContext>();
var app = builder
    .ConfigureServices()
    .ConfigurePipeline();

//app.AddSeedData();

app.Run();